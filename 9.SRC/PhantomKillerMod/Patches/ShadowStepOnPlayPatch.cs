using System.Threading.Tasks;
using HarmonyLib;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;

namespace PhantomKillerMod.Patches;

/// <summary>
/// 原版 ShadowStep.OnPlay：
///   1) CardCmd.Discard(手牌)   -- 弃掉整手牌
///   2) PowerCmd.Apply&lt;ShadowStepPower&gt; -- 下回合攻击双倍伤害
/// 替换为仅保留第 2 步，即幻影杀手效果。
/// </summary>
[HarmonyPatch(typeof(ShadowStep), "OnPlay")]
public static class ShadowStepOnPlayPatch
{
    private static bool Prefix(
        PlayerChoiceContext choiceContext,
        CardPlay cardPlay,
        ShadowStep __instance,
        ref Task __result)
    {
        if (!ModConfig.IsReplaceShadowStepEnabled)
        {
            return true;
        }

        __result = PhantasmalKillerOnPlay(choiceContext, cardPlay, __instance);
        return false;
    }

    private static async Task PhantasmalKillerOnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay cardPlay,
        ShadowStep instance)
    {
        await PowerCmd.Apply<ShadowStepPower>(
            choiceContext,
            instance.Owner.Creature,
            1m,
            instance.Owner.Creature,
            instance);
    }
}
