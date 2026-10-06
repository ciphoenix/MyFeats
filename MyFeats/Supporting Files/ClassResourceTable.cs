using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic;                    // BlueprintAbilityResource (my best guess at the namespace)
using System.Collections.Generic;
using System.Linq;

internal static class ClassResourceTable
{
    // One line per (class, resource) pair. Add new classes here.
    private static readonly (string Class, string Resource)[] Rows =
{
    (CharacterClassRefs.AlchemistClass.ToString(),  AbilityResourceRefs.AlchemistBombsResource.ToString()),
    (CharacterClassRefs.ArcanistClass.ToString(),   AbilityResourceRefs.ArcanistArcaneReservoirResource.ToString()),
    (CharacterClassRefs.BardClass.ToString(),       AbilityResourceRefs.BardicPerformanceResource.ToString()),
    (CharacterClassRefs.BarbarianClass.ToString(),  AbilityResourceRefs.RageResourse.ToString()),
    (CharacterClassRefs.BloodragerClass.ToString(), AbilityResourceRefs.BloodragerRageResource.ToString()),
    (CharacterClassRefs.CavalierClass.ToString(),   AbilityResourceRefs.CavalierChallengeResource.ToString()),
    (CharacterClassRefs.ClericClass.ToString(),     AbilityResourceRefs.ChannelEnergyResource.ToString()),
    (CharacterClassRefs.InquisitorClass.ToString(), AbilityResourceRefs.JudgmentResource.ToString()),
    (CharacterClassRefs.KineticistClass.ToString(), AbilityResourceRefs.BurnResource.ToString()),
    (CharacterClassRefs.MonkClass.ToString(),       AbilityResourceRefs.KiPowerResource.ToString()),
    (CharacterClassRefs.PaladinClass.ToString(),    AbilityResourceRefs.ChannelEnergyResource.ToString()),
    (CharacterClassRefs.PaladinClass.ToString(),    AbilityResourceRefs.LayOnHandsResource.ToString()),
    (CharacterClassRefs.PaladinClass.ToString(),    AbilityResourceRefs.SmiteEvilResource.ToString()),
    (CharacterClassRefs.PaladinClass.ToString(),    AbilityResourceRefs.WeaponBondResourse.ToString()),
    (CharacterClassRefs.ShamanClass.ToString(),     AbilityResourceRefs.ShamanItemBondResource.ToString()),
    (CharacterClassRefs.ShifterClass.ToString(),    AbilityResourceRefs.ShifterAspectResource.ToString()),
    (CharacterClassRefs.ShifterClass.ToString(),    AbilityResourceRefs.ShifterWildShapeResource.ToString()),
    (CharacterClassRefs.SkaldClass.ToString(),      AbilityResourceRefs.RagingSongResource.ToString()),
};

    private static List<(BlueprintCharacterClass Class, BlueprintAbilityResource Resource)> _resolved;

    // Looked up on first use, once the blueprint cache exists
    internal static List<(BlueprintCharacterClass Class, BlueprintAbilityResource Resource)> Resolved =>
        _resolved ??= Rows.Select(r => (BlueprintTool.Get<BlueprintCharacterClass>(r.Class),
                                        BlueprintTool.Get<BlueprintAbilityResource>(r.Resource))).ToList();
}