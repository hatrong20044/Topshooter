using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunUpgradeDialog : Dialog {

    [SerializeField] private GunStatUI m_bulletStatUI;
    [SerializeField] private GunStatUI m_damageStatUI;
    [SerializeField] private GunStatUI m_fireateStatUI;
    [SerializeField] private GunStatUI m_reloadStatUI;
    [SerializeField] private Text m_upgradeBtnTxt;
    private Weapon m_weapon;
    private WeaponStats m_weaponStats;
    public override void Show(bool isShow)
    {
        base.Show(isShow);
        Time.timeScale = 0f;
        m_weapon=GameManager.Ins.Player.weapon;
        m_weaponStats = m_weapon.statData;
        UpdateUI();
    }
    private void UpdateUI()
    {
        if (m_weapon == null || m_weaponStats == null) return;
        if(titleTxt) titleTxt.text = $"LEVEL{m_weaponStats.level.ToString("00")}";
        if(m_upgradeBtnTxt) m_upgradeBtnTxt.text=$"UP[${m_weaponStats.upgradePrice.ToString("n0")}]";
        if (m_bulletStatUI)
        {
            m_bulletStatUI.UpdateStat("Bullet:", m_weaponStats.bullets.ToString("n0"), $"(+{m_weaponStats.BulletsUpInfo.ToString("n0")})");
        }

        if (m_damageStatUI)
        {
            m_damageStatUI.UpdateStat("Damage:", m_weaponStats.damage.ToString("F2"), $"(+{m_weaponStats.DamageUpInfo.ToString("F3")})");
        }

        if (m_fireateStatUI)
        {
            m_fireateStatUI.UpdateStat("Firerate:", m_weaponStats.firerate.ToString("F2"), $"(+{m_weaponStats.FirearateUpInfo.ToString("F3")})");
        }

        if (m_reloadStatUI)
        {
            m_reloadStatUI.UpdateStat("Reload:", m_weaponStats.reloadTime.ToString("F2"), $"(+{m_weaponStats.ReloadTimeUpInfo.ToString("F3")})");
        }
    }

    public void UpgradeGun()
    {
        if (m_weaponStats == null) return;
        m_weaponStats.Upgrade(OnUpgradeSuccess, OnUpGradeFailed);
    }

    private void OnUpGradeFailed()
    {
        Debug.Log("Upgrade failled");
    }

    private void OnUpgradeSuccess()
    {
        UpdateUI();
        GUIManager.Ins.UpdateCoinCounting(Prefs.coins);
        AudioController.Ins.PlaySound(AudioController.Ins.upgradeSuccess);
    }

    public override void Close()
    {
       
        base.Close();
        Time.timeScale = 1f;
    }

}
