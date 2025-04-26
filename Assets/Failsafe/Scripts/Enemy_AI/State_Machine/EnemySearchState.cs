using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemySearchState : EnemyBaseState
{
    private Vector3 searchPoint;
    private NavMeshAgent _navMeshAgent;
    private FieldOfView _fieldOfView;
    float _timeToGet;
    float _searchDuration;
    float _changePointTimer;

    /// <summary>
    /// ����������� ��� ����� � ��������� ������.
    /// </summary>
    public override void EnterState(EnemyStateMachine enemy)
    {
        InitializeComponents(enemy);
        ResetAllTimers(enemy);
        searchPoint = enemy.afterChase ? enemy.transform.position : enemy.player.transform.position;
        enemy.afterChase = false;
        MoveToSearchPoint();
    }

    /// <summary>
    /// ����������� ��� ������ �� ��������� ������.
    /// </summary>
    public override void ExitState(EnemyStateMachine enemy)
    {
        searchPoint = Vector3.zero;
        _navMeshAgent.SetDestination(enemy.transform.position);
        ResetAllTimers(enemy);
    }

    /// <summary>
    /// ��������� ������ ��������� ������.
    /// </summary>
    public override void UpdateState(EnemyStateMachine enemy)
    {
        Debug.Log("Updating Search State");
        enemy.LookForPlayer();
        enemy.CheckForPlayer(enemy);
        CantGetToSearchPoint(enemy);
        OnThePoint(enemy);
    }

    /// <summary>
    /// ���������� ����� � ��������� ��������������.
    /// </summary>
    private void BackToPatrol(EnemyStateMachine enemy)
    {
        Debug.Log("Going back to Patrol State");
        enemy.EnemySwitchState(enemy.patrolState);
    }

    /// <summary>
    /// ������ ������ ������.
    /// </summary>
    private void PerformSearch(EnemyStateMachine enemy)
    {
        Debug.Log("Searching for Player");

        _searchDuration -= Time.deltaTime;
        _changePointTimer -= Time.deltaTime;
        Debug.Log(_searchDuration);
        Debug.Log(_changePointTimer);
        if (_searchDuration <= 0)
        {
            BackToPatrol(enemy);
        }
        else if (_changePointTimer <= 0)
        {
            _changePointTimer = 0.5f;
            Vector3 randomSearchPoint = new Vector3(
                UnityEngine.Random.Range(searchPoint.x - enemy.searchRadius, searchPoint.x + enemy.searchRadius),
                0,
                UnityEngine.Random.Range(searchPoint.z - enemy.searchRadius, searchPoint.z + enemy.searchRadius)
            );

            if (_navMeshAgent.destination != randomSearchPoint)
            {
                _navMeshAgent.SetDestination(randomSearchPoint);
            }
        }
    }

    /// <summary>
    /// ���������� ����� � ����� ������.
    /// </summary>
    private void MoveToSearchPoint()
    {
        Debug.Log("Going to Search Point");
        _navMeshAgent.SetDestination(searchPoint);
        _navMeshAgent.speed = 8f;
    }

    /// <summary>
    /// �������������� ����������� ����������.
    /// </summary>
    private void InitializeComponents(EnemyStateMachine enemy)
    {
        _navMeshAgent = enemy.GetComponent<NavMeshAgent>();
        _fieldOfView = enemy.GetComponent<FieldOfView>();

        if (_navMeshAgent == null || _fieldOfView == null)
        {
            Debug.LogError("NavMeshAgent ��� FieldOfView �� ������� �� ������� �����!");
        }
    }

    /// <summary>
    /// ���������� ��� �������.
    /// </summary>
    private void ResetAllTimers(EnemyStateMachine enemy)
    {
        _timeToGet = enemy.timeToGet;
        _searchDuration = enemy.searchDuration;
        _changePointTimer = enemy.changePointTimer;
    }

    /// <summary>
    /// ���������, ��������� �� ���� �� ����� ������.
    /// </summary>
    private void OnThePoint(EnemyStateMachine enemy)
    {
        if (Vector3.Distance(enemy.transform.position, searchPoint) < enemy.offsetSearchinPoint)
        {
            PerformSearch(enemy);
        }
    }

    /// <summary>
    /// ���������, ����� �� ���� ��������� �� ����� ������.
    /// </summary>
    private void CantGetToSearchPoint(EnemyStateMachine enemy)
    {
        if (_navMeshAgent.velocity.magnitude < 0.1f)
        {
            _timeToGet -= Time.deltaTime;
            if (_timeToGet <= 0)
            {
                BackToPatrol(enemy);
            }
        }
    }
}
