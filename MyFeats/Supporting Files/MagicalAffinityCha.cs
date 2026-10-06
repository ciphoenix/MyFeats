// MagicalAffinityCha.cs
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Properties.Getters;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums.Damage;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Properties;
using MyFeats.Supporting_Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// MagicalAffinityCha.cs
namespace MyFeats.Feats
{
    public class MagicalAffinityCha
    {
        public static void Configure()
        {
            Main.Log.Log("Calling the Configure Method");
            var feature = FeatureConfigurator.New(Feature, FeatureGuid)
                    .SetDisplayName("MagicalAffinityCha.FeatureName")
                    .SetDescription("MagicalAffinityCha.FeatureDescription")
                    .SetHideInUI(true);

            AffinityCommon.Apply(feature)
                .AddContextRankConfig(ContextRankConfigs.StatBonus(StatType.Charisma).WithMultiplyByModifierProgression(2))
                .Configure();
        }

        internal static readonly string Feature = "MagicalAffinityChaFeat";
        internal static readonly string FeatureName = "Magical Affinity (Charisma)";
        internal static readonly string FeatureGuid = "A1B2C3D4-6666-4A66-9A66-666666666666";
        private static readonly string FeatureDescription = "You become more attuned to magic, drawing resilience from your force of personality, and can resist spells that should damage you";
    }
}