using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace TR;

public class MechConstructionBillStack
{
    private MechConstructionBill _curItem;
    private List<MechConstructionBill> _queue;
    
    public void Tick()
    {
        if (_queue.Count <= 0) return;

        if (_curItem is { IsFinished: false })
        {
            _curItem.TickProgress();
            return;
        }
        
        var item = _queue[0];
        if (item.TryStartNow())
        {
            _curItem = item;
        }
    }

    public void Begin(MechConstructionBill bill)
    {

    }

    public void Finish()
    {
        
    }

    public void Notify_Reordered(MechRecipeDef def, int newIndex)
    {
        var bill = _queue.Find(c => c.Recipe == def);
        _queue.Remove(bill);
        _queue.Insert(newIndex, bill);
    }
    
    public void AddRecipe(MechRecipeDef recipe)
    {
        _queue.Add(new MechConstructionBill(recipe));
    }
}

public class MechConstructionBill
{
    private MechRecipeDef _curRecipe;
    
    private DefValueStack<ThingDef> _input;
    private bool _isActive;
    private bool _isPaid;
    private int _progress;
    
    public MechRecipeDef Recipe => _curRecipe;
    public bool IsFinished => _progress < _curRecipe.workCost;
    
    public MechConstructionBill(MechRecipeDef recipe)
    {
        _curRecipe = recipe;
        _input = new List<ThingDefCount>();
    }
    
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

    public bool CanStartNow()
    {
        CheckBeginNow();
        return _isPaid;
    }
    
    public bool TryStartNow()
    {
        if (!CanStartNow()) return false;
        return _isActive = true;
    }
}