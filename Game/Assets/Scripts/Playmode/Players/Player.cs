using System;
using UnityEngine;

namespace Game
{
    public class Player : MonoBehaviour
    {
        private XboxOneControllerInput xboxOneControllerInput;
        private ThirdPersonCamera thirdPersonCamera;

        private void Awake()
        {
            InitializeComponents();
            VerifyComponents();
        }

        private void OnEnable()
        {
            SubscribeToXBoxOneControllerInputs();
        }

        private void OnDisable()
        {
            UnSubscribeToXBoxOneControllerInputs();
        }

        private void InitializeComponents()
        {
            xboxOneControllerInput = GetComponentInChildren<XboxOneControllerInput>();
            thirdPersonCamera = Camera.main?.GetComponent<ThirdPersonCamera>();
        }

        private void VerifyComponents()
        {
            if (xboxOneControllerInput == null)
            {
                throw new NullReferenceException(nameof(xboxOneControllerInput) + " not found!");
            }
            if (thirdPersonCamera == null)
            {
                throw new NullReferenceException(nameof(thirdPersonCamera) + " not found!");
            }
        }

        private void SubscribeToXBoxOneControllerInputs()
        {
            xboxOneControllerInput.OnRightJoystickDirectionChanged += UpdateRightJoystickDirection;
        }

        private void UnSubscribeToXBoxOneControllerInputs()
        {
            xboxOneControllerInput.OnRightJoystickDirectionChanged -= UpdateRightJoystickDirection;
        }

        private void UpdateRightJoystickDirection(Vector2 direction)
        {
            thirdPersonCamera.UpdateMovingDirection(direction);
        }
    }
}
