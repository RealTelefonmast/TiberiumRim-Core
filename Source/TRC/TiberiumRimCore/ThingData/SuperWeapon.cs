using RimWorld;
using Verse;

namespace TR;

public class SuperWeapon : IExposable
{
    public TRBuildingPrototype building;
    public int ticksUntilReady;

    public virtual bool Active => building.DestroyedOrNull() && IsPowered;

    public virtual bool CanFire => ticksUntilReady <= 0;

    public bool IsPowered => ((CompPowerTrader)building.PowerComp).PowerOn;
    public void ExposeData()
    {
        Scribe_References.Look(ref building, "building");
        Scribe_Values.Look(ref ticksUntilReady, "ticksUntilReady");
    }
}