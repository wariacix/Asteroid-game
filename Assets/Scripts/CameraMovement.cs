using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform shipTransform;
    void Update()
    {
        transform.SetPositionAndRotation(new Vector3(shipTransform.position.x, shipTransform.position.y, transform.position.z), Quaternion.identity);
    }
}
