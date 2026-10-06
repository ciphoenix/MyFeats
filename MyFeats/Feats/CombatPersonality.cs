using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFeats.Feats
{
    public class CombatPersonality
    {
        public static void Configure()
        {
            Main.Log.Log("Calling the Configure Method");
            var feature = FeatureConfigurator.New(Feature, FeatureGuid)
                .SetDisplayName("CombatPersonality.FeatureName")
                .SetDescription("CombatPersonality.FeatureDescription")
                .SetHideInUI(true);

            CombatFeatCommon.Apply(feature, new CombatFeatOptions
            {
                Stat = StatType.Charisma,
                OwnGuid = FeatureGuid,
                AffinityGuid = MagicalAffinityCha.FeatureGuid,
                SpellCasterMods = true
            })
                .Configure();
        }

        internal static readonly string Feature = "CombatPersonality";
        internal static readonly string FeatureName = "Combat Personality";
        internal static readonly string FeatureGuid = "1A1A6844-7D67-407F-93DC-CA48B999DAD8";
        internal static readonly string FeatureDescription = "You can avoid attacks via sheer personality";
    }
}