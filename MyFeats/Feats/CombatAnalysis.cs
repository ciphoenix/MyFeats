using BlueprintCore.Actions.Builder;
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
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Actions.Builder.ContextEx;

namespace MyFeats.Feats
{
    internal class CombatAnalysis
    {
        public static void Configure()
        {
            Main.Log.Log("Calling the Configure Method");
            var feature = FeatureConfigurator.New(Feature, FeatureGuid)
                .SetDisplayName("CombatAnalysis.FeatureName")
                .SetDescription("CombatAnalysis.FeatureDescription")
                .SetHideInUI(true);

            CombatFeatCommon.Apply(feature, new CombatFeatOptions
            {
                Stat = StatType.Intelligence,
                OwnGuid = FeatureGuid,
                AffinityGuid = MagicalAffinityInt.FeatureGuid,
                SpellCasterMods = true
            })
                .Configure();
        }

        internal static readonly string Feature = "CombatAnalysis";
        internal static readonly string FeatureName = "Combat Analysis";
        internal static readonly string FeatureGuid = "15A12D7F-161A-48F5-8766-1C135018438E";
        internal static readonly string FeatureDescription = "Your analytic mind makes you see the enemy's moves before they make them";
    }
}