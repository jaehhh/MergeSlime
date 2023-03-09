using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 스탯
public class UserStat : MonoBehaviour
{
    private int point; // how many get coins when touch the creen
    public int Point
    {
        get { return point; }
        set { point = value; }
    }

    private int coin;
    public int Coin
    {
        get { return coin; }
        set { coin = value; }
    }
}
