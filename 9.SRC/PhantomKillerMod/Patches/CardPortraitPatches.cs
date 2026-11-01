using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;

namespace PhantomKillerMod.Patches;

/// <summary>
/// 把 ShadowStep 的卡图三个来源全部替换为模组内新图：
/// PortraitPngPath / Portrait / PortraitPath。
/// </summary>
[HarmonyPatch(typeof(CardModel), "PortraitPngPath", MethodType.Getter)]
public static class ShadowStepPortraitPngPathPatch
{
    private static bool Prefix(CardModel __instance, ref string __result)
    {
        if (__instance is not ShadowStep || !ModConfig.IsReplaceShadowStepEnabled)
        {
            return true;
        }

        __result = ModEntry.PortraitPng;
        return false;
    }
}

[HarmonyPatch(typeof(CardModel), "Portrait", MethodType.Getter)]
public static class ShadowStepPortraitPatch
{
    private static bool Prefix(CardModel __instance, ref Texture2D __result)
    {
        if (__instance is not ShadowStep || !ModConfig.IsReplaceShadowStepEnabled)
        {
            return true;
        }

        __result = PortraitTextureLoader.Get();
        return false;
    }
}

[HarmonyPatch(typeof(CardModel), "PortraitPath", MethodType.Getter)]
public static class ShadowStepPortraitPathPatch
{
    private static bool Prefix(CardModel __instance, ref string __result)
    {
        if (__instance is not ShadowStep || !ModConfig.IsReplaceShadowStepEnabled)
        {
            return true;
        }

        __result = ModEntry.PortraitPng;
        return false;
    }
}
