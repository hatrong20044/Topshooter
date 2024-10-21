using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Weapon Stats", menuName = "UDEV/TDS/Create Weapon Stats")]

public class WeaponStats : Stats
{
    [Header("Base Stats")]
    public int bullets;
    public float firerate;
    public float reloadTime;
    public float damage;

    [Header("Upgrade:")]
    public int level;
    public int maxLevel;
    public int bulletsUp;
    public float firerateUp;
    public float reloadTimeUp;
    public float damageUp;
    public int upgradePrice;//gia tien can thiet
    public int upgradePriceUp;//gia tien up

    [Header("Litmit:")]
    public float minFirerate = 0.1f;
    public float minReloadTime = 0.01f;

    //day la may cong thuc kieu nhu len level se dc cong chi so ay(doan vay)
    public int BulletsUpInfo { get => bulletsUp * (level + 1); }
    public float FirearateUpInfo { get => firerateUp * Helper.GetUpgradeFormula(level + 1); }

    public float ReloadTimeUpInfo { get=>reloadTime*Helper.GetUpgradeFormula(level+1); }
    public float DamageUpInfo { get=>damageUp*Helper.GetUpgradeFormula(level+1); }
    public override bool IsMaxLevel()
    {
        return level >= maxLevel;//nhan vat da max level,hnhu a y lam sai
    }
    public override void Load()
    {
        if (!string.IsNullOrEmpty(Prefs.playerWeaponData))
        {
            JsonUtility.FromJsonOverwrite(Prefs.playerWeaponData, this);
        }
    }
    public override void Save()
    {
        Prefs.playerWeaponData = JsonUtility.ToJson(this);
    }

    public override void Upgrade(Action OnSuccess = null, Action OnFailed = null)
    {
        if(Prefs.IsEnoughCoins(upgradePrice)&&!IsMaxLevel()) { //neu du tien va level sung chua max

            Prefs.coins -= upgradePrice;
            level++;
            bullets += bulletsUp * level;
            firerate -= firerateUp * Helper.GetUpgradeFormula(level);//dooj delay
            firerate=Mathf.Clamp(firerate, minFirerate, firerate);

            reloadTime-=reloadTimeUp*Helper.GetUpgradeFormula(level);
            reloadTime=Mathf.Clamp(reloadTime,minReloadTime, reloadTime);

            damage += damageUp * Helper.GetUpgradeFormula(level);
            upgradePrice += upgradePriceUp * level;

            Save();
            OnSuccess?.Invoke();

            return;
        }

        OnFailed?.Invoke();//sẽ kiểm tra xem hàm callback OnFailed có tồn tại trước khi gọi nó, giúp tránh lỗi khi hàm callback chưa được khởi tạo.
         
    }
}
