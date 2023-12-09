﻿using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace TR.Patches;

[StaticConstructorOnStartup]
public static class UIPatches
{
    static UIPatches()
    {
        TiberiumCoreMod.Tiberium.Patch(typeof(UI_BackgroundMain).GetMethod(nameof(UI_BackgroundMain.BackgroundOnGUI)), new HarmonyMethod(typeof(UIPatches), nameof(UIPatches.BackgroundOnGUIPatch)));
    }

    internal static bool BackgroundOnGUIPatch()
    {
        if (!TiberiumCoreSettings.Settings.UseCustomBackground) return true;
        bool flag = !((float) UI.screenWidth > (float) UI.screenHeight * (2048f / 1280f));
        Rect position;
        if (flag)
        {
            float height = (float) UI.screenHeight;
            float num = (float) UI.screenHeight * (2048f / 1280f);
            position = new Rect((float) (UI.screenWidth / 2) - num / 2f, 0f, num, height);
        }
        else
        {
            float width = (float) UI.screenWidth;
            float num2 = (float) UI.screenWidth * (1280f / 2048f);
            position = new Rect(0f, (float) (UI.screenHeight / 2) - num2 / 2f, width, num2);
        }

        GUI.DrawTexture(position, TRContent.BGPlanet, ScaleMode.ScaleToFit);
        return false;
    }
    
    //Tiberium Background Selection
    [HarmonyPatch(typeof(Dialog_Options))]
    [HarmonyPatch(nameof(Dialog_Options.DoUIOptions))]
    public static class Dialog_OptionsDoUIOptions_Patch
    {
        private static readonly MethodInfo _AddTiberiumOption = AccessTools.Method(typeof(Dialog_OptionsDoUIOptions_Patch), nameof(AddTiberiumBGOption));
        private static readonly MethodInfo _ChangeButtonLabel = AccessTools.Method(typeof(Dialog_OptionsDoUIOptions_Patch), nameof(ChangeButtonLabel));
        private static readonly MethodInfo _WindowStackGetter = AccessTools.PropertyGetter(typeof(Find), nameof(Find.WindowStack));

        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var instructionList = instructions.ToList();

            for (var i = 0; i < instructionList.Count; i++)
            {
                var cur = instructionList[i];
                var previous = i > 0 ? instructionList[i - 1] : null;

                if (previous != null && cur != null)
                {
                    if(previous.opcode == OpCodes.Call && previous.Calls(_WindowStackGetter) && cur.opcode == OpCodes.Ldloc_S && (cur.operand as LocalBuilder).LocalIndex.Equals(18))
                    {
                        yield return new CodeInstruction(OpCodes.Ldloc_S, 18);
                        yield return new CodeInstruction(OpCodes.Call, _AddTiberiumOption);
                    }
                }
                    
                if(cur.opcode == OpCodes.Stloc_S && (cur.operand as LocalBuilder).LocalIndex.Equals(17))
                {
                    yield return cur;
                    yield return new CodeInstruction(OpCodes.Ldloc_S, 17);
                    yield return new CodeInstruction(OpCodes.Call, _ChangeButtonLabel);
                    yield return new CodeInstruction(OpCodes.Stloc_S, 17);
                    continue;
                }

                yield return cur;
            }
        }

        private static void SetTiberiumBG()
        {
            ((UI_BackgroundMain)UIMenuBackgroundManager.background).overrideBGImage = TRContent.BGPlanet;
            TiberiumCoreSettings.Settings.UseCustomBackground = true;
        }

        public static TaggedString ChangeButtonLabel(TaggedString label)
        {
            if (TiberiumCoreSettings.Settings.UseCustomBackground)
            {
                return "TiberiumRim";
            }
            return label;
        }

        private static void AddTiberiumBGOption(List<FloatMenuOption> options)
        {
            options.Add(new FloatMenuOption("TiberiumRim", SetTiberiumBG, TRContent.ForgottenIcon, Color.white));
        }
    }
}