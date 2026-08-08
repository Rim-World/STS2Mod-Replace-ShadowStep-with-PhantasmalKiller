using System;
using System.Reflection;
using HarmonyLib;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Modding;

namespace PhantomKillerMod;

[ModInitializer("ModLoaded")]
public static class ModEntry
{
    public const string ModId = "PhantomKillerMod";

    public static readonly string PortraitPng =
        $"res://{ModId}/images/card_portraits/big/phantasmal_killer.png";

    public static readonly string PowerIcon64Path =
        $"res://{ModId}/images/powers/phantasmal.png";

    public static readonly string PowerIcon256Path =
        $"res://{ModId}/images/powers/big/phantasmal.png";

    private static Harmony? _harmony;

    public static void ModLoaded()
    {
        try
        {
            Log.Info($"{ModId}: loading...");
            _harmony = new Harmony(ModId);
            _harmony.PatchAll(Assembly.GetExecutingAssembly());
            Log.Info($"{ModId}: Harmony patches applied (ShadowStep -> Phantasmal Killer)");
        }
        catch (Exception e)
        {
            Log.Error($"{ModId}: failed to apply patches: {e}");
        }
    }
}
