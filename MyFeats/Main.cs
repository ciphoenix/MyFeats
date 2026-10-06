using BlueprintCore.Blueprints.Configurators;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.Utility;
using MyFeats.Feats;
using System.Reflection;
using System.Text;
using UnityModManagerNet;
using MyFeats.Supporting_Files;

namespace MyFeats;


public static class Main
{
    internal static Harmony HarmonyInstance;
    internal static UnityModManager.ModEntry.ModLogger Log;

    internal static readonly WeaponCategory[] AllMeleeAndRangedCategories =
    Enum.GetValues(typeof(WeaponCategory))
        .Cast<WeaponCategory>()
        .ToArray();


    public static bool Load(UnityModManager.ModEntry modEntry)
    {
        Log = modEntry.Logger;
        modEntry.OnGUI = OnGUI;
        HarmonyInstance = new Harmony(modEntry.Info.Id);
        try
        {
            HarmonyInstance.PatchAll(Assembly.GetExecutingAssembly());
        }
        catch
        {
            HarmonyInstance.UnpatchAll(HarmonyInstance.Id);
            throw;
        }
        return true;
    }

    public static void OnGUI(UnityModManager.ModEntry modEntry)
    {

    }

    [HarmonyPatch(typeof(BlueprintsCache))]
    public static class BlueprintsCaches_Patch
    {
        private static bool Initialized = false;

        [HarmonyPriority(Priority.First)]
        [HarmonyPatch(nameof(BlueprintsCache.Init)), HarmonyPostfix]
        public static void Init_Postfix()
        {
            try
            {
                if (Initialized)
                {
                    Log.Log("Already initialized blueprints cache.");
                    return;
                }
                Initialized = true;

                Log.Log("Patching blueprints.");

                Log.Log("Loading Mnemonic Device...");
                MnemonicDevice.Configure();

                string[] supersededFeats =
                {
                    FeatureRefs.EnduringSpells.ToString(),      FeatureRefs.EnduringSpellsGreater.ToString(),
                    FeatureRefs.EmpowerSpellFeat.ToString(),    FeatureRefs.MaximizeSpellFeat.ToString(),
                    FeatureRefs.QuickenSpellFeat.ToString(),    FeatureRefs.ExtendSpellFeat.ToString(),
                    FeatureRefs.HeightenSpellFeat.ToString(),   FeatureRefs.ReachSpellFeat.ToString(),
                    FeatureRefs.PersistentSpellFeat.ToString(), FeatureRefs.SelectiveSpellFeat.ToString(),
                    FeatureRefs.BolsteredSpellFeat.ToString(),
                    FeatureRefs.PiercingSpell.ToString(),       // no "Feat" suffix
                    FeatureRefs.IntensifiedSpell.ToString(),    // no "Feat" suffix
                };

                foreach (var guid in supersededFeats)
                    FeatureConfigurator.For(guid)
                        .AddPrerequisiteNoFeature(MnemonicDevice.FeatureGuid)
                        .Configure();

                Log.Log("Loaded.");


                Log.Log("Loading Magical Affinity (Strength)...");
                MagicalAffinityStr.Configure();
                Log.Log("Loaded");

                Log.Log("Loading Magical Affinity (Dexterity)...");
                MagicalAffinityDex.Configure();
                Log.Log("Loaded");

                Log.Log("Loading Magical Affinity (Constitution)...");
                MagicalAffinityCon.Configure();
                Log.Log("Loaded");

                Log.Log("Loading Magical Affinity (Intelligence)...");
                MagicalAffinityInt.Configure();
                Log.Log("Loaded");

                Log.Log("Loading Magical Affinity (Wisdom)...");
                MagicalAffinityWis.Configure();
                Log.Log("Loaded");

                Log.Log("Loading Magical Affinity (Charisma)...");
                MagicalAffinityCha.Configure();
                Log.Log("Loaded");

                Log.Log("Loading Protective Luck Buff...");
                MyProtectiveLuckBuff.Configure();
                Log.Log("Loaded.");

                Log.Log("Loading Combat Personality...");
                CombatPersonality.Configure();
                Log.Log("Loaded.");

                Log.Log("Loading Combat Analysis...");
                CombatAnalysis.Configure();
                Log.Log("Loaded.");

                Log.Log("Loading Combat Avoidance...");
                CombatAvoidance.Configure();
                Log.Log("Loaded.");

                Log.Log("Loading Combat Insight...");
                CombatInsight.Configure();
                Log.Log("Loaded.");

                Log.Log("Loading Combat Conditioning...");
                CombatConditioning.Configure();
                Log.Log("Loaded.");

                Log.Log("Loading Combat Prowess...");
                CombatProwess.Configure();
                Log.Log("Loaded.");

                Log.Log("Loading Advanced Combat Tactics...");
                AdvancedCombatTactics.Configure();
                Log.Log("Loaded.");

                FeatureSelectionConfigurator.For("247a4068296e8be42890143f451b4b45")
                    .AddToAllFeatures(AdvancedCombatTactics.FeatureGuid)
                    .Configure();

                PreloadCompanionFeats();

                EventBus.Subscribe(new CompanionFeatGranter());

            }
            catch (Exception e)
            {
                Log.Log(string.Concat("Failed to initialize.", e));
            }
        }

        private static void PreloadCompanionFeats()
        {
            var map = new (string name, string unitGuid, string featGuid)[] {
                ("Seelah",     "54be53f0b35bf3c4592a97ae335fe765", CombatPersonality.FeatureGuid),
                ("Camelia",    "397b090721c41044ea3220445300e1b8", CombatInsight.FeatureGuid),
                ("Ember",      "2779754eecffd044fbd4842dba55312c", CombatPersonality.FeatureGuid),
                ("Lann",       "cb29621d99b902e4da6f5d232352fbda", CombatInsight.FeatureGuid),
                ("Woljif",     "766435873b1361c4287c351de194e5f9", CombatAnalysis.FeatureGuid),
                ("Nenio",      "1b893f7cf2b150e4f8bc2b3c389ba71d", CombatAnalysis.FeatureGuid),
                ("Arueshalae", "a352873d37ec6c54c9fa8f6da3a6b3e1", CombatPersonality.FeatureGuid),
            };

            foreach (var (name, unit, feat) in map)
            {
                try
                {
                    UnitConfigurator.For(unit)
                        .AddFacts(new List<Blueprint<BlueprintUnitFactReference>> { feat })
                        .Configure();
                    Log.Log($"Preloaded feat on {name}");
                }
                catch (Exception e)
                {
                    Log.Log($"Preload failed for {name}: {e.Message}");
                }
            }
        }
    }
}