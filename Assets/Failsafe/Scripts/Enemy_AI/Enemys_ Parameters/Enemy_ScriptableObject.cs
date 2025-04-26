using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy", order = 1)]
public class Enemy_ScriptableObject : ScriptableObject
{
    [Header("Enemy Parameters")]
    public string enemyName; // ��� �����
    public float accelaration = 120; // ��������� �����, ��� ������ �� ����� ������� ������������ �������� ��������� ����� � �������
    public int angelarSpeed = 120; // ���� �������� �����, ��� ������ �� ����� ����������� ��������� ������ � �������
    public float stoppingDistance = 0.5f; // ��������� ��������� �����

    [Header("Enemy Chase")]
    public float enemyChaseSpeed = 6f; // �������� �������������
    public float enemyLostPlayerTime = 5f; // ����� ������ ������
    [Header("Enemy Patroling")]
    public float enemyPatrolingSpeed = 4f; // �������� ��������������
    public float enemyPatrolingWaitTime = 2f; // ����� �������� ��� ��������������
    [Header("Enemy Searching")]
    public float enemySearchingSpeed = 3f; // �������� ������
    public float enemySearchingDuration = 2f; // ����� �������� ��� ������
    public float enemySearchRadius = 5f; // ������ ������
    public float offsetSearchingPoint = 10f; // ������ ������� ������
    [Header("Enemy Health")]
    public float enemyHealth = 100f; // �������� �����
    [Header("Enemy Damage")]
    public float enemyDamage = 10f; // ���� �����
    [Header("Enemy Attack Range")]
    public bool isRangeAttack = true; // ����, �����������, �������� �� ����� �������� ���
    public float enemyAttackRange = 2f; // ��������� ����� �����
    [Header("Enemy Attack Speed")]
    public float enemyAttackSpeed = 1f; // �������� ����� �����


}
