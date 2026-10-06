using Kingmaker.Blueprints.JsonSystem;               // TypeId
using Kingmaker.EntitySystem.Stats;                  // StatType, ModifiableValueAttributeStat
using Kingmaker.PubSubSystem;                        // IInitiatorRulebookHandler, ISubscriber, IInitiatorRulebookSubscriber
using Kingmaker.RuleSystem;                          // IRulebookHandler
using Kingmaker.RuleSystem.Rules.Abilities;          // RuleCalculateAbilityParams
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities.Blueprints;      // BlueprintAbility, AbilityType
using Kingmaker.UnitLogic.FactLogic;                 // UnitFactComponentDelegate
using System;

/// <summary>
/// Custom Heighten: every spell and spell-like ability the owner casts is treated as a higher
/// level by the owner's stat modifier. Raises the DC (10 + spell level + casting stat) and
/// anything else keyed on spell level. Free: no slot cost, and it does not touch metamagic flags.
/// </summary>
[TypeId("E7FC9069-85C2-4941-8E66-2CC3F47B1109")]
public class MyStatHeighten : UnitFactComponentDelegate,
    IInitiatorRulebookHandler<RuleCalculateAbilityParams>, IRulebookHandler<RuleCalculateAbilityParams>,
    ISubscriber, IInitiatorRulebookSubscriber
{
    public StatType Stat;
    public int MaxBonus = 10;     // safety cap, so a huge modifier can't push spell levels out of range

    public void OnEventAboutToTrigger(RuleCalculateAbilityParams evt)
    {
        var ability = evt.Blueprint as BlueprintAbility;
        if (ability == null) return;
        if (ability.Type != AbilityType.Spell && ability.Type != AbilityType.SpellLike) return;

        var attr = Owner.Stats.GetStat<ModifiableValueAttributeStat>(Stat);
        int bonus = attr == null ? 0 : Math.Min(MaxBonus, Math.Max(0, attr.Bonus));
        if (bonus > 0)
            evt.AddBonusSpellLevel(bonus);
    }

    public void OnEventDidTrigger(RuleCalculateAbilityParams evt) { }
}