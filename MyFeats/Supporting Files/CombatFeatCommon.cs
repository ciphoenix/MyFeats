using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyFeats.Feats
{
    internal sealed class CombatFeatOptions
    {
        public StatType Stat;
        public string OwnGuid;
        public string AffinityGuid;
        public bool ReplaceAcStat = true;    // off for Dex (it already feeds AC)
        public bool SwapAttackStat = true;    // off for Str and Dex
        public bool SwapDamageStat = true;    // off for Str
        public bool ScaleSpellSlots = true;    // off for Str and Dex, on purpose
        public bool SpellCasterMods = false;   // ON for Int, Wis, Cha: Hex DC, all-spells DC, spell penetration
        public bool AbilityDcMods = false;   // DC + penetration without Hex (Con, for Kineticist)
    }

    internal static class CombatFeatCommon
    {
        private static readonly StatType[] RankScaledStats =
        {
            StatType.AdditionalCMB, StatType.AdditionalCMD, StatType.Speed,
            StatType.SaveWill, StatType.SaveReflex, StatType.SaveFortitude,
            StatType.HitPoints, StatType.Initiative, StatType.BonusCasterLevel
        };

        internal static string[] AllGuids() => new[]
        {
            CombatProwess.FeatureGuid, CombatAvoidance.FeatureGuid, CombatConditioning.FeatureGuid,
            CombatAnalysis.FeatureGuid, CombatInsight.FeatureGuid, CombatPersonality.FeatureGuid
        };

        internal static FeatureConfigurator Apply(FeatureConfigurator feature, CombatFeatOptions o)
        {
            feature = feature.AddRecalculateOnStatChange(stat: o.Stat);

            if (o.ScaleSpellSlots)
                feature = feature.AddComponent(new MyScalingSpellsPerDay
                { Stat = o.Stat, MinLevel = 1, MaxLevel = 10, Multiplier = 1 });

            if (o.ReplaceAcStat)
                feature = feature.AddReplaceStatBaseAttribute(baseAttributeReplacement: o.Stat,
                    replaceIfHigher: true, replaceMod: Kingmaker.UnitLogic.Buffs.BonusMod.AsIs,
                    targetStat: StatType.AC);

            if (o.SwapAttackStat)
                feature = feature.AddComponent(new AttackStatReplacement
                { ReplacementStat = o.Stat, CheckWeaponTypes = false });

            feature = feature
                .AddComponent(new AddTargetBeforeAttackRollTrigger
                {
                    ActionsOnAttacker = ActionsBuilder.New()
                        .ApplyBuff(buff: MyProtectiveLuckBuff.FeatureGuid,
                                   durationValue: ContextDuration.Fixed(1, DurationRate.Rounds))
                        .Build()
                })
                .AddMaxDexBonusIncrease(contextBonus: ContextValues.Rank(), checkCategory: false, useContextInstead: true)
                .AddFortification(bonus: 100)
                .AddModifyD20(takeBest: true, rollsAmount: 3, rule: RuleType.All,
                              rollCondition: ModifyD20.RollConditionType.None,
                              savingThrowType: FlaggedSavingThrowType.All);

            foreach (var s in RankScaledStats)
                feature = feature.AddContextStatBonus(stat: s, value: ContextValues.Rank());

            // Casting-stat feats (Int, Wis, Cha): Hex DC on top of the general bonus
            if (o.SpellCasterMods)
                feature = feature.AddIncreaseSpellContextDescriptorDC(descriptor: SpellDescriptor.Hex, value: ContextValues.Rank());

            // DC and spell penetration for every ability: Int, Wis, Cha, and Con (Kineticist blasts)
            if (o.SpellCasterMods || o.AbilityDcMods)
                feature = feature
                    .AddIncreaseAllSpellsDC(value: ContextValues.Rank())
                    .AddSpellPenetrationBonus(value: ContextValues.Rank());

            feature = feature
                .AddAttackTypeCriticalMultiplierIncrease(type: Kingmaker.Enums.WeaponRangeType.Normal, additionalMultiplier: 1)
                .AddAttackTypeCriticalMultiplierIncrease(type: Kingmaker.Enums.WeaponRangeType.Touch, additionalMultiplier: 1)
                .AddCriticalConfirmationBonus(value: ContextValues.Rank())
                .AddContextRankConfig(ContextRankConfigs.StatBonus(o.Stat).WithMultiplyByModifierProgression(1))
                .AddWeaponCriticalEdgeIncreaseStackable(value: 2)
                .AddFeatureOnApply(feature: o.AffinityGuid)
                .AddFeatureOnApply(feature: MnemonicDevice.FeatureGuid);

            foreach (var guid in AllGuids())
                if (guid != o.OwnGuid)
                    feature = feature.AddPrerequisiteNoFeature(feature: guid);

            if (o.SwapDamageStat)
                feature = Main.AllMeleeAndRangedCategories.Aggregate(feature, (f, category) =>
                    f.AddComponent(new WeaponTypeDamageStatReplacement
                    {
                        Stat = o.Stat,
                        Category = category,
                        OnlyOneHanded = false,
                        TwoHandedBonus = true
                    }));

            // Class-resource boost (Channel Energy, Burn, ...), keyed on the bearer's classes
            feature = feature.AddComponent(new MyClassResourceBonus { Stat = o.Stat });

            feature = feature.AddComponent(new MyStatHeighten { Stat = o.Stat });

            return feature;
        }
    }
}