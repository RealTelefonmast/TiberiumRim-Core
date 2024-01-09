using Verse;

namespace TR.ThingClasses;

public class Building_Hangar : TRBuildingPrototype
{
    private MechConstructionStack _stack;
    
    public override void SpawnSetup(Map map, bool respawningAfterLoad)
    {
        base.SpawnSetup(map, respawningAfterLoad);
        _stack = new MechConstructionStack();
    }
}