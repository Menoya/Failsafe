using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [Header("������� ����������� ���� ������")]
    public float radius;
    public float radiusWalking;
    public float radiusSprinting;
    [Range(0, 360)]
    public float angleSprint, angleWalk, angleNear;
    public GameObject playerRef;
    public LayerMask targetMask, obstructionMask;
    public bool canSeePlayer;



    private void Update()
    {
        FieldOfViewCheck();
    }

    /// <summary>
    /// ��������� ��������� ������ � ������ �������� � �����.
    /// </summary>
    private void FieldOfViewCheck()
    {
        canSeePlayer = false;

        // �������� ��� �������
        if (CheckVisibility(radiusSprinting, angleSprint))
        {
            canSeePlayer = true;
            return;
        }

        // �������� ��� ������
        if (CheckVisibility(radiusWalking, angleWalk))
        {
            canSeePlayer = true;
            return;
        }

        // �������� ��� �������� �������
        if (CheckVisibility(radius, angleNear))
        {
            canSeePlayer = true;
        }
    }

    /// <summary>
    /// ���������, ����� �� ����� � �������� ������� � ����.
    /// </summary>
    /// <param name="radius">������ ��������.</param>
    /// <param name="angle">���� ��������.</param>
    /// <returns>���������� true, ���� ����� �����.</returns>
    private bool CheckVisibility(float radius, float angle)
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length == 0)
            return false;

        Transform target = rangeChecks[0].transform;
        Vector3 directionToTarget = (target.position - transform.position).normalized;

        if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
            {
                return true;
            }
        }

        return false;
    }
}








