using BlueprintCore.Actions.Builder;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items.Weapons;
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
    internal class CombatProwess
    {
        public static void Configure()
        {
            Main.Log.Log("Calling the Configure Method");
            var feature = FeatureConfigurator.New(Feature, FeatureGuid)
                .SetDisplayName("CombatProwess.FeatureName")
                .SetDescription("CombatProwess.FeatureDescription")
                .SetHideInUI(true);

            CombatFeatCommon.Apply(feature, new CombatFeatOptions
            {
                Stat = StatType.Strength,
                OwnGuid = FeatureGuid,
                AffinityGuid = MagicalAffinityStr.FeatureGuid,
                SwapAttackStat = false,
                SwapDamageStat = false,
                ScaleSpellSlots = false
            })
                .Configure();
        }

        internal static readonly string Feature = "CombatProwess";
        internal static readonly string FeatureName = "Combat Prowess";
        internal static readonly string FeatureGuid = "5F08C6B2-E950-4271-85F8-5655E0AF8BE8";
        internal static readonly string FeatureDescription = "Your physical strength is your best weapon, Your feats of strength make enemies think twice about attacking you reducing their effectiveness against you";
    }
}