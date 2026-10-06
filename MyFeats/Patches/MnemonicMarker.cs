using BlueprintCore.Utils;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem;                    // EntityFact
using Kingmaker.EntitySystem.Entities;
using MyFeats.Feats;

namespace MyFeats.Patches
{
    internal static class MnemonicMarker
    {
        private static BlueprintFeature _bp;
        internal static BlueprintFeature Blueprint =>
            _bp ??= BlueprintTool.Get<BlueprintFeature>(MnemonicDevice.FeatureGuid);

        // The Mnemonic Device fact on this unit, or null if it doesn't have it
        internal static EntityFact Get(UnitEntityData unit)
        {
            var fact = unit?.Descriptor.Facts.Get(Blueprint);
            return fact != null && fact.Active ? fact : null;
        }

        internal static bool Has(UnitEntityData unit) => Get(unit) != null;
    }
}