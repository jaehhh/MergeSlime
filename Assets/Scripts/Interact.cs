using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

// 조작에 따른 상호작용
public class Interact : MonoBehaviour
{
    [SerializeField]
    private SpawnUnit spawnUnit;
    [SerializeField]
    private UIController uiController;

    private Transform target; // 드래그로 움직이고 있는 유닛
    private Transform nextTarget; // 합쳐질 대상 유닛

    public void MoveUnit(Transform unit)
    {
        target = unit;
        StartCoroutine("MoveUnitCoroutine");
    }

    public void StopMoveUnit()
    {
        if (target == null) return;

        StopCoroutine("MoveUnitCoroutine");

        target.GetComponent<CircleCollider2D>().enabled = true;
        // y위치에 따라 레이어 정리
        target.GetComponent<SortingGroup>().sortingOrder = (int)(target.transform.position.y * 1000) * -1;

        target = null;
    }

    private IEnumerator MoveUnitCoroutine()
    {
        target.GetComponent<CircleCollider2D>().enabled = false;
        // y위치에 따라 레이어 정리
        target.GetComponent<SortingGroup>().sortingOrder = 30000;

        Vector2 mousePosition = Vector2.zero;

        while (target != null)
        {
#if UNITY_ANDROID || UNITY_IPHONE
            mousePosition  = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
#else
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
#endif

            target.position = mousePosition;

            yield return null;
        }
    }

    public void FuseUnit(Transform nextUnit)
    {
        if (target == null) return;

        nextTarget = nextUnit;

        Unit targetUnit = target.GetComponent<Unit>();
        Unit nextTargetUnit = nextTarget.GetComponent<Unit>();

        // 두 유닛이 같은 레벨일 때
        if (targetUnit.level == nextTargetUnit.level)
        {
            // 합성에 필요한 필요 터치 회수를 달성하지 못했을 때
            if(targetUnit.timeToFuse > targetUnit.touchTime ||
            nextTargetUnit.timeToFuse > nextTargetUnit.touchTime)
            {
                return;
            }

            int level = target.GetComponent<Unit>().level;

            spawnUnit.Spawn(level, true);

            Destroy(target.gameObject);
            Destroy(nextTarget.gameObject);

            uiController.Sound(ClickSound.fuse);

            target = null;
            nextTarget = null;
        }
    }
}
