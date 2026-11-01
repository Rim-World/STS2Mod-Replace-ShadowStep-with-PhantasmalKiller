# 002-T：RitsuLib 可选集成与创意工坊多语言标题（2026-08-06）

> 背景：用户在 Steam 创意工坊看到 OLC 的《Crimson BIade VaIkyrie (IronCIad Skin/CardArt)》等 mod：不依赖任何基础 mod 也能运行；装了 RitsuLib 后能在其设置界面单独开关功能；创意工坊里英文名/中文名都能搜到，列表显示英文、详情页显示中文。用户要求解释这两个功能可能如何实现，并汇报后续需要什么帮助。

## 结论一：可选 RitsuLib 设置页 = “零编译依赖 + 元数据互操作契约”

### 常规做法（硬依赖，对照样本 LinkuraMod）

常规 RitsuLib 集成是“编译期引用 + manifest 声明依赖”：

- `mod_manifest.json` 的 `dependencies` 写 `STS2-RitsuLib`（新版本对象写法 `{ "id": "STS2-RitsuLib" }`，旧版本字符串写法 `"STS2-RitsuLib"`）；
- 代码里 `using STS2RitsuLib;` 并调用 `RitsuLibFramework.RegisterModSettings(modId, page => ...)`；
- 没有 RitsuLib 时该 mod 无法加载。

本机克隆的 `refs/LinkuraMod`（未随本仓库提供）就是这个样板：manifest 硬依赖，`LinkuraMod.cs` 里 `LinkuraModConfig.RegisterSettings(MOD_ID)`。

### 可选做法（RegentFX 实证，零编译引用）

