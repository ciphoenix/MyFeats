// MagicalAffinityInt.cs
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

namespace MyFeats.Feats
{
    public class MagicalAffinityInt
    {
        public static void Configure()
        {
            Main.Log.Log("Calling the Configure Method");
            var feature = FeatureConfigurator.New(Feature, FeatureGuid)
                .SetDisplayName("MagicalAffinityInt.FeatureName")
                .SetDescription("MagicalAffinityInt.FeatureDescription")
                .SetHideInUI(true);                                   // <- semicolon ends the declaration

            AffinityCommon.Apply(feature)
                .AddContextRankConfig(ContextRankConfigs.StatBonus(StatType.Intelligence).WithMultiplyByModifierProgression(2))
                .Configure();
        }

        internal static readonly string Feature = "MagicalAffinityIntFeat";
        internal static readonly string FeatureName = "Magical Affinity (Intelligence)";
        internal static readonly string FeatureGuid = "A1B2C3D4-4444-4A44-9A44-444444444444";
        private static readonly string FeatureDescription = "You become more attuned to magic, drawing resilience from your analytical mind, and can resist spells that should damage you";
    }
}