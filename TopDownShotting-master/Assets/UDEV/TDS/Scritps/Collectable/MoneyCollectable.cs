using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyCollectable : Collectable
{
    public override void Trigger()
    {
        Prefs.coins += m_bonus;
        GUIManager.Ins.UpdateCoinCounting(Prefs.coins);
        AudioController.Ins.PlaySound(AudioController.Ins.coinPickup);
    }
    
}
