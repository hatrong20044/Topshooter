using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Prefs 
{
   
    public static int coins
      {
          set=> PlayerPrefs.SetInt(PrefConst.COIN_KEY, value);
          get=>PlayerPrefs.GetInt(PrefConst.COIN_KEY,0);
      }
    public static String playerData
    {
        set => PlayerPrefs.SetString(PrefConst.PLAYER_DATA_KEY, value);
        get => PlayerPrefs.GetString(PrefConst.PLAYER_DATA_KEY);
    }
    public static String playerWeaponData
    {
        set => PlayerPrefs.SetString(PrefConst.PLAYER_WEAPON_DATA_KEY, value);
        get => PlayerPrefs.GetString(PrefConst.PLAYER_WEAPON_DATA_KEY);
    }
    public static String enemyData
    {
        set => PlayerPrefs.SetString(PrefConst.ENEMY_DATA_KEY, value);
        get => PlayerPrefs.GetString(PrefConst.ENEMY_DATA_KEY);
    }


    public static bool IsEnoughCoins(int coinToCheck)
    {
        return coins >= coinToCheck;//coin ng choi lon hon coin dua vao 
    }
}
