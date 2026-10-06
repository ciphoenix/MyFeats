using BlueprintCore.Actions.Builder;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
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
    internal class CombatInsight
    {
        public static void Configure()
        {
            Main.Log.Log("Calling the Configure Method");
            var feature = FeatureConfigurator.New(Feature, FeatureGuid)
                .SetDisplayName("CombatInsight.FeatureName")
                .SetDescription("CombatInsight.FeatureDescription")
                .SetHideInUI(true);

            CombatFeatCommon.Apply(feature, new CombatFeatOptions
            {
                Stat = StatType.Wisdom,
                OwnGuid = FeatureGuid,
                AffinityGuid = MagicalAffinityWis.FeatureGuid,
                SpellCasterMods = true
            })
                .Configure();
        }

        internal static readonly string Feature = "CombatAwareness";
        internal static readonly string FeatureName = "Combat Awareness";
        internal static readonly string FeatureGuid = "374EDCCC-7553-4B8A-A8FC-CF88412ED8DF";
        internal static readonly string FeatureDescription = "Your insight into enemy behaviour helps you avoid attacks";
    }
}