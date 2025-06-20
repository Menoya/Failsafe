using Failsafe.PlayerMovements.Controllers;
using UnityEngine;

namespace Failsafe.PlayerMovements.States
{
    /// <summary>
    /// Падение
    /// </summary>
    public class FallState : BehaviorState
    {
        private InputHandler _inputHandler;
        private CharacterController _characterController;
        private readonly PlayerMovementController _movementController;
        private readonly PlayerMovementParameters _movementParameters;
        private readonly PlayerNoiseController _playerNoiseController;
        private readonly PlayerMovementSpeedModifier _playerMovementSpeedModifier;

        //Если не задавать дополнительную силу падения то контроллер не приземляется
        private float _fallSpeed = 0.1f;
        private float _fallProgress = 0;
        private Vector3 _initialVelocity;
        private Vector3 _initialPosition;

        public float FallHeight => _initialPosition.y - _characterController.transform.position.y;

        public FallState(InputHandler inputHandler, CharacterController characterController, PlayerMovementController movementController, PlayerMovementParameters movementParameters, PlayerNoiseController playerNoiseController, PlayerMovementSpeedModifier playerMovementSpeedModifier)
        {
            _inputHandler = inputHandler;
            _characterController = characterController;
            _movementController = movementController;
            _movementParameters = movementParameters;
            _playerNoiseController = playerNoiseController;
            _playerMovementSpeedModifier = playerMovementSpeedModifier;
        }

        public override void Enter()
        {
            Debug.Log("Enter " + nameof(FallState));
            _fallProgress = 0;
            _initialVelocity = new Vector3(_movementController.Velocity.x, -_fallSpeed, _movementController.Velocity.z);
            _initialPosition = _characterController.transform.position;
        }

        public override void Update()
        {
            _fallProgress += Time.deltaTime;

            var airMovement = _movementController.GetRelativeMovement(_inputHandler.MovementInput) * _movementParameters.AirMovementSpeed;

            _movementController.Move(_initialVelocity + airMovement);
        }

        public override void Exit()
        {
            if (FallHeight > _movementParameters.FallDistanceToSlow)
            {
                _playerMovementSpeedModifier.ApplySlowOnLanding();
            }
            _playerNoiseController.CreateNoise(FallHeight, 2);
        }
    }
}
