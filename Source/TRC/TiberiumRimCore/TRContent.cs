using RimWorld;
using TeleCore;
using UnityEngine;
using Verse;

namespace TRC;

[StaticConstructorOnStartup]
public static class TRContent
{
    public static readonly Texture2D BGPlanet = ContentFinder<Texture2D>.Get("UI/Menu/Background", true);
    public static readonly Texture2D ResearchBG = ContentFinder<Texture2D>.Get("UI/Menu/ResearchBG", true);
    public static readonly Texture2D MainMenu = ContentFinder<Texture2D>.Get("UI/Menu/MainMenu", true);
    public static readonly Texture2D MenuWindow = ContentFinder<Texture2D>.Get("UI/Menu/MenuWindow", true);
    public static readonly Texture2D Banner = ContentFinder<Texture2D>.Get("UI/Menu/Banner", true);
    public static readonly Texture2D LockedBanner = ContentFinder<Texture2D>.Get("UI/Menu/LockedBanner", true);
    
    public static readonly Texture2D TopBar = ContentFinder<Texture2D>.Get("UI/Menu/TopBar", true);
    public static readonly Texture2D TibOptionBG = ContentFinder<Texture2D>.Get("UI/Menu/TibOptionBG", true);
    public static readonly Texture2D TibOptionBG_Cut = ContentFinder<Texture2D>.Get("UI/Menu/TibOptionBG_Cut", true);
    public static readonly Texture2D TibOptionBG_CutFade = ContentFinder<Texture2D>.Get("UI/Menu/TibOptionBG_CutFade", true);
    
    //Research
    public static readonly Texture2D UnseenResearch = ContentFinder<Texture2D>.Get("UI/Icons/Research/UnseenResearch", true);
    public static readonly Texture2D NewResearch = ContentFinder<Texture2D>.Get("UI/Icons/Research/NewResearch", true);

    //
    public static readonly Texture2D OpenMenu = ContentFinder<Texture2D>.Get("UI/Icons/Research/OpenMenu", true);
    public static readonly Texture2D Construct = ContentFinder<Texture2D>.Get("UI/Icons/Research/Construct", true);
    public static readonly Texture2D SelectThing = ContentFinder<Texture2D>.Get("UI/Icons/Research/SelectThing", true);
}