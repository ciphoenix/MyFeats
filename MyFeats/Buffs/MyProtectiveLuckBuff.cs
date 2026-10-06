// MyProtectiveLuckBuff.cs
using BlueprintCore.Blueprints.CustomConfigurators.UnitLogic.Buffs;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.RuleSystem;

namespace MyFeats.Feats
{
    internal class MyProtectiveLuckBuff
    {
        public static void Configure()
        {
            Main.Log.Log("Calling the Configure Method");
            BuffConfigurator.New(Feature, FeatureGuid)
                    .SetDisplayName("MyProtectiveLuckBuff.FeatureName")
                    .SetDescription("MyProtectiveLuckBuff.FeatureDescription")
                    .AddComponent(new ModifyD20
                    {
                        Rule = RuleType.AttackRoll,
                        RollsAmount = 3,
                        TakeBest = false,
                        DispellOnRerollFinished = true
                    })
                    .Configure();
        }

        internal static readonly string Feature = "MyProtectiveLuckBuff";
        internal static readonly string FeatureName = "Protective Luck (Effect)";
        internal static readonly string FeatureGuid = "3B9ACFF1-1680-4B67-A7CE-2CEED180ADB7";
        internal static readonly string FeatureDescription = "Internal: applied to an attacker to impose disadvantage on their next attack roll against you.";
    }
}