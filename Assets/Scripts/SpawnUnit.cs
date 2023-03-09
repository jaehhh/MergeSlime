using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering;

// 유닛 생성, 구매
public class SpawnUnit : MonoBehaviour
{
    [SerializeField]
    UnitTable[] unitTable;
    [SerializeField]
    private GameObject[] unitPrefab; // 유닛 프리팹  
    private List<Unit> spawnedUnit;
    [SerializeField]
    private int price = 100;
    private int maxPrice = 300;
    [SerializeField]
    TextMeshProUGUI buttonBuyText;

    [SerializeField]
    private Transform user;
    private UIController uiController;

    private void Awake()
    {
        uiController = GetComponent<UIController>();

        spawnedUnit = new List<Unit>();

        Spawn();

        // buttonBuyText.text = $"Buy {price}";
    }

    public void Spawn(int level = 0, bool fusing = false)
    {
        Vector3 position = Vector3.zero;

        // 합성이면 스폰을 마우스 지점으로
        if (fusing)
        {
#if UNITY_ANDROID || UNITY_IOS
            position  = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
#else
            position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
#endif

            position = new Vector3(position.x, position.y, 0);

            GameObject unit = Instantiate(unitPrefab[level], position, Quaternion.identity);

            unit.GetComponent<Unit>().Setup(user, this.transform, unitTable[level]);

            SortingLayer(unit.transform);

            // 리스트 추가
            spawnedUnit.Add(unit.GetComponent<Unit>());
        }
        // 합성이 아니면 랜덤 위치 스폰
        else
        {
            SpawnAtBackGround(level);
        }
    }

    private void SpawnAtBackGround(int level)
    {
        float random = Random.Range(1, 10);
        random /= 10;
        float random2 = Random.Range(1, 10);
        random2 /= 10;

        // 해상도 대비 랜덤값 적용
        Vector2 screenRandom = new Vector2(Camera.main.pixelWidth * random, Camera.main.pixelHeight * random2);
        // 해상도 값을 월드좌표로 변경
        Vector3 position = Camera.main.ScreenToWorldPoint(screenRandom);
        
        position = new Vector3(position.x, position.y, 0);

        RaycastHit2D[] hit = Physics2D.RaycastAll(position, Vector3.forward, Mathf.Infinity);


        // 화면 내 랜덤한 위치에 스폰
        GameObject unit = Instantiate(unitPrefab[level], position, Quaternion.identity);
        unit.GetComponent<Unit>().Setup(user, this.transform, unitTable[level]);

        SortingLayer(unit.transform);

        // 리스트 추가
        spawnedUnit.Add(unit.GetComponent<Unit>());
    }

    private void SortingLayer(Transform target)
    {
        // y위치에 따라 레이어 정리
        target.GetComponent<SortingGroup>().sortingOrder = (int)(target.transform.position.y * 1000) * -1;
    }

    public void BuyUnit()
    {
        int coin = user.GetComponent<UserStat>().Coin;

        if (coin >= price)
        {
            int price = this.price;

            Spawn();

            coin -= price;
            user.GetComponent<UserStat>().Coin = coin;
            uiController.UpdateCoin(coin);

            price += 50;
            this.price = price; ;
            if ( this.price > maxPrice)
            {
                this.price = maxPrice;
            }

            // buttonText.text = $"Buy {price}";
        }
        else
        {
            uiController.AlarmMessage("Not Enough Coin");
        }
    }

    public void AutoSpawn()
    {
        for(int i = 0; i < spawnedUnit.Count; i++)
        {
            int level = spawnedUnit[i].TouchToAutoSpawn();

            if (level >= 0)
            {
                Spawn(level);
            }
        }
    }

    public void DestroyUnit(Unit unit)
    {
        for (int i = 0; i < spawnedUnit.Count; i++)
        {
            if (spawnedUnit[i] == unit)
            {
                spawnedUnit.RemoveAt(i);
            }
        }
    }
}
