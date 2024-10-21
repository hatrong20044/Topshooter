using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVs : ActorVisual
{
    public void OnDead()
    {
        AudioController.Ins.PlaySound(AudioController.Ins.aiDeath);
        CineController.Ins.ShakeTrigger();
    }
}
