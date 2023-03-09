using UnityEngine;

[CreateAssetMenu(fileName = "Unit Data Level", menuName = "ScriptableObjects/UnitData")]
public class UnitTable : ScriptableObject
{
    public int Level; // 유닛 테이블 레벨
    public int Point; // 클릭당 코인
    public int TimeToFuse; // 합성하기 위한 필요 클릭 회수 
    public int TimeToSpawnNew; // 새로운 유닛 생성에 필요한 클릭 횟수
}
