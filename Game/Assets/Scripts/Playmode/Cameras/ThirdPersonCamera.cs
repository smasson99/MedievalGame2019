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

        [Tooltip("The rotation speed for the camera.")] [SerializeField]
        private float cameraRotationSpeed = 10f;

        [Tooltip("The minimum rotation in degrees from the camera and the up of its follow target.")] [SerializeField]
        private float minUpRotation = 90;
        
        [Tooltip("The minimum rotation in degrees from the camera and the up of its follow target.")] [SerializeField]
        private float maxUpRotation = 150;
        
        private Vector2 movingDirection;

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

        private void Update()
        {
            transform.LookAt(targetTransform);

            Vector3 targetVector3 = targetTransform.position - transform.position;
            float targetUpAngle = Vector3.Angle(targetVector3, targetTransform.up);
            
            if (targetUpAngle >= minUpRotation && targetUpAngle <= maxUpRotation)
            {
                transform.Translate(movingDirection * cameraRotationSpeed * Time.deltaTime);
            }
            else
            {
                if (targetUpAngle < minUpRotation)
                {
                    if (movingDirection.y >= 0)
                    {
                        transform.Translate(movingDirection * cameraRotationSpeed * Time.deltaTime);
                    }
                    else
                    {
                        Vector2 newDirection = new Vector2(movingDirection.x, 0);
                        transform.Translate(newDirection * cameraRotationSpeed * Time.deltaTime);
                    }
                }
                else if (targetUpAngle > maxUpRotation)
                {
                    if (movingDirection.y < 0)
                    {
                        transform.Translate(movingDirection * cameraRotationSpeed * Time.deltaTime);
                    }
                    else
                    {
                        Vector2 newDirection = new Vector2(movingDirection.x, 0);
                        transform.Translate(newDirection * cameraRotationSpeed * Time.deltaTime);
                    }
                }
            }
        }

        public void SetTargetTransform(Transform newTarget)
        {
            targetTransform = newTarget;
        }

        public void UpdateMovingDirection(Vector2 direction)
        {
            movingDirection = direction;
        }
    }
}