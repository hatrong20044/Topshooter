using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVS : ActorVisual
{
    [SerializeField] private GameObject m_deadVfxPrefab;
    private Player m_player;
    private PlayerStats m_playerStats;

    private void Start()
    {
        m_player = (Player)m_actor;
        m_playerStats = m_player.PlayerStats;
    }


    public void OnLostLife()
    {
        if(m_player==null || m_playerStats==null) return;
        AudioController.Ins.PlaySound(AudioController.Ins.lostLife);

        GUIManager.Ins.UpDateLifeInfo(GameManager.Ins.CurLife);
        GUIManager.Ins.UpdateHpInfo(m_player.CurHp, m_playerStats.hp);
    }
    public override void OnTakeDamage()
    {
        base.OnTakeDamage();
        GUIManager.Ins.UpdateHpInfo(m_actor.CurHp, m_actor.statData.hp);
    }
    

    public void OnDead()
    {
        if (m_deadVfxPrefab)
        {
            Instantiate(m_deadVfxPrefab, transform.position, Quaternion.identity);
        }
        AudioController.Ins.PlaySound(AudioController.Ins.playerDeath);
        GUIManager.Ins.ShowGameOverDialog();
    }

    public void OnAddXp()
    {
        if (m_playerStats == null) return;
        GUIManager.Ins.UpdateLevelInfo(m_playerStats.level, m_playerStats.xp, m_playerStats.levelUpXpRequired);
    }
    public void OnLevelUp()
    {
        AudioController.Ins.PlaySound(AudioController.Ins.levelUp);
    }
}
