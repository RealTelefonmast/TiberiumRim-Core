using TeleCore;
using UnityEngine;

namespace TR;

public class PlaySetting_EVA : PlaySettingsWorker
{
    public override bool Visible => true;
    public override Texture2D Icon => TRContent.Icon_EVA;
    public override string Description => "TR.PlaySettings.EVA";
    
    public override void OnToggled(bool isActive)
    {
        GameComponent_EVA.EVAComp().EVAActive = isActive;
    }
}