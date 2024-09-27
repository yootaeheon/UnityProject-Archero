using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] Vector3 offset;
   
    private void LateUpdate()
    {
       transform.position = playerTransform.position + offset;
        transform.LookAt(playerTransform.position);
    }
}
