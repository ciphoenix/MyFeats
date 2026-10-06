using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Enums.Damage;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFeats.Feats
{
    public class MnemonicDevice
    {
        // Heighten is absent until the custom version exists (the vanilla auto-add did nothing).
        // Extend is absent on purpose: Enduring Spells is the duration mechanic.
        private const Metamagic CoreFlags =
            Metamagic.Reach | Metamagic.Piercing | Metamagic.Bolstered | Metamagic.Empower |
            Metamagic.Maximize | Metamagic.Selective | Metamagic.Intensified;

        private static readonly AutoMetamagic.AllowedType[] ItemTypes =
        {
            AutoMetamagic.AllowedType.FromScroll,
            AutoMetamagic.AllowedType.FromPotion
        };

        private static readonly DamageEnergyType[] AscendantElements =
        {
            DamageEnergyType.Cold, DamageEnergyType.Fire, DamageEnergyType.Electricity,
            DamageEnergyType.Acid, DamageEnergyType.Magic, DamageEnergyType.NegativeEnergy
        };

        private static readonly SpellDescriptor[] IgnoredImmunities =
        {
            SpellDescriptor.Poison, SpellDescriptor.Compulsion, SpellDescriptor.Death,
            SpellDescriptor.Daze, SpellDescriptor.Disease, SpellDescriptor.MindAffecting,
            SpellDescriptor.Nauseated, SpellDescriptor.Stun
        };

        public static void Configure()
        {
            Main.Log.Log("Calling the Configure Method");
            var feature = FeatureConfigurator.New(Feature, FeatureGuid)
                .SetDisplayName("MnemonicDevice.FeatureName")
                .SetDescription("MnemonicDevice.FeatureDescription")
                .SetHideInUI(true);

            // Spells and spell-likes: all core flags in one component
            feature.AddAutoMetamagic(allowedAbilities: AutoMetamagic.AllowedType.Any, metamagic: CoreFlags);

            // Quicken is restricted to spells and spell-likes
            feature.AddAutoMetamagic(allowedAbilities: AutoMetamagic.AllowedType.SpellOrSpellLike, metamagic: Metamagic.Quicken);

            // Scrolls and potions
            foreach (var item in ItemTypes)
                feature.AddAutoMetamagic(allowedAbilities: item, metamagic: CoreFlags | Metamagic.Quicken);

            foreach (var element in AscendantElements)
                feature.AddAscendantElement(element: element);

            foreach (var descriptor in IgnoredImmunities)
                feature.AddIgnoreSpellImmunity(spellDescriptor: descriptor);

            feature
                .AddComponent(new MyEnduringSpells())
                .AddMechanicsFeature(feature: AddMechanicsFeature.MechanicsFeatureType.QuickenPerformance2)
                .AddIgnoreConcealment()
                .Configure();
        }

        internal static readonly string Feature = "MnemonicDevice";
        internal static readonly string FeatureName = "Mnemonic Device";
        internal static readonly string FeatureGuid = "DD81C2DD-DDF3-4567-AB07-39A2F5187B92";
        internal static readonly string FeatureDescription = "You have discovered ways to fortify your mind, enabling you to memorise more spells";
    }
}