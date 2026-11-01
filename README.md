# STS2Mod-幻杀（PhantomKillerMod）

把《杀戮尖塔 2》静默猎手的「暗影步（Shadow Step）」整卡替换为《杀戮尖塔 1》的「幻影杀手
（Phantasmal Killer）」——去掉弃牌，只保留下回合攻击双倍伤害。

A Slay the Spire 2 mod that replaces the Silent's **Shadow Step** card with Slay the Spire 1's
**Phantasmal Killer**: the discard effect is removed, only "Attacks deal double damage next turn"
remains. Card name, card text, portrait, buff name and buff icon are all replaced.

| 项目 | 内容 |
|---|---|
| Mod ID | `PhantomKillerMod` |
| 版本 | 1.0 |
| 最低游戏版本 | 0.107.1 |
| 依赖 | 无（`dependencies: []`，不依赖 BaseLib / RitsuLib） |
| 影响游戏性 | 是（`affects_gameplay: true`） |

## 功能

- **效果替换**：打出该卡不再弃掉整手牌，只施加「下回合攻击造成双倍伤害」。
- **卡面完整替换**：卡名、卡面文本、1000×760 卡图（另备 250×190 小图）。
- **Buff 替换**：Buff 名称与描述改为 STS1「幻影」，图标取 STS1 幻影图集，含 64×64 与 256×256 两种尺寸。
- **15 个语言目录**：`deu`、`eng`、`esp`、`fra`、`ita`、`jpn`、`kor`、`pol`、`ptb`、`rus`、`spa`、`tha`、
  `tur`、`zhs`、`zht`（共 14 种语言，`esp` 与 `spa` 内容相同）。文本取自 STS1，仅作排版规范化。
- **不新增卡牌**：直接在原「暗影步」卡牌上修改，因此不会出现「新卡 + 旧卡」并存。

## 兼容性

- 支持 default 107 与 public beta 110 两个分支；`min_game_version` 为 0.107.1。
- 采用最小依赖写法（只依赖游戏自带的 Harmony），正常与大多数 mod 共存；不与任何基础 mod 产生编译依赖。
- 本地化键统一使用 `RE_` 前缀，避免与其它 mod 的同名键互相覆盖。
- 不修改 `DoubleDamagePower`（下一回合的过渡 Buff），保持其原名称与原图标。
- 不保证与盗版、修改版、移动版、老旧版本、冷门或次级 mod 的兼容性。
- 联机时房间内所有玩家需安装本 mod（`affects_gameplay: true`）。
- **存档影响**：开启 mod 期间，历史记录中的「暗影步」会显示为「幻影杀手」；关闭后恢复原状。
  本 mod 不写入任何自定义存档字段。

## 安装

1. 将压缩包内的 `PhantomKillerMod/` 整个文件夹解压到游戏的 `mods/` 目录：
   - Windows：`<Steam>/steamapps/common/Slay the Spire 2/mods/PhantomKillerMod/`
2. 目录内应有 `PhantomKillerMod.json`、`PhantomKillerMod.dll`、`PhantomKillerMod.pck`（以及可选的 `.pdb`）。
3. 启动游戏，在 mod 管理界面确认本 mod 已加载。

> 打包时请打包整个 `PhantomKillerMod/` 文件夹，而不是文件夹内部的散文件。

## 构建

需要 .NET 9 SDK 与 Godot 4.5.1 Mono（导出 pck）。工程路径与游戏路径写在
`9.SRC/PhantomKillerMod/PhantomKillerMod.csproj` 与 `build.ps1` 顶部，换机器时需自行修改。

```powershell
cd 9.SRC/PhantomKillerMod
./build.ps1
```

脚本依次执行：`dotnet build` → `Godot --headless --import` → `Godot --headless --export-pack`
→ 拷贝 `.dll` / `.pdb` / `.json` 到游戏的 `mods/PhantomKillerMod/`。

## 目录结构

```
STS2Mod-幻杀/
├─ 1.ADR/          决策记录（ADR.md 为索引）
├─ 2.PLANS/        施工计划（PLANS.md 为索引）
├─ 3.DOCS/         知识与取证笔记（DOCS.md 为索引）
├─ 4.SDD/          软件设计文档（SDD.md 为索引）
├─ 5.ChangeLog/    分片版本日志（ChangeLog.md 为索引）
├─ 9.SRC/          源码（PhantomKillerMod/ 为完整 Godot C# 工程）
├─ 10.OUTPUT/      构建产物与发布包（output.md 为索引，内容不入库）
├─ PRIVACY/        机密信息（PRIVACY.md 为索引，永不入库）
├─ TMP/            临时与过程文件（TMP.md 为索引，内容不入库）
├─ TOOLS/          外部工具清单（TOOLS.md）
├─ description/    创意工坊/发布文案（中英文）
└─ pics/           封面与素材图
```

## 文档索引

- 决策：[1.ADR/ADR.md](1.ADR/ADR.md)
- 计划：[2.PLANS/PLANS.md](2.PLANS/PLANS.md)
- 知识：[3.DOCS/DOCS.md](3.DOCS/DOCS.md)（内含 STS2 模组机制取证、RitsuLib 调研、存档损坏根因、翻译对照表）
- 设计：[4.SDD/SDD.md](4.SDD/SDD.md)
- 版本日志：[5.ChangeLog/ChangeLog.md](5.ChangeLog/ChangeLog.md) 与 [CHANGELOG.md](CHANGELOG.md)
- 产物清单：[10.OUTPUT/output.md](10.OUTPUT/output.md)

源码目录另有一份历史 README（上传前的仓库说明）：[9.SRC/PhantomKillerMod/README.md](9.SRC/PhantomKillerMod/README.md)。

## 版本

当前发布版本 `1.0`（2026-08-09）。完整变更见 [CHANGELOG.md](CHANGELOG.md)。

## 致谢

- 本 mod 使用 Codex（桌面版）+ DeepSeek V4 Flash（0731 正式版）完成，感谢梁圣。
- 感谢《杀戮尖塔 2》制作组为 mod 制作所做的努力。
- 感谢 Baselib 制作者提供的文档，感谢 GitHub 项目 [@jiegec/STS2RevertAnthony](https://github.com/jiegec/STS2RevertAnthony)
  提供的源码参考。

## 开源地址

- 仓库：<https://github.com/Rim-World/STS2Mod-Replace-ShadowStep-with-PhantasmalKiller>
- 欢迎在评论区提出意见与反馈。

## License

代码部分采用 MIT 许可，全文见 [LICENSE](LICENSE)。mod 内的卡名、卡图、卡面文本、Buff 图标等素材
取自《杀戮尖塔 1》/《杀戮尖塔 2》，版权归其权利人（Mega Crit Games, LLC）所有；本项目为免费的
非商业同人 mod。
