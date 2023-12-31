﻿using System.Linq;
using RimWorld;
using Verse;

namespace TR;

public class BaseEvent : IExposable
{
    public EventDef def;
    private int startTick = 0;
    private int endTick = 0;

    private LookTargets targets;

    public LookTargets EventTargets
    {
        get => targets;
        set => targets = value;
    }

    public string DescriptionWithTarget => def.description.Formatted(EventTargets.PrimaryTarget);

    protected virtual Map MapForEvent => Find.Maps.Where(m => m.IsPlayerHome).RandomElementByWeight(WeightForMap);


    public void ExposeData()
    {
        Scribe_Defs.Look(ref def, "props");
        Scribe_Values.Look(ref startTick, "startTick");
        Scribe_Values.Look(ref endTick, "endTick");
    }

    public void StartEvent(EventDef def)
    {
        this.def = def;
        startTick = Find.TickManager.TicksGame;
        endTick = startTick + def.ActiveTimeTicks;

        EventSetup();

        def.discoveries?.Discover();
        SendLetter(null, EventTargets);
    }

    public void SendLetter(IncidentParms parms, LookTargets targets)
    {
        def.letter?.SendLetter(parms, targets);
    }

    public void FinishEvent()
    {
        TRUtils.EventManager().Notify_EventFinished(this);
    }

    public void EventTick()
    {
        int tick = Find.TickManager.TicksGame;
        if (CanDoEventAction(tick))
        {
            EventAction();
        }

        if (ShouldFinishNow(tick))
            FinishEvent();
    }

    public bool ShouldFinishNow(int curTick)
    {
        return startTick == endTick || curTick >= endTick;
    }

    public virtual void EventSetup()
    {

    }

    public virtual void EventAction()
    {
    }

    public virtual bool CanDoEventAction(int curTick)
    {
        return ShouldFinishNow(curTick);
    }

    protected virtual float WeightForMap(Map map)
    {
        return StorytellerUtility.DefaultThreatPointsNow(map);
    }

    public string[] DescArguments => null;

    public string TimeReadOut
    {
        get
        {
            /*
            float days = ticksLeft.TicksToDays();
            float hours = GenDate.ToStringTicksToDays() GenDate.TicksPerHour;
            float minutes = ;
            float seconds = ;
            */
            return (endTick - Find.TickManager.TicksGame).ToStringTicksToPeriodVerbose(true, false);
        }
    }
}

