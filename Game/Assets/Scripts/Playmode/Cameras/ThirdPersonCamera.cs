using System;
using UnityEngine;

namespace Game
{
    public class ThirdPersonCamera : MonoBehaviour
    {
        [Tooltip("The player to follow.")] [SerializeField]
        private Transform targetTransform;

        [Tooltip("The distance to keep between the player and the camera.")] [SerializeField]
        private float cameraOffsetFromPlayer = 5;

        [Tooltip("The translation speed for the camera.")] [SerializeField]
        private float cameraTranslationSpeed = 10f;
        
        [Tooltip("The rotation speed for the camera.")] [SerializeField]
        private float cameraRotationSpeed = 10f;

        [Header("Limits")]
        [Tooltip("The minimum rotation in degrees from the camera and the up of its follow target.")]
        [SerializeField]
        private float minUpRotation = 90;

        [Tooltip("The minimum rotation in degrees from the camera and the up of its follow target.")] [SerializeField]
        private float maxUpRotation = 150;

        [Tooltip("The offset of the camera \"LookAt\" point.")] [SerializeField]
        private Vector3 lookAtOffset = new Vector3(0, 2, 0);

        private Vector2 rightJoysticDirection;

        private void Awake()
        {
            VerifyComponents();
        }

        private void VerifyComponents()
        {
            if (targetTransform == null)
            {
                throw new NullReferenceException(nameof(targetTransform) + "can't be null.");
            }
        }

        private void UpdateCameraRotation()
        {
            Vector3 targetVector3 = targetTransform.position - transform.position;
            float targetUpAngle = Vector3.Angle(targetVector3, targetTransform.up);

            if (targetUpAngle >= minUpRotation && targetUpAngle <= maxUpRotation)
            {
                transform.Translate(rightJoysticDirection * cameraRotationSpeed * Time.deltaTime);
            }
            else
            {
                if (targetUpAngle < minUpRotation)
                {
                    if (rightJoysticDirection.y >= 0)
                    {
                        transform.Translate(rightJoysticDirection * cameraRotationSpeed * Time.deltaTime);
                    }
                    else
                    {
                        Vector2 newDirection = new Vector2(rightJoysticDirection.x, 0);
                        transform.Translate(newDirection * cameraRotationSpeed * Time.deltaTime);
                    }
                }
                else if (targetUpAngle > maxUpRotation)
                {
                    if (rightJoysticDirection.y < 0)
                    {
                        transform.Translate(rightJoysticDirection * cameraRotationSpeed * Time.deltaTime);
                    }
                    else
                    {
                        Vector2 newDirection = new Vector2(rightJoysticDirection.x, 0);
                        transform.Translate(newDirection * cameraRotationSpeed * Time.deltaTime);
                    }
                }
            }
        }

        private void MoveCamera()
        {
            Vector3 targetVector = targetTransform.position - transform.position;

            targetVector.y = 0;

            if (Vector3.Distance(new Vector3(targetTransform.position.x, 0, targetTransform.position.z),
                    new Vector3(transform.position.x, 0, transform.position.z)) >= cameraOffsetFromPlayer)
            {
                transform.Translate(targetVector * cameraTranslationSpeed * Time.deltaTime, Space.World);
                Debug.Log("Translate");
            }
        }

        private void Update()
        {
            //transform.LookAt(targetTransform);
            transform.LookAt(new Vector3(targetTransform.position.x + lookAtOffset.x,
                targetTransform.position.y + lookAtOffset.y, targetTransform.position.z + lookAtOffset.z));

            UpdateCameraRotation();
            
            MoveCamera();
        }

        public void SetTargetTransform(Transform newTarget)
        {
            targetTransform = newTarget;
        }

        public void UpdateMovingDirection(Vector2 direction)
        {
            rightJoysticDirection = direction;
        }
    }
}