using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCollectable :Collectable
{

    public override void Trigger()
    {
        GameManager.Ins.CurLife += m_bonus;
        GUIManager.Ins.UpDateLifeInfo(GameManager.Ins.CurLife);


        AudioController.Ins.PlaySound(AudioController.Ins.lifePickup);
    }

}
