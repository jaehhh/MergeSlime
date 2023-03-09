using UnityEngine;

[CreateAssetMenu(fileName = "Unit Data Level", menuName = "ScriptableObjects/UnitData")]
public class UnitTable : ScriptableObject
{
    public int Level; // ���� ���̺� ����
    public int Point; // Ŭ���� ����
    public int TimeToFuse; // �ռ��ϱ� ���� �ʿ� Ŭ�� ȸ�� 
    public int TimeToSpawnNew; // ���ο� ���� ������ �ʿ��� Ŭ�� Ƚ��
}
