using Failsafe.Player.View;
using Failsafe.PlayerMovements;
using Failsafe.PlayerMovements.States;
using System;
using UnityEngine;
using VContainer.Unity;

namespace Failsafe.Player
{
    public class PlayerAnimationController : IInitializable, ITickable, IDisposable
    {
        private readonly PlayerController _playerController;
        private readonly Animator _animator;
        private readonly Transform _payerTransform;

        private float _movementDumpTime = 0.2f;
        private int _xMovementId = Animator.StringToHash("XMovement");
        private int _zMovementId = Animator.StringToHash("ZMovement");
        private int _standingId = Animator.StringToHash("Standing");
        private int _walkingId = Animator.StringToHash("Walking");
        private int _runningId = Animator.StringToHash("Running");
        private int _crouchingId = Animator.StringToHash("Crouching");
        private int _fallingId = Animator.StringToHash("Falling");
        private int _disabledId = Animator.StringToHash("Disabled");
        private int _jumpId = Animator.StringToHash("Jump");


        public PlayerAnimationController(PlayerController playerController, PlayerView playerView)
        {
            _playerController = playerController;
            _animator = playerView.Animator;
            _payerTransform = playerView.transform;
        }

        public void Tick()
        {
            var playerVelocity = _payerTransform.InverseTransformVector(_playerController.PlayerMovementController.Velocity);
            if (playerVelocity.Equals(Vector3.zero))
            {
                _animator.SetFloat(_xMovementId, 0, _movementDumpTime, Time.deltaTime);
                _animator.SetFloat(_zMovementId, 0, _movementDumpTime, Time.deltaTime);
            }
            else
            {
                playerVelocity.Normalize();
                _animator.SetFloat(_xMovementId, playerVelocity.x, _movementDumpTime, Time.deltaTime);
                _animator.SetFloat(_zMovementId, playerVelocity.z, _movementDumpTime, Time.deltaTime);
            }

            _animator.SetBool(_standingId, _playerController.StateMachine.CurrentState is StandingState);
            _animator.SetBool(_walkingId, _playerController.StateMachine.CurrentState is WalkState);
            _animator.SetBool(_runningId, _playerController.StateMachine.CurrentState is SprintState);
            _animator.SetBool(_crouchingId, _playerController.StateMachine.CurrentState is CrouchState || _playerController.StateMachine.CurrentState is CrouchIdle);
            _animator.SetBool(_fallingId, _playerController.StateMachine.CurrentState is FallState);
        }

        public void Initialize()
        {
            _playerController.StateMachine.GetState<JumpState>().OnEnter += OnStartJumping;
        }

        public void Dispose()
        {
            _playerController.StateMachine.GetState<JumpState>().OnEnter -= OnStartJumping;
        }

        public void OnStartJumping()
        {
            _animator.SetTrigger(_jumpId);
        }
    }
}
