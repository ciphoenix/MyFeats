using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs;
using Kingmaker.UnitLogic.Mechanics.Actions;
using MyFeats.Feats;
using System;

namespace MyFeats.Patches;

[HarmonyPatch(typeof(ContextActionDispelMagic), "TryDispelBuff")]
internal static class UndispellableDebuffsPatch
{
    [HarmonyPrefix]
    private static bool Prefix(ContextActionDispelMagic __instance, Buff buff, ref bool __result)
    {
        try
        {
            var caster = buff.Context?.MaybeCaster;
            if (!MnemonicMarker.Has(caster)) return true;               // not one of ours

            // The unit the dispel is being run on (Target is not public on ContextAction)
            var holder = Traverse.Create(__instance).Property("Target").Property("Unit")
                                 .GetValue<UnitEntityData>();
            if (holder == null || !holder.IsEnemy(caster)) return true; // only buffs on enemies

            __result = false;   // same outcome as vanilla's IsNotDispelable skip
            return false;
        }
        catch (Exception e)
        {
            Main.Log.Error($"UndispellableDebuffsPatch: {e}");
            return true;        // on any error, fall back to vanilla behaviour
        }
    }
}