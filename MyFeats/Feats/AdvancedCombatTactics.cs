using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// AdvancedCombatTactics.cs
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using Kingmaker.Blueprints.Classes;

namespace MyFeats.Feats
{
    internal class AdvancedCombatTactics
    {
        public static void Configure()
        {
            Main.Log.Log("Calling the Configure Method");
            FeatureSelectionConfigurator.New(Feature, FeatureGuid, FeatureGroup.Feat, FeatureGroup.CombatFeat)
                    .SetDisplayName("AdvancedCombatTactics.FeatureName")
                    .SetDescription("AdvancedCombatTactics.FeatureDescription")
                    .SetGroups(FeatureGroup.Feat, FeatureGroup.CombatFeat)
                    .AddToAllFeatures(
                        CombatProwess.FeatureGuid,
                        CombatAvoidance.FeatureGuid,
                        CombatConditioning.FeatureGuid,
                        CombatAnalysis.FeatureGuid,
                        CombatInsight.FeatureGuid,
                        CombatPersonality.FeatureGuid
                    )
                    .Configure();
            
        }

        private static readonly string Feature = "AdvancedCombatTactics";
        private static readonly string FeatureName = "Advanced Combat Tactics";
        internal static readonly string FeatureGuid = "A1B2C3D4-8888-4A88-9A88-888888888888";
        private static readonly string FeatureDescription = "You have honed a particular approach to combat, drawing on one aspect of yourself to gain an edge in battle";
    }
}