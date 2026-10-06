using BlueprintCore.Utils;
using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic;
using MyFeats.Feats;

namespace MyFeats;

internal class CompanionFeatGranter : IAreaHandler
{
    static readonly (string unit, string feat)[] Map = {
        ("Seelah_Companion",     CombatPersonality.FeatureGuid),
        ("Camelia_Companion",    CombatInsight.FeatureGuid),
        ("Ember_Companion",      CombatPersonality.FeatureGuid),
        ("Lann_Companion",       CombatInsight.FeatureGuid),
        ("Woljif_Companion",     CombatAnalysis.FeatureGuid),
        ("Nenio_Companion",      CombatAnalysis.FeatureGuid),
        ("Arueshalae_Companion", CombatPersonality.FeatureGuid),
    };

    public void OnAreaBeginUnloading() { }
    public void OnAreaDidLoad() => Run();

    internal static void Run()
    {
        try
        {
            var units = Game.Instance?.Player?.AllCharacters;
            if (units == null) return;
            foreach (var u in units)
            {
                foreach (var (name, guid) in Map)
                {
                    if (u.Blueprint.name != name) continue;
                    var feat = BlueprintTool.Get<BlueprintFeature>(guid);
                    if (u.Facts.Get(feat) != null) continue;
                    u.AddFact(feat);
                    Main.Log.Log($"Granted feat to {name}");
                }
            }
        }
        catch (System.Exception e)
        {
            Main.Log.Log($"Companion grant failed: {e}");
        }
    }
}