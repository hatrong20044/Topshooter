using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper 
{
    public static float GetUpgradeFormula(int level)//UpgradeFormula=cong thuc nang cap
    {
        return (level / 2 - 0.5f) * 0.5f;
    }
}
