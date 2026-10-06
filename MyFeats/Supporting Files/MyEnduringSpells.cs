using System;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;

[AllowedOn(typeof(BlueprintUnitFact), false)]
[TypeId("96E8D4D1-D9F1-43B8-9798-929055C6713E")]
public class MyEnduringSpells : UnitFactComponentDelegate, IUnitBuffHandler, IGlobalSubscriber, ISubscriber
{
    private static readonly TimeSpan OneMinute = TimeSpan.FromMinutes(1);
    private static readonly TimeSpan TenMinutes = TimeSpan.FromMinutes(10);
    private static readonly TimeSpan OneHour = TimeSpan.FromHours(1);
    private static readonly TimeSpan OneDay = TimeSpan.FromHours(24);

    public void HandleBuffDidAdded(Buff buff)
    {
        // Same spell filter as vanilla: spellbook spells only, not items or spell-likes
        if (!buff.Blueprint.EmulateAbilityContext)
        {
            var ability = buff.Context?.SourceAbilityContext?.Ability;
            if (ability == null || ability.Spellbook == null || ability.SourceItem != null)
                return;
        }

        if (buff.MaybeContext?.MaybeCaster != Owner) return;

        var left = buff.TimeLeft;
        TimeSpan target;
        if (left > TenMinutes && left <= OneDay) target = OneDay;   // > 10 min  -> 24 h
        else if (left > OneMinute && left <= TenMinutes) target = OneHour;  // > 1 min, <= 10 min -> 1 h
        else return;                                                       // <= 1 min, or already > 24 h

        buff.SetEndTime(target + buff.AttachTime);
    }

    public void HandleBuffDidRemoved(Buff buff) { }
}