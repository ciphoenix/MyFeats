using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Utils.Types;
using Kingmaker.Enums.Damage;
using Kingmaker.UnitLogic;  

namespace MyFeats.Supporting_Files
{
    internal static class AffinityCommon
    {
        private static readonly DamageEnergyType[] ResistedEnergies =
        {
            DamageEnergyType.NegativeEnergy, DamageEnergyType.Acid,          DamageEnergyType.Unholy,
            DamageEnergyType.Cold,           DamageEnergyType.Holy,          DamageEnergyType.Divine,
            DamageEnergyType.Fire,           DamageEnergyType.PositiveEnergy, DamageEnergyType.Sonic,
            DamageEnergyType.Electricity,    DamageEnergyType.Magic
        };

        private static readonly UnitCondition[] ImmuneConditions =
        {
            UnitCondition.LoseDexterityToAC, UnitCondition.Nauseated,    UnitCondition.Blindness,
            UnitCondition.DifficultTerrain,  UnitCondition.Prone,        UnitCondition.SuppressedEnergyResistance,
            UnitCondition.Stunned,           UnitCondition.CanNotAttack, UnitCondition.CantAct,
            UnitCondition.CantMove,          UnitCondition.CantUseStandardActions,
            UnitCondition.Frightened,        UnitCondition.Confusion,    UnitCondition.MovementBan
        };

        internal static FeatureConfigurator Apply(FeatureConfigurator feature)
        {
            foreach (var type in ResistedEnergies)
                feature.AddDamageResistanceEnergy(value: ContextValues.Rank(), type: type);

            feature
                .AddDamageResistanceHardness(value: ContextValues.Rank())
                .AddDamageResistanceForce(value: ContextValues.Rank())
                .AddImmunityToCriticalHits()
                .AddImmunityToPrecisionDamage()
                .AddImmunityToEnergyDrain()
                .AddImmunityToAbilityScoreDamage();

            foreach (var condition in ImmuneConditions)
                feature.AddConditionImmunity(condition: condition);

            return feature;
        }
    }
}