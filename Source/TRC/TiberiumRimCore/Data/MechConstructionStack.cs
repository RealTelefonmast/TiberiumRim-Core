using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace TR;

public class MechConstructionStack
{
    private MechConItem _curItem;
    private List<MechConItem> _queue;
    
    public void Tick()
    {
        
    }

    public void Begin()
    {
        
    }

    public void Finish()
    {
        
    }
}

public class MechConItem
{
    private MechRecipeDef _curRecipe;
    
    private List<ThingDefCount> _input;
    private bool _isPaid;
    private int _progress;
    
    

    public bool IsFinished => _progress < _curRecipe.workCost;
    
    public void AddInput(ThingDefCount input)
    {
    }

    public void CheckBeginNow()
    {
        _isPaid = _curRecipe.costList.All(cost => _input.Any(input => input.thingDef == cost.thingDef && input.Count >= cost.count));
    }

    public void TickProgress()
    {
        if(!IsFinished)
            _progress += 10;
        
    }
}