using Kingmaker.Blueprints;
using Kingmaker.Blueprints.JsonSystem;        // TypeId
using Kingmaker.EntitySystem.Stats;           // StatType, ModifiableValueAttributeStat
using Kingmaker.PubSubSystem;                 // IResourceAmountBonusHandler, IUnitSubscriber, ISubscriber
using Kingmaker.UnitLogic;                    // BlueprintAbilityResource
using Kingmaker.UnitLogic.FactLogic;          // UnitFactComponentDelegate

[TypeId("C19D02BC-75F3-4AD7-AD1A-F1892B967C28")]
public class MyClassResourceBonus : UnitFactComponentDelegate, IResourceAmountBonusHandler, IUnitSubscriber, ISubscriber
{
    public StatType Stat;

    public void CalculateMaxResourceAmount(BlueprintAbilityResource resource, ref int bonus)
    {
        int levels = 0;
        foreach (var (cls, res) in ClassResourceTable.Resolved)
            if (resource == res)
                levels += Owner.Progression.GetClassLevel(cls);

        if (levels <= 0) return;                      // bearer has none of the listed classes

        var attr = Owner.Stats.GetStat<ModifiableValueAttributeStat>(Stat);
        bonus += (attr != null ? attr.Bonus : 0) + levels;   // stat modifier once, plus class levels
    }
}