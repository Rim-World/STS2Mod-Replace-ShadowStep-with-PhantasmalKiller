# 工具清单（TOOLS）

本目录用于收纳项目专用的自写脚本与便携工具；本仓库当前**没有**随附任何脚本或便携工具，
下表的工具均安装在系统目录或项目目录之外，仅登记信息以便复现构建环境。

目录约定：`TOOLS/` 的一级子项只允许为「以工具命名的文件夹」「`scripts/`」和本文件。

## 工具清单

| 时间 | 工具/脚本 | 用途 | 版本 | 大小 | 来源 | 安装方式/依赖 |
|---|---|---|---|---|---|---|
| 2026-08-05 | .NET SDK | 编译 `PhantomKillerMod.csproj`（`net9.0`） | 项目文档记录为 9.0.316；本机当前为 9.0.320（另装有 8.0.427） | 未记录 | <https://dotnet.microsoft.com/> | 系统级安装于 `C:\Program Files\dotnet\`；`dotnet --list-sdks` 可见 |
| 2026-08-05 | Godot 4.5.1 Mono（MegaDot 构建） | 生成 `.import` / `.ctex`，并用 `--export-pack` 导出 pck | `4.5.1-stable_mono_win64` | 221 MB | <https://godotengine.org/>（文档记录为 MegaDot 版） | 免安装解压于 `C:/Godot&GDRE/Godot_v4.5.1-stable_mono_win64/`；`build.ps1` 与 `csproj` 中硬编码该路径 |
| 2026-08-05 | ilspycmd | 反编译游戏 `sts2.dll` 取证（补丁点、签名、107/110 差异） | `9.1.0.7988`（`ICSharpCode.Decompiler 9.1.0.7988`） | 未记录 | NuGet 全局工具 `ilspycmd` | `dotnet tool install -g ilspycmd`；仅取证用，不参与构建 |
| 2026-08-05 | GitHub CLI（`gh`） | 创建 Release、查询仓库信息 | 未记录 | 未记录 | <https://cli.github.com/> | 系统级安装，已登录 `Rim-World` 账号；本 mod 的 Release 由它发布 |

## 自写脚本

本仓库没有独立的自写脚本；唯一的项目脚本是构建脚本 `9.SRC/PhantomKillerMod/build.ps1`，
它属于源码工程的一部分（四步构建：`dotnet build` → Godot `--import` → `--export-pack` → 拷贝到 mods），
故不放入 `TOOLS/scripts/`。

## 备注

- 版本与大小以登记时的本机状态为准；`未记录` 表示现有文档与仓库中没有可靠数据。
- 文档提到过的备选方案 `BSchneppe.StS2.PckPacker`（纯 dotnet 生成 pck）**最终没有采用**，
  实际构建走 Godot 导出，故不列入上表。
- 换机器构建时需修改 `PhantomKillerMod.csproj` 的 `Sts2Path` / `GodotPath` 与 `build.ps1` 顶部的常量。
