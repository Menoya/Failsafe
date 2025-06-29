using DMDungeonGenerator;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.AI;

public class EnemyController
{
    private readonly Transform _transform;
    private readonly NavMeshAgent _navMeshAgent;
    private RoomData _currentRoom;
    private Sensor[]  _sensors;
    private Vector3 _lastKnownPlayerPosition;
    private Vector3 _lastKnownPlayerDirection;
    public EnemyController(Sensor[] sensors, Transform transform, NavMeshAgent navMeshAgent)
    {
        _transform = transform;
        _navMeshAgent = navMeshAgent;
        _sensors = sensors;
        _navMeshAgent.updatePosition = false;
        _navMeshAgent.updateRotation = false;
    }

    public void MoveToPoint(Vector3 point, float speed)
    {
        _navMeshAgent.isStopped = false;
        _navMeshAgent.speed = speed;
        _navMeshAgent.SetDestination(point);
    }


    public void RunToPoint(Vector3 point, float speed)
    {
        _navMeshAgent.isStopped = false;
        _navMeshAgent.speed = speed;
        _navMeshAgent.SetDestination(point);
    }

    public void StopMoving()
    {
        _navMeshAgent.isStopped = true;
        _navMeshAgent.speed = 0f;
    }

    public void ResumeMoving()
    {
        _navMeshAgent.isStopped = false;
    }

    public void SetCurrentRoom(RoomData room)
    {
        _currentRoom = room;
    }

    public RoomData CurrentRoom => _currentRoom;

    public List<Transform> GetRoomPatrolPoints()
    {
        if (_currentRoom == null)
        {
            Debug.LogWarning("CurrentRoom is NULL");
            return new List<Transform>();
        }

        if (_currentRoom.PatrolPoints == null || _currentRoom.PatrolPoints.Count == 0)
        {
            Debug.LogWarning($"Комната {_currentRoom.name} не содержит PatrolPoints — AutoCollectPatrolPoints?");
        }

        return _currentRoom.GetPatrolPoints();
    }
 
    public bool IsPointReached()
    {
        if (Vector3.Distance(_navMeshAgent.destination, _transform.position) <= _navMeshAgent.stoppingDistance)
        {
            if (!_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude < 0.05f)
            {
                StopMoving();
                return true;
            }
        }
        return false;
    }

    public Vector3 RandomPointAround(Vector3 center, float radius)
    {
        Vector2 circle = UnityEngine.Random.insideUnitCircle * radius;
        Vector3 randomPoint = new Vector3(center.x + circle.x, center.y, center.z + circle.y);

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 2f, NavMesh.AllAreas))
        {
            return hit.position;
        }

        return center; // fallback
    }
    
    public Vector3 RandomPointInForwardCone(Vector3 center, Vector3 forward, float radius, float angle = 90f)
    {
        Vector2 circle = UnityEngine.Random.insideUnitCircle * radius;
        Vector3 randomOffset = new Vector3(circle.x, 0, circle.y);

        // Направление смещения по конусу
        Quaternion rotation = Quaternion.AngleAxis(Random.Range(-angle / 2f, angle / 2f), Vector3.up);
        Vector3 direction = rotation * forward.normalized;
        Vector3 randomPoint = center + direction * Random.Range(radius * 0.5f, radius);

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 2f, NavMesh.AllAreas))
        {
            return hit.position;
        }

        return center; // fallback
    }

    public void RotateToPoint(Vector3 targetPoint, float rotationSpeed = 5f)
    {
        Vector3 direction = targetPoint - _transform.position;
        direction.y = 0f; // Игнорируем вертикаль (Y)

        if (direction.sqrMagnitude < 0.001f)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
    
    public void SetLastKnownPlayerPosition(Vector3 position, Vector3 direction)
    {
        _lastKnownPlayerPosition = position;
        _lastKnownPlayerDirection = direction.normalized;
    }
    
    public Vector3 LastKnownPlayerPosition => _lastKnownPlayerPosition;
    public Vector3 LastKnownPlayerDirection => _lastKnownPlayerDirection;
    
    public void SetVisionSensorParams(float distance, float angle, float focusTime)
    {
        foreach (var sensor in _sensors)
        {
            if (sensor is VisualSensor visualSensor)
            {
                visualSensor.SetDistance(distance);
                visualSensor.SetAngle(angle);
                visualSensor.SetFocusingTime(focusTime);
            }
        }
    }

    public void SetHearingSensorParams(float distance, float minSignal, float maxSignal, float focusTime)
    {
        foreach (var sensor in _sensors)
        {
            if (sensor is NoiseSensor noiseSensor)
            {
                noiseSensor.SetDistance(distance);
                noiseSensor.SetMinMaxStrength(minSignal, maxSignal);
                noiseSensor.SetFocusingTime(focusTime);
            }
        }
    }
}