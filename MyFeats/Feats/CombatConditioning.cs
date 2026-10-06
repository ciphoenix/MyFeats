using BlueprintCore.Actions.Builder;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
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
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Actions.Builder.ContextEx;

namespace MyFeats.Feats
{
    internal class CombatConditioning
    {
        public static void Configure()
        {
            Main.Log.Log("Calling the Configure Method");
            var feature = FeatureConfigurator.New(Feature, FeatureGuid)
                .SetDisplayName("CombatConditioning.FeatureName")
                .SetDescription("CombatConditioning.FeatureDescription")
                .SetHideInUI(true);

            CombatFeatCommon.Apply(feature, new CombatFeatOptions
            {
                Stat = StatType.Constitution,
                OwnGuid = FeatureGuid,
                AffinityGuid = MagicalAffinityCon.FeatureGuid,
                AbilityDcMods = true
            })
                .Configure();
        }

        internal static readonly string Feature = "CombatConditioning";
        internal static readonly string FeatureName = "Combat Conditioning";
        internal static readonly string FeatureGuid = "1C4F8A9C-DD22-45A6-80AF-6FBA55A80C6D";
        internal static readonly string FeatureDescription = "You can feel the flow of the battle and your body is conditioned to take on any attack";
    }
}