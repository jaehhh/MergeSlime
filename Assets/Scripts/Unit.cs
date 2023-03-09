using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// 유닛 관리
public class Unit : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textUI;

    [SerializeField]
    private GameObject bright;
    private bool isBright = false;

    private int point = 1;
    public int level = 1;
    public int timeToFuse = 10;
    public int timeToSpawnNew = 5;
    public int touchTime;

    private Transform user;
    private Transform gameManager;

    public void Setup(Transform user, Transform gameManager, UnitTable unitTable)
    {
        this.user = user;
        this.gameManager = gameManager;

        point = unitTable.Point;
        level = unitTable.Level;
        timeToFuse = unitTable.TimeToFuse;
        timeToSpawnNew = unitTable.TimeToSpawnNew;

        user.GetComponent<UserStat>().Point += point;

        UpdateUnitUI();
    }

    // 문구 갱신
    private void UpdateUnitUI()
    {
        // 합성 가능
        if (touchTime >= timeToFuse)
        {
            // 후발광 시작
            if (isBright == false)
            {
                GameObject clone = Instantiate(bright, transform.position, Quaternion.identity);
                clone.transform.SetParent(transform);

                isBright = true;
            }
            // 분열 조건 달성
            if (touchTime >= timeToSpawnNew)
            {
                textUI.text = $"{timeToFuse}/{timeToFuse}\n{timeToSpawnNew}/{timeToSpawnNew}";
            }
            else
            {
                textUI.text = $"{timeToFuse}/{timeToFuse}\n{touchTime}/{timeToSpawnNew}";
            }
        }

        else if (touchTime >= timeToSpawnNew)
        {
            textUI.text = $"{touchTime}/{timeToFuse}\n{timeToSpawnNew}/{timeToSpawnNew}";
        }

        else
        {
            textUI.text = $"{touchTime}/{timeToFuse}\n{touchTime}/{timeToSpawnNew}";
        }
    }

    public int TouchToAutoSpawn()
    {
        touchTime++;

        UpdateUnitUI();

        /*
        if(touchTime == timeToFuse)
        {
            GetComponent<Animator>().SetTrigger("canFuse");
        }*/

        if (touchTime == timeToSpawnNew)
        {
            return level-1;
        }
        else
        {
            return -1;
        }
    }

    public void OnDestroy()
    {
        user.GetComponent<UserStat>().Point -= point;

        gameManager.GetComponent<SpawnUnit>().DestroyUnit(this);
    }
}