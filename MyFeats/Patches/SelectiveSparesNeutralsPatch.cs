using HarmonyLib;
using Kingmaker.EntitySystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFeats.Patches;

[HarmonyPatch(typeof(AreaEffectEntityData), "IsSuitableTargetType")]
internal static class SelectiveSparesNeutralsPatch
{
    [HarmonyPostfix]
    private static void Postfix(AreaEffectEntityData __instance, UnitEntityData unit,
                                bool ___m_CanAffectAllies, ref bool __result)
    {
        if (!__result || ___m_CanAffectAllies) return;   // already excluded, or Selective not active

        var caster = __instance.Context?.MaybeCaster;
        if (!MnemonicMarker.Has(caster)) return;         // vanilla behaviour for everyone else

        // Under Selective, a unit that got through is an enemy or a neutral.
        if (!caster.IsEnemy(unit)) __result = false;     // spare the neutral
    }
}