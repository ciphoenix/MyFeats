using BlueprintCore.Actions.Builder;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Utils.Types;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Actions.Builder.ContextEx;

namespace MyFeats.Feats
{
    internal class CombatAvoidance
    {
        public static void Configure()
        {
            Main.Log.Log("Calling the Configure Method");
            var feature = FeatureConfigurator.New(Feature, FeatureGuid)
                .SetDisplayName("CombatAvoidance.FeatureName")
                .SetDescription("CombatAvoidance.FeatureDescription")
                .SetHideInUI(true);

            CombatFeatCommon.Apply(feature, new CombatFeatOptions
            {
                Stat = StatType.Dexterity,
                OwnGuid = FeatureGuid,
                AffinityGuid = MagicalAffinityDex.FeatureGuid,
                ReplaceAcStat = false,
                SwapAttackStat = false,
                ScaleSpellSlots = false
            })
                .Configure();
        }

        internal static readonly string Feature = "CombatAvoidance";
        internal static readonly string FeatureName = "Combat Avoidance";
        internal static readonly string FeatureGuid = "D58F87BE-D4EC-4A9D-807E-E4BE05969BB5";
        internal static readonly string FeatureDescription = "They cannot hit you if they cannot get to you. You weave around the battlefield at incredible speeds";
    }
}