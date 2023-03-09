using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 조작
public class PlayerController : MonoBehaviour
{
    private UserStat userStat;
    private StatCalculator statCalculator;
    private Interact interact;
    [SerializeField]
    private UIController uiController;
    [SerializeField]
    private SpawnUnit spawnUnit;

    Touch touch;

    private void Awake()
    {
        userStat = GetComponent<UserStat>();
        statCalculator = GetComponent<StatCalculator>();
        interact = GetComponent<Interact>();

        if(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        touch = Input.GetTouch(0);
    }

    private void Update()
    {
#if UNITY_ANDROID || UNITY_IPHONE
        TouchMobile();
#else
        Touch();

#endif
    }

    // 마우스 클릭
    private void Touch()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector3.forward, Mathf.Infinity);

            // 마우스 포인터에 유닛이 있을 시(현재는 땅까지 적용)
            if (hit)
            {
                if(hit.transform.CompareTag("Unit"))
                {
                    interact.MoveUnit(hit.transform);
                }
                else if(hit.transform.CompareTag("Ground"))
                {
                    TouchTheGround();
                }
                else
                { }
            }

            Debug.Log(hit.transform.name);
        }

        else if (Input.GetMouseButtonUp(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector3.forward, Mathf.Infinity);

            if (hit)
            {
                if (hit.transform.CompareTag("Unit"))
                {
                    interact.FuseUnit(hit.transform);
                }
            }

            interact.StopMoveUnit();
        }
    }

    // 모바일 터치
    private void TouchMobile()
    {
        if (touch.phase == TouchPhase.Began)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector3.forward, Mathf.Infinity);

            // 마우스 포인터에 유닛이 있을 시(현재는 땅까지 적용)
            if (hit)
            {
                if (hit.transform.CompareTag("Unit"))
                {
                    interact.MoveUnit(hit.transform);
                }
                else if (hit.transform.CompareTag("Ground"))
                {
                    TouchTheGround();
                }
                else
                { }
            }
        }

        else if (touch.phase == TouchPhase.Ended)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector3.forward, Mathf.Infinity);

            if (hit)
            {
                if (hit.transform.CompareTag("Unit"))
                {
                    interact.FuseUnit(hit.transform);
                }
            }

            interact.StopMoveUnit();
        }
    }

    private void TouchTheGround()
    {
        statCalculator.GetCoin();
        uiController.UpdateCoin(userStat.Coin);
        uiController.TouchEffect(userStat.Point);

        spawnUnit.AutoSpawn();
    }
}
