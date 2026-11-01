# 更新日志

本文件遵循 [Keep a Changelog](https://keepachangelog.com/zh-CN/1.1.0/) 结构，
版本号遵循 [语义化版本](https://semver.org/lang/zh-CN/)。

分片式版本记录（含开发人员反馈、失败经验与提交号）见 [5.ChangeLog/](5.ChangeLog/ChangeLog.md)。

## [未发布]

### 变更

- 仓库按 AGENTS.md 规定的十目录结构整理：源码移入 `9.SRC/PhantomKillerMod/`，
  文档分入 `1.ADR/`、`2.PLANS/`、`3.DOCS/`、`4.SDD/`、`5.ChangeLog/`，
  构建产物与发布包移入 `10.OUTPUT/`，素材图归入 `pics/`。
- 文档统一编号并补全索引（`ADR.md`、`PLANS.md`、`DOCS.md`、`SDD.md`、`ChangeLog.md`、`output.md`、`TMP.md`、`TOOLS.md`）。
- 新增根 `README.md`、根 `CHANGELOG.md` 与合并后的 `.gitignore`。

### 说明

- 本次整理**未修改任何代码**（`.cs` / `.csproj` / `.json` / `.cfg` / `build.ps1` 字节不变，仅改变目录层级）。
- mod 版本号仍为 `1.0`，未发布新版本。

## [1.0] - 2026-08-09

### 变更

- 版本号统一为 `1.0`（此前为 `0.5.0`），manifest 与产物名一致。
- 剔除冗余资源后重新构建：pck 由 803,300 字节降至 664,612 字节，`mod_image.png` 换为 133,601 字节的正式图标。
- 清理源码根目录的历史构建产物（`*.dll` / `*.pck` / `*.pdb`）与 `ThirdParty/` 残留。

### 修复

- 无（本版本不含逻辑修复）。

### 已知问题

- 若从 110 分支降回 107，游戏会因 run 存档 `schema_version` 更高而判为损坏并改名 `.FUT.corrupt`，
  与本 mod 无关，详见 [3.DOCS/003-T](3.DOCS/003-T-版本切换与存档损坏根因.md)。

## [0.5.0] - 2026-08-07

### 变更

- 全部本地化键加模组前缀 `RE_`：`RE_SHADOW_STEP_PHANTOM.*`、`RE_SHADOW_STEP_PHANTOM_POWER.*`。
- 移除可选 RitsuLib 设置页：删除 `ThirdParty/RitsuLibModConfig.cs`、程序集元数据与相关开关逻辑。
- 补丁启用判断简化为常量 `ModConfig.IsReplaceShadowStepEnabled => true`。

### 修复

- 修复与自带同卡改写的第三方 mod（CustomCardBalance）撞键导致的卡面文本被覆盖问题。

## [0.4.0] - 2026-08-06

### 新增

- 可选 RitsuLib 设置页（零编译引用互操作）：安装 RitsuLib 时出现「单卡替换 > 幻影杀手」开关，支持实时切换。

### 变更

- 本地化键由覆盖原版 `SHADOW_STEP.*` 改为新增 `SHADOW_STEP_PHANTOM.*` / `SHADOW_STEP_PHANTOM_POWER.*`。
- 所有替换补丁（效果、卡图、Buff 图标、卡名/卡面/Buff 文本）改为运行时读取开关。

### 已知问题

- RitsuLib 互操作契约无公开文档，依赖其方法名约定，存在随 RitsuLib 更新失效的风险。

## [0.3.1] - 2026-08-06

### 变更

- 模组图标换为用户提供的 420×420 图片（`cover_small.png`，206,110 字节），替换纯色占位。

## [0.3.0] - 2026-08-06

### 新增

- 新增意大利语、波兰语、泰语、土耳其语；语言目录达到 15 个（14 种语言，`esp` 与 `spa` 同内容）。

### 修复

- 按「方案 B」规范化排版：繁中卡面与 Buff 描述补句号、韩语卡面补句号并修正 Buff 双句号、
  简中去掉句号前空格、繁中去尾随空格。
- 按语法补全波兰语复数的第三形「tur」、俄语的「хода/ходов」。
- 土耳其语 `#b` 动态数字标记转为 `[blue]{}[/blue]`；泰语按语言习惯不加句号。

## [0.2.0] - 2026-08-06

### 新增

- 语言支持扩至 10 种（11 个语言目录）。
- Buff 名称与描述改为「幻影」，新增 STS1 幻影 Buff 图标（64×64 与 256×256）。

### 变更

- 卡图改为 STS1 官方卡图（500×380 → 1000×760）并生成 250×190 小图。

### 已知问题

- STS1 原文缺句末标点，繁中与韩语卡面显示无句号。

## [0.1.0] - 2026-08-05

### 新增

- 首个可加载版本：去掉暗影步的弃手牌效果，只保留下回合攻击双倍伤害。
- 卡名、卡面文本、卡图、Buff 名称替换为幻影杀手。

### 已知问题

- 仅支持简体中文；卡图与模组图标为纯色占位；仅针对 public beta 0.110.1 验证。
