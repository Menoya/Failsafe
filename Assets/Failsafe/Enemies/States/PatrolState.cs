﻿using FMOD;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Debug = UnityEngine.Debug;
public class PatrolState : BehaviorState
{
    private readonly Sensor[] _sensors;
    private readonly EnemyController _enemyController;
    private NavMeshAgent _navMeshAgent;
    private Enemy_ScriptableObject _enemyConfig;
    private Transform _enemyPos;
    
    private List<Transform> _patrolPoints = new();
    private int _currentPatrolPointIndex = -1;
    private Vector3 _patrolPoint;

    private float _waitTimer;
    private bool _isWaiting = false;

    public PatrolState(Sensor[] sensors,Transform enemyPos, EnemyController enemyController, NavMeshAgent navMeshAgent, Enemy_ScriptableObject enemyConfig)
    {
        _sensors = sensors;
        _enemyController = enemyController;
        _navMeshAgent = navMeshAgent;
        _enemyConfig = enemyConfig;
        _enemyPos = enemyPos;

    }

    public override void Enter()
    {
        base.Enter();
        _waitTimer = _enemyConfig.PatrollingWaitTime;
        _navMeshAgent.stoppingDistance = 1f;
        _isWaiting = false;
        ChoosePatroloStyle();

    }

    private void ChoosePatroloStyle()
    {
        if (_patrolPoints == null || _patrolPoints.Count == 0)
        {
            _patrolPoints = _enemyController.GetRoomPatrolPoints();
        }

        if (_patrolPoints == null || _patrolPoints.Count == 0)
        {
            _patrolPoint = _enemyController.RandomPointAround(_enemyPos.position, _enemyConfig.offsetSearchingPoint);
            _enemyController.MoveToPoint(_patrolPoint, _enemyConfig.PatrolingSpeed);
        }
        else
        {
            _currentPatrolPointIndex = -1;
            HandlePatrolling();
        }
    }
    public override void Update()
    {

        if (_isWaiting)
        {
            HandleWaiting();
        }
        else if (_enemyController.IsPointReached() && !_isWaiting)
        {
            _isWaiting = true;
            _enemyController.StopMoving();
        }
    }

    private void HandleWaiting()
    {
        _waitTimer -= Time.deltaTime;
        if (_waitTimer <= 0f)
        {
            _waitTimer = _enemyConfig.PatrollingWaitTime;
            _isWaiting = false;
            HandlePatrolling();
        }
        _isWaiting = true;

    }

    private void HandlePatrolling()
    {
        if (_patrolPoints == null || _patrolPoints.Count == 0)
        {
            _patrolPoint = _enemyController.RandomPointAround(_enemyPos.position, _enemyConfig.offsetSearchingPoint);
        }
        else
        {
            _currentPatrolPointIndex = (_currentPatrolPointIndex + 1) % _patrolPoints.Count;
            _patrolPoint = _patrolPoints[_currentPatrolPointIndex].position;
        }

        _enemyController.MoveToPoint(_patrolPoint, _enemyConfig.PatrolingSpeed);
    }
    
    public void SetManualPatrolPoints(List<Transform> points, bool restart = true)
    {
        _patrolPoints = points ?? new List<Transform>();

        if (restart)
        {
            _currentPatrolPointIndex = -1;
            _isWaiting = false;
            _waitTimer = _enemyConfig.PatrollingWaitTime;
            HandlePatrolling();
        }
    }
}