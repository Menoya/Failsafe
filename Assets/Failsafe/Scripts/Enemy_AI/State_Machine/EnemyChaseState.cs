using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : EnemyBaseState
{
    private NavMeshAgent _navMeshAgent;
    private FieldOfView _fieldOfView;
    private ZonesOfHearing _zonesOfHearing;
    private bool isChasing = false;
    float _lostPlayerTimer; // ������ ������ ������
    /// <summary>
    /// ����������� ��� ����� � ��������� �������������.
    /// </summary>
    public override void EnterState(EnemyStateMachine enemy)
    {
        InizialezeComponents(enemy);
        _navMeshAgent.speed = enemy.enemyChaseSpeed;
        isChasing = true;
        Debug.Log("Entering Chase State");
    }

    /// <summary>
    /// ����������� ��� ������ �� ��������� �������������.
    /// </summary>
    public override void ExitState(EnemyStateMachine enemy)
    {
        Debug.Log("Exiting Chase State");
    }

    /// <summary>
    /// ��������� ������ ��������� �������������.
    /// </summary>
    public override void UpdateState(EnemyStateMachine enemy)
    {
        if (_fieldOfView == null || _zonesOfHearing == null) return;

        if (_fieldOfView.canSeePlayer)
        {
            _lostPlayerTimer = 5f; // ����� �������
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }

        if (isChasing)
        {
            ChasePlayer(enemy);
        }
        else
        {
            LosePlayer(enemy);
        }
        AttackStateSwitch(enemy);
    }

    /// <summary>
    /// ������ ������������� ������.
    /// </summary>
    private void ChasePlayer(EnemyStateMachine enemy)
    {
        if (_navMeshAgent == null || enemy.player == null) return;

        if (_navMeshAgent.destination != enemy.player.transform.position)
        {
            _navMeshAgent.SetDestination(enemy.player.transform.position);
        }
    }

    /// <summary>
    /// ������ ������ ������.
    /// </summary>
    private void LosePlayer(EnemyStateMachine enemy)
    {
        if (_navMeshAgent == null) return;

        if (_lostPlayerTimer > 0)
        {
            _lostPlayerTimer -= Time.deltaTime;
            ChasePlayer(enemy); // ���������� �������� � ������
        }
        else
        {
            ResetChaseState(enemy);
        }
    }

    /// <summary>
    /// ����� ��������� �������������.
    /// </summary>
    private void ResetChaseState(EnemyStateMachine enemy)
    {
        isChasing = false;
        _lostPlayerTimer = enemy.lostPlayerTimer; // ����� �������
        enemy.afterChase = true; // ���������� ���� ����� �������������
        enemy.EnemySwitchState(enemy.searchState); // ������� � ��������� ������
    }

    private void AttackStateSwitch(EnemyStateMachine enemy)
    {
        if (_zonesOfHearing.playerNear)
        {
            enemy.EnemySwitchState(enemy.attackState);
        }
    }

    private void InizialezeComponents(EnemyStateMachine enemy)
    {
        _navMeshAgent = enemy.GetComponent<NavMeshAgent>();
        _fieldOfView = enemy.GetComponent<FieldOfView>();
        _zonesOfHearing = enemy.GetComponent<ZonesOfHearing>();
        Debug.Log("��� ����������� ���������� �������.");

        if (_navMeshAgent == null || _fieldOfView == null || _zonesOfHearing == null)
        {
            Debug.LogError("�� ������� ����� ����������� ���������� �� ������� �����.");
            return;
        }
       
        
    }
}
