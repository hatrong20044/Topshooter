using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Stats", menuName = "UDEV/TDS/Create Player Stats")]

public class PlayerStats : ActorStats
{
    [Header("Level up Base:")]
    public int level;
    public int maxLevel;
    public float xp;
    public float levelUpXpRequired;
    [Header("Level Up")]
    public float levelUpXpRequiredUp;
    public float hpUp;
    public override bool IsMaxLevel()
    {
        return level >= maxLevel;//nhan vat da max level,hnhu a y lam sai
    }
    public override void Load()
    {
        if (!string.IsNullOrEmpty(Prefs.playerData))
        {
            JsonUtility.FromJsonOverwrite(Prefs.playerData, this);
        }
    }
    public override void Save()
    {
       Prefs.playerData=JsonUtility.ToJson(this);
    }
    public override void Upgrade(Action OnSuccess = null, Action OnFailed = null)
    {
       
        while(xp>=levelUpXpRequired&& !IsMaxLevel())//(neu du kinh nghiem va chua o level cao nhat)
        {
            level++;
            xp -= levelUpXpRequired;
            hp += hpUp * Helper.GetUpgradeFormula(level);
            levelUpXpRequired += levelUpXpRequiredUp * Helper.GetUpgradeFormula(level);
            Save();
            OnSuccess?.Invoke();
        }
        if (xp < levelUpXpRequired || IsMaxLevel())
        {
            OnFailed?.Invoke();
        }
    }
}
