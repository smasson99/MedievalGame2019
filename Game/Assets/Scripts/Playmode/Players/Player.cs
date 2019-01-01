using System;
using UnityEngine;

namespace Game
{
    public class Player : MonoBehaviour
    {
        private XboxOneControllerInput xboxOneControllerInput;
        private ThirdPersonCamera thirdPersonCamera;

        private Vector2 leftJoysticDirection;

        [Tooltip("The rotation speed of the player.")] [SerializeField]
        private float rotationSpeed = 3.5f;

        private void Awake()
        {
            InitializeComponents();
            InitializeValues();
            VerifyValues();
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

        private void InitializeValues()
        {
            leftJoysticDirection = Vector2.zero;
        }

        private void VerifyValues()
        {
            if (rotationSpeed <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(rotationSpeed) + " must be superior to zero.");
            }
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
            xboxOneControllerInput.OnLeftJoystickDirectionChanged += UpdateLeftJoysticDirection;
            xboxOneControllerInput.OnRightJoystickDirectionChanged += UpdateRightJoystickDirection;
        }

        private void UnSubscribeToXBoxOneControllerInputs()
        {
            xboxOneControllerInput.OnLeftJoystickDirectionChanged -= UpdateLeftJoysticDirection;
            xboxOneControllerInput.OnRightJoystickDirectionChanged -= UpdateRightJoystickDirection;
        }

        private void UpdateLeftJoysticDirection(Vector2 direction)
        {
            leftJoysticDirection = direction;
        }

        private void UpdateRightJoystickDirection(Vector2 direction)
        {
            thirdPersonCamera.UpdateMovingDirection(direction);
        }

        private bool LeftJoysticIsUsed()
        {
            return leftJoysticDirection.x != 0 && leftJoysticDirection.y != 0;
        }

        private void Rotate()
        {
            if (LeftJoysticIsUsed())
            {
                float angle = Mathf.Atan2(leftJoysticDirection.x, leftJoysticDirection.y) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
            }
        }
        
        private void Update()
        {
            Rotate();
        }
    }
}