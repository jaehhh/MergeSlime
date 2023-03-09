using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 스탯 계산
public class StatCalculator : MonoBehaviour
{
    private UserStat userStat;

    private void Awake()
    {
        userStat = GetComponent<UserStat>();
    }

    public void GetCoin()
    {
        userStat.Coin += userStat.Point;
    }
}
