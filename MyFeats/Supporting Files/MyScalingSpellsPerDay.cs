using Kingmaker.Blueprints;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Parts;
using Newtonsoft.Json;
using System;

[TypeId("758041BE-A29B-4615-9691-C9F014886E85")]
public class MyScalingSpellsPerDay
    : UnitFactComponentDelegate<MyScalingSpellsPerDay.ComponentData>
{
    public StatType Stat;
    public int MinLevel = 1;
    public int MaxLevel = 10;
    public int Multiplier = 1;   // bonus slots per point of ability modifier

    public class ComponentData
    {
        [JsonProperty]
        public int Applied;      // same delta goes to every level, so one int is enough
    }

    public override void OnTurnOn() => Sync(Desired());
    public override void OnTurnOff() => Sync(0);
    public override void OnRecalculate() => Sync(Desired());

    private int Desired()
    {
        var attr = Owner.Stats.GetStat<ModifiableValueAttributeStat>(Stat);
        return attr == null ? 0 : Math.Max(0, attr.Bonus * Multiplier);
    }

    private void Sync(int wanted)
    {
        int delta = wanted - Data.Applied;
        if (delta == 0) return;

        var part = Owner.Ensure<UnitPartExtraSpellsPerDay>();
        for (int lvl = Math.Max(0, MinLevel); lvl <= Math.Min(10, MaxLevel); lvl++)
            part.BonusSpells[lvl] += delta;
        Data.Applied = wanted;

        foreach (var book in Owner.Spellbooks)
            book.UpdateAllSlotsSize(true);
    }
}