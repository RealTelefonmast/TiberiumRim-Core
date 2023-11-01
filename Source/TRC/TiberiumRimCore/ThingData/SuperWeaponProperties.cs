using System;
using Verse;

namespace TRC;

public class SuperWeaponProperties
{
    public float chargeTime;
    public Type designator;
    public Type worker = typeof(SuperWeapon);

    private Designator resolvedDesignator;

    public Designator ResolvedDesignator
    {
        get
        {
            if (resolvedDesignator == null)
            {
                resolvedDesignator = (Designator)Activator.CreateInstance(designator);
            }
            return this.resolvedDesignator;
        }
    }
}