# 002-M：RitsuLib 设置页集成（已撤销）

状态：**已撤销**。v0.4.0 曾实施并构建安装（2026-08-06）；2026-08-07 随 v0.5.0 整体移除 RitsuLib，
改为常开替换，撤销过程的记录见 [3.DOCS/001-E](../3.DOCS/001-E-STS2模组机制取证笔记.md) 的「全局改键与移除 RitsuLib」一节。
下文保留立项时的调研结论与待办，作为决策留痕。

## 调研结论（详见 [3.DOCS/002-T](../3.DOCS/002-T-可选RitsuLib集成与创意工坊多语言标题调研.md)）

1. “无 RitsuLib 可运行 + 有 RitsuLib 时出设置页”有现成官方互操作通道（RegentFX 实证）：manifest 依赖留空；DLL 加 `RitsuLib.ModSettingsInterop.ProviderType` 元数据；实现约定方法名（CreateRitsuLibSettingsSchema / Get/SetRitsuLibSettingValue / 类型化读写 / Save / InvokeAction）；零编译引用。
2. “创意工坊多语言名称”是 Steam 平台功能，上传后到物品页按语言添加标题即可，与 mod 代码无关。

## 待用户拍板

- [x] 给 PhantomKillerMod 增加“可选 RitsuLib 设置页”——已实施；
- [x] 设置项清单与默认值——唯一开关“将暗影步替换为幻影杀手”，默认开；
- [x] RitsuLib 版本——本机创意工坊安装 v0.5.10（2026-08-04），互操作契约已按此版本核对；
- [ ] 创意工坊主标题与各语言标题对照（若用户需要我协助准备）。

## 已落地内容（v0.4.0）

- `ThirdParty/RitsuLibModConfig.cs`：互操作提供器（schema/读写/保存）+ 运行时开关。
- `Patches/ShadowStepTextPatches.cs`：卡名/卡面/Buff 文本补丁，支持实时切换。
- 所有既有补丁（OnPlay/卡图/Buff 图标）加开关守卫。
- 本地化键从覆盖原键改为新增 `SHADOW_STEP_PHANTOM.*` / `SHADOW_STEP_PHANTOM_POWER.*`。
- 程序集元数据 `RitsuLib.ModSettingsInterop.ProviderType`。
- 状态文件：`%LOCALAPPDATA%\SlayTheSpire2\PhantomKillerMod\ritsu_interop_state.json`。

## 待验证

- [ ] 有 RitsuLib：设置界面出现“单卡替换 > 幻影杀手”，切换开关后无需重启即时生效；
- [ ] 无 RitsuLib：mod 照常默认启用；
- [ ] 关闭开关后卡名/卡面/Buff/效果恢复原版；
- [ ] default 0.107.1 分支适配（沿用原计划，后续进行）。

## 若实施（路线草案）

- manifest：保持 `dependencies: []`；
- 新增普通静态类（不引用 RitsuLib），实现互操作方法名契约；
- 默认配置兜底，RitsuLib 缺省时行为与现在一致；
- 本地测试：分别在有/无 RitsuLib 两种环境启动验证；
- 不改变 110 / 107 双版本兼容策略（互操作通道同样按 RitsuLib 版本走）。
