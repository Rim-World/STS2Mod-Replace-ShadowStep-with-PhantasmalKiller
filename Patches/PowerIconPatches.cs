using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace PhantomKillerMod.Patches;

/// <summary>
/// 把 ShadowStepPower（打出幻影杀手后立即获得的“幻影”Buff）的图标替换为 STS1 幻影图标。
/// 覆盖小图标路径、大图标路径以及两个 Texture 属性（含 pck 加载失败时的内嵌兜底）。
/// 注意：不修改 DoubleDamagePower（用户确认保持原名原图标）。
/// </summary>
[HarmonyPatch(typeof(PowerModel), "PackedIconPath", MethodType.Getter)]
public static class ShadowStepPowerPackedIconPathPatch
{
    private static bool Prefix(PowerModel __instance, ref string __result)
    {
        if (__instance is not ShadowStepPower || !ModConfig.IsReplaceShadowStepEnabled)
        {
            return true;
        }

        __result = ModEntry.PowerIcon64Path;
        return false;
    }
}

[HarmonyPatch(typeof(PowerModel), "ResolvedBigIconPath", MethodType.Getter)]
public static class ShadowStepPowerBigIconPathPatch
{
    private static bool Prefix(PowerModel __instance, ref string __result)
    {
        if (__instance is not ShadowStepPower || !ModConfig.IsReplaceShadowStepEnabled)
        {
            return true;
        }

        __result = ModEntry.PowerIcon256Path;
        return false;
    }
}

[HarmonyPatch(typeof(PowerModel), "Icon", MethodType.Getter)]
public static class ShadowStepPowerIconPatch
{
    private static bool Prefix(PowerModel __instance, ref Texture2D __result)
    {
        if (__instance is not ShadowStepPower || !ModConfig.IsReplaceShadowStepEnabled)
        {
            return true;
        }

        __result = PowerIconTextureLoader.Get64();
        return false;
    }
}

[HarmonyPatch(typeof(PowerModel), "BigIcon", MethodType.Getter)]
public static class ShadowStepPowerBigIconPatch
{
    private static bool Prefix(PowerModel __instance, ref Texture2D __result)
    {
        if (__instance is not ShadowStepPower || !ModConfig.IsReplaceShadowStepEnabled)
        {
            return true;
        }

        __result = PowerIconTextureLoader.Get256();
        return false;
    }
}