本机 `mods（手动从N网下载，107及以前）\RegentFX\` 正好就是“可选 RitsuLib”的真实样本：

- manifest：`"dependencies": []` → 游戏独立加载，没有 RitsuLib 也照常运行；
- DLL 反编译后**不引用任何 `STS2RitsuLib.*` 类型**，只引用 `System.Text.Json`、Godot、`MegaCrit.Sts2.Core.*`；
- DLL 带一个 .NET 程序集元数据键：`RitsuLib.ModSettingsInterop.ProviderType` = `RegentFX.ThirdParty.RitsuLibModConfig`；
- `RegentFX.ThirdParty.RitsuLibModConfig` 是一个普通静态类，自建本地 JSON 存储（`%LOCALAPPDATA%\SlayTheSpire2\RegentFX\ritsu_interop_state.json`），并实现 RitsuLib 按**方法名约定**反射调用的几个方法。

RitsuLib 侧对应的源码（本机克隆 `refs/STS2-RitsuLib`，未随本仓库提供）把这一整套称为“Runtime Interop 镜像”：

- `src/Settings/ModSettings/Mirrors/RuntimeInterop/RuntimeInteropMirrorSource.cs`：
  - 扫描 `AppDomain.CurrentDomain.GetAssemblies()` 里所有程序集，读取 `AssemblyMetadataAttribute`，键为 `RitsuLib.ModSettingsInterop.ProviderType`；
  - 对命中的类型反射调用：
    - `CreateRitsuLibSettingsSchema()`：返回 JSON 字符串 / 可解析 JSON 的字符串 / 文件路径（RegentFX 返回的是本地 schema 文件路径）；
    - `GetRitsuLibSettingValue(string)` / `SetRitsuLibSettingValue(string, object?)`：通用读写；
    - `Get/SetRitsuLibSettingBool / Double / Int / String`：类型化读写（UI 控件按需使用）；
    - `SaveRitsuLibSettings()`：持久化保存；
    - `InvokeRitsuLibSettingAction(string)`：按钮动作（如重置）。
  - schema 内容是一份 JSON（`modId`、`modDisplayName`、`pages[].sections[].entries[]`，条目类型支持 Toggle/Slider/IntSlider/String/Choice/Button/Header/Paragraph 等），RitsuLib 据此在设置界面生成页面。
- `src/Settings/ModSettings/Mirrors/ModSettingsMirrorInteropPolicy.cs`：镜像策略支持 `RitsuLib.ModSettingsMirror.*` 系列 AssemblyMetadata 指令（Global/Mod/Type 三级 DisableSources / PreferredSource），说明这套互操作本来就是给“第三方可选”场景设计的。
- `src/Settings/ModSettings/Mirrors/ModSettingsMirrorRegistrarBootstrap.cs`：RitsuLib 在主菜单预热、设置子菜单打开、设置集成入口等处调用 `TryRegisterMirroredPages()`，其中包含 `RuntimeInteropMirrorSource.TryRegisterMirroredPages()`——所以装了 RitsuLib 后设置页会自动出现，不装则什么也不发生。

RitsuLib 另有一套 `RuntimeReflectionMirrorSource`（元数据键 `RitsuLib.ModSettingsReflection.ProviderType`），但它要求 provider 类型使用 RitsuLib 定义的 `[ModSettingsPage]` 等特性，本质需要编译期引用 RitsuLib，因此**不适合“完全可选”**。RegentFX 用的是 Interop 通道，才是真正零引用的可选集成。

### 对本项目（PhantomKillerMod）的意义

如果未来想让幻影杀手 mod 也“可选集成 RitsuLib 设置页”，可完全复刻 RegentFX 模式：manifest 依赖留空；DLL 只加一条 `AssemblyMetadata("RitsuLib.ModSettingsInterop.ProviderType", "命名空间.类型名")`；自己写一个普通静态类实现上述方法名契约，并用默认配置兜底。这样：

- 没有 RitsuLib：mod 照常运行（用默认值）；
- 有 RitsuLib：设置界面自动出现，可单独开关功能、持久化。

唯一注意点：这套 JSON schema / 方法名契约目前没有公开文档，只能以 RitsuLib 源码 + RegentFX 反编译为蓝本。

## 结论二：创意工坊多语言名称 = Steam 平台功能，与 mod 代码无关

### 上传工具只支持单一标题

官方 `sts2-mod-uploader`（本机克隆 `refs/sts2-mod-uploader`，未随本仓库提供）：

- `template/workshop.json` / `src/ModConfig.cs`：只有 `title`、`description` 等单值字段；
- `src/UploadCommand.cs`：调用 `SteamUGC.SetItemTitle(updateHandle, modConfig.title)` / `SetItemDescription`，没有语言参数、没有多语言数组。

所以“英文名 + 中文名都能搜到、列表英文详情中文”**不是上传器或 mod 实现的**。

### Steam Workshop 本身支持各语言标题/描述/标签

- 上传时标题会被当作英文/主标题；上传后作者在创意工坊物品页的 “Edit title and description” 界面按语言逐个添加标题与描述（Wallpaper Engine 玩家讨论、多个 Steam 社区讨论均证实）；
- 有游戏官方公告也明确写：上传时要求英文名，之后可在 Workshop 页面配置本地化名称；
- 2024 年 Steam 建议帖确认“目前可以翻译创意工坊主页标题、描述以及标签”。

### 显示与搜索行为

Steam 会索引所有本地化标题（所以中英文都能搜到）；列表/搜索结果按默认标题展示，物品详情页按浏览者语言展示本地化标题——正好吻合用户观察到的“外面英文、点进去中文”。

### 对本项目的意义

用户自行上传时：先用主标题（如英文名）上传，再在创意工坊物品管理页添加简体中文标题即可复现同样效果；不需要为此改任何模组代码。

## 证据清单

- RitsuLib 源码（本机克隆 `refs/STS2-RitsuLib`，未随本仓库提供）：
  - `src/Settings/ModSettings/Mirrors/RuntimeInterop/RuntimeInteropMirrorSource.cs`
  - `src/Settings/ModSettings/Mirrors/RuntimeReflection/RuntimeReflectionMirrorSource.cs`
  - `src/Settings/ModSettings/Mirrors/ModSettingsMirrorInteropPolicy.cs`
  - `src/Settings/ModSettings/Mirrors/ModSettingsMirrorRegistrarBootstrap.cs`
  - 其仓库内 `README.md` / `docs/pages/guide/mod-settings.md`
- RegentFX 本地反编译目录（本机 `refs/regentfx-decompiled/`，未随本仓库提供）与 `RegentFX.json`（`dependencies: []`）
- LinkuraMod（本机 clone `refs/LinkuraMod`，未随本仓库提供）：硬依赖 + `RegisterModSettings` 样板
- sts2-mod-uploader（本机 clone `refs/sts2-mod-uploader`，未随本仓库提供）：单标题字段证据
- Steam 社区/官方文档链接：
  - 建议帖（可翻译标题/描述/标签）：https://steamcommunity.com/discussions/forum/10/6982351863822799963/
  - 游戏公告（上传用英文名、之后页面配本地化名）：https://store.steampowered.com/news/posts/?feed=steam_community_announcements&enddate=1641389935
  - Wallpaper Engine 讨论（上传后添加韩文标题）：https://steamcommunity.com/app/431960/discussions/search/?l=schinese&q=wallpaper+engine&p=2

