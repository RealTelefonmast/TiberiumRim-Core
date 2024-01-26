using Verse;

namespace TR.ThingClasses;

public class Building_Hangar : TRBuildingPrototype
{
    private MechConstructionBillStack billStack;
    
    public override void SpawnSetup(Map map, bool respawningAfterLoad)
    {
        base.SpawnSetup(map, respawningAfterLoad);
        billStack = new MechConstructionBillStack();
    }

    public override void Tick()
    {
        base.Tick();
        billStack.Tick();
    }
    
    public void AddMechConstructionBill(MechRecipeDef recipe)
    {
        billStack.AddRecipe(recipe);
    }

    internal void MechConstructionFinished(MechConstructionBill bill)
    {
        
    }
}