﻿using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace TRC;

public class ConditionalStatModifier
{
    public List<StatModifier> equippedOffsets = new List<StatModifier>();
    public List<StatModifier> unequippedOffsets = new List<StatModifier>();
    public List<ThingDef> requiredEquip;

    public List<StatModifier> StatModList(Pawn pawn)
    {
        return HasEquipped(pawn) ? equippedOffsets : unequippedOffsets;
    }

    public float StatOffsetForStat(StatDef stat, Pawn pawn)
    {
        var list = StatModList(pawn);
        if (list.NullOrEmpty()) return 0;
        return list.GetStatOffsetFromList(stat);
    }

    public bool HasEquipped(Pawn pawn)
    {
        return pawn.apparel != null && pawn.apparel.WornApparel.Any(a => requiredEquip.Contains(a.def));
    }

    public bool AffectsStat(StatDef stat)
    {
        return equippedOffsets.Concat(unequippedOffsets).Any(s => s.stat == stat);
    }
}