# ADR-004：可选 RitsuLib 设置页（v0.4.0）

状态：已实施（2026-08-06，v0.4.0 已构建安装，待游戏内验证）。

## 背景

用户希望为“单卡替换”系列 mod 提供可选的 RitsuLib 设置页：不装 RitsuLib 时 mod 独立运行；装了之后在 RitsuLib 设置界面出现统一的“单卡替换”栏目，可实时开关（无需重启游戏）。首个落地的是幻影杀手 mod：唯一开关“是否将暗影步替换为幻影杀手”。

## 决策

1. **零编译引用互操作通道**：采用 RitsuLib v0.5.10 的 `RuntimeInteropMirrorSource` 契约——DLL 只带程序集元数据 `RitsuLib.ModSettingsInterop.ProviderType`，指向 `PhantomKillerMod.ThirdParty.RitsuLibModConfig`；不引用任何 `STS2RitsuLib.*` 类型。
2. **统一栏目约定**：所有“单卡替换”类 mod 的 schema 使用同一个 `modId = SingleCardReplacement`，各自使用独立 `pageId` 与设置 key。RitsuLib 侧边栏按 modId 分组，因此多个 mod 自动合并到“单卡替换”分组下。
3. **实时开关**：所有替换补丁（打出效果、卡图、Buff 图标、卡名/卡面文本、Buff 文本）在执行时检查 `RitsuLibModConfig.IsReplaceShadowStepEnabled`；开关值即时生效，无需重启。
4. **本地化键迁移**：pck 不再覆盖原版 `SHADOW_STEP.*` / `SHADOW_STEP_POWER.*`，改为新增 `SHADOW_STEP_PHANTOM.*` / `SHADOW_STEP_PHANTOM_POWER.*`，由文本补丁在启用时切换到新键、关闭时回到原版键。
5. **状态存储**：`%LOCALAPPDATA%\SlayTheSpire2\PhantomKillerMod\ritsu_interop_state.json`，默认启用；无 RitsuLib 时同样按该文件/默认值运行。

## 影响

- 安装产物仍为 json + dll + pck，manifest 依赖保持 `[]`。
- 设置页文案暂为中英双语（zhs/eng），完整多语言文本按用户要求后续再议。
- 多人模式下每个玩家的本地开关各自生效。
