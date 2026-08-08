using HarmonyLib;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;

namespace PhantomKillerMod.Patches;

/// <summary>
/// 文本替换补丁：本地化文件新增 RE_SHADOW_STEP_PHANTOM /
/// RE_SHADOW_STEP_PHANTOM_POWER 键（模组专属前缀，避免键冲突），由这里切换到新键。
/// </summary>
[HarmonyPatch(typeof(CardModel), "TitleLocString", MethodType.Getter)]
public static class ShadowStepTitleLocStringPatch
{
    private static void Postfix(CardModel __instance, ref LocString __result)
    {
        if (__instance is ShadowStep && ModConfig.IsReplaceShadowStepEnabled)
        {
            __result = new LocString("cards", "RE_SHADOW_STEP_PHANTOM.title");
        }
    }
}

[HarmonyPatch(typeof(CardModel), "Description", MethodType.Getter)]
public static class ShadowStepDescriptionPatch
{
    private static void Postfix(CardModel __instance, ref LocString __result)
    {
        if (__instance is ShadowStep && ModConfig.IsReplaceShadowStepEnabled)
        {
            __result = new LocString("cards", "RE_SHADOW_STEP_PHANTOM.description");
        }
    }
}

[HarmonyPatch(typeof(PowerModel), "Title", MethodType.Getter)]
public static class ShadowStepPowerTitlePatch
{
    private static void Postfix(PowerModel __instance, ref LocString __result)
    {
        if (__instance is ShadowStepPower && ModConfig.IsReplaceShadowStepEnabled)
        {
            __result = new LocString("powers", "RE_SHADOW_STEP_PHANTOM_POWER.title");
        }
    }
}

[HarmonyPatch(typeof(PowerModel), "Description", MethodType.Getter)]
public static class ShadowStepPowerDescriptionPatch
{
    private static void Postfix(PowerModel __instance, ref LocString __result)
    {
        if (__instance is ShadowStepPower && ModConfig.IsReplaceShadowStepEnabled)
        {
            __result = new LocString("powers", "RE_SHADOW_STEP_PHANTOM_POWER.description");
        }
    }
}

[HarmonyPatch(typeof(PowerModel), "SmartDescription", MethodType.Getter)]
public static class ShadowStepPowerSmartDescriptionPatch
{
    private static void Postfix(PowerModel __instance, ref LocString __result)
    {
        if (__instance is ShadowStepPower && ModConfig.IsReplaceShadowStepEnabled)
        {
            __result = new LocString("powers", "RE_SHADOW_STEP_PHANTOM_POWER.smartDescription");
        }
    }
}
