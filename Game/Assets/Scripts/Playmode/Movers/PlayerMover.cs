using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public void Rotate(float direction, float rotateSpeed)
    {
        transform.RotateAround(
            transform.position,
            Vector3.forward,
            (direction < 0 ? rotateSpeed : -rotateSpeed) * Time.deltaTime
        );
    }
}