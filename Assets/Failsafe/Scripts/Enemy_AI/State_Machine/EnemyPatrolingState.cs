using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolingState : EnemyBaseState
{
    private NavMeshAgent agent;
    private GameObject[] patrolPoints;
    private int currentPatrolPointIndex = 0;
    private float waitTimer;
    private bool isWaiting = false;

    /// <summary>
    /// ����������� ��� ����� � ��������� ��������������.
    /// </summary>
    public override void EnterState(EnemyStateMachine enemy)
    {
        agent = enemy.GetComponent<NavMeshAgent>();
        patrolPoints = enemy.patrolPoints;

        if (agent == null || patrolPoints == null || patrolPoints.Length == 0)
        {
            Debug.LogError("NavMeshAgent ��� ���������� ����� �� �������!");
            return;
        }

        agent.speed = enemy.patrolSpeed;
        waitTimer = enemy.waitTime;
        MoveToNextPatrolPoint();
    }

    /// <summary>
    /// ����������� ��� ������ �� ��������� ��������������.
    /// </summary>
    public override void ExitState(EnemyStateMachine enemy)
    {
        agent.ResetPath(); // ����� ����
        Debug.Log("Exiting Patrol State");
    }

    /// <summary>
    /// ��������� ������ ��������� ��������������.
    /// </summary>
    public override void UpdateState(EnemyStateMachine enemy)
    {
        if (isWaiting)
        {
            HandleWaiting(enemy);
        }
        else
        {
            CheckPatrolPointProximity();
        }

        enemy.LookForPlayer();
        enemy.CheckForPlayer(enemy);
    }

    /// <summary>
    /// ������������ �������� �� ���������� �����.
    /// </summary>
    private void HandleWaiting(EnemyStateMachine enemy)
    {
        waitTimer -= Time.deltaTime;
        if (waitTimer <= 0)
        {
            isWaiting = false;
            waitTimer = enemy.waitTime;
            MoveToNextPatrolPoint();
        }
    }

    /// <summary>
    /// ��������� ���������� �� ������� ���������� �����.
    /// </summary>
    private void CheckPatrolPointProximity()
    {
        if (!agent.pathPending && agent.remainingDistance < 1f)
        {
            isWaiting = true;
        }
    }

    /// <summary>
    /// ����������� ����� �� ��������� ���������� �����.
    /// </summary>
    private void MoveToNextPatrolPoint()
    {
        if (patrolPoints == null || patrolPoints.Length == 0) return;

        currentPatrolPointIndex = (currentPatrolPointIndex + 1) % patrolPoints.Length;
        agent.SetDestination(patrolPoints[currentPatrolPointIndex].transform.position);
    }
}
