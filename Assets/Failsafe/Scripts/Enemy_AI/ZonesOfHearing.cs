using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonesOfHearing : MonoBehaviour
{
    public LayerMask mask;
    public float radiusNear;
    public float radiusWalk;
    public float radiusSprint;
    public bool playerNear;
    public bool playerSprint;
    public bool playerWalk;

    private FieldOfView _fieldOfView;
    private PlayerInputHandler _playerInputHandler;

    private void Awake()
    {
        _fieldOfView = GetComponent<FieldOfView>();
        if (_fieldOfView == null)
        {
            Debug.LogError("FieldOfView component not found on the object!");
            return;
        }

        radiusNear = _fieldOfView.radius;
        radiusSprint = _fieldOfView.radiusSprinting;
        radiusWalk = _fieldOfView.radiusWalking;

        if (_fieldOfView.playerRef != null)
        {
            _playerInputHandler = _fieldOfView.playerRef.GetComponent<PlayerInputHandler>();
            if (_playerInputHandler == null)
            {
                Debug.LogError("PlayerInputHandler component not found on the player reference!");
            }
        }
    }

    private void Update()
    {
        if (_fieldOfView == null || _playerInputHandler == null) return;

        // �������� ��� ����������
        playerNear = CheckHearingZone(radiusNear);
        playerSprint = CheckHearingZone(radiusSprint) && _playerInputHandler.SprintTriggered;
        playerWalk = CheckHearingZone(radiusWalk) && _playerInputHandler.MovementInput.magnitude != 0;

        // ���������� ��������� ��������� ������
        _fieldOfView.canSeePlayer = playerNear || playerSprint || playerWalk;
    }

    /// <summary>
    /// ���������, ��������� �� ����� � �������� ���� ����������.
    /// </summary>
    /// <param name="radius">������ ���� ����������.</param>
    /// <returns>���������� true, ���� ����� ��������� � ���� ����������.</returns>
    private bool CheckHearingZone(float radius)
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, mask);
        return rangeChecks.Length != 0;
    }
}
