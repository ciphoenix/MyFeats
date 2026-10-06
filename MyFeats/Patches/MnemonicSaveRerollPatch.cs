using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker.Blueprints.Classes;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using MyFeats.Feats;
using System;

namespace MyFeats.Patches;

[HarmonyPatch(typeof(RuleSavingThrow), nameof(RuleSavingThrow.OnTrigger))]
internal static class MnemonicSaveRerollPatch
{
    private const int ExtraRolls = 3;   // 1 normal + 3 extra = 4 dice, worst one counts

    private static BlueprintFeature _marker;
    private static BlueprintFeature Marker =>
        _marker ??= BlueprintTool.Get<BlueprintFeature>(MnemonicDevice.FeatureGuid);

    [HarmonyPrefix]
    private static void Prefix(RuleSavingThrow __instance)
    {
        try
        {
            var reason = __instance.Reason;
            if (reason?.Ability == null) return;                          
            var abilityType = reason.Ability.Blueprint.Type;
            if (abilityType != AbilityType.Spell &&
                abilityType != AbilityType.SpellLike &&
                abilityType != AbilityType.Supernatural) return;   // Extraordinary excluded

            var caster = reason.Caster ?? reason.Context?.MaybeCaster;
            var target = __instance.Initiator;
            if (caster == null || target == null || !target.IsEnemy(caster)) return;

            var fact = caster.Descriptor.GetFeature(Marker);               // Mnemonic Device = marker
            if (fact == null) return;

            __instance.D20.AddReroll(ExtraRolls, false, fact);             // false = take worst
        }
        catch (Exception e)
        {
            Main.Log.Error($"MnemonicSaveRerollPatch: {e}");
        }
    }
}