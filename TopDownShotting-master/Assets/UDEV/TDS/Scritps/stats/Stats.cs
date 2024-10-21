using System;
using UnityEngine;

//class xu li nhung chi so co so trong game
public abstract class Stats : ScriptableObject
{
    public abstract void Save();
    public abstract void Load();
    public abstract void Upgrade(Action OnSuccess=null,Action OnFailed=null);

    public abstract bool IsMaxLevel();

}
