﻿using Verse;

namespace TR;

public class TRGameSettingsInfo : WorldInformation
{
    //PlaySettings
    public bool ShowNetworkValues = false;
    public bool RadiationOverlay = false;

    public TRGameSettingsInfo(RimWorld.Planet.World world) : base(world)
    {
    }

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref ShowNetworkValues, "ShowNetworkValues");
        Scribe_Values.Look(ref RadiationOverlay, "RadiationOverlay");
    }
}