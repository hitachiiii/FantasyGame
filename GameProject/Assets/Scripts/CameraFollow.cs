using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float smoothing = 5f; //camera movement

    Vector3 offset; //offset between the player and the camera

    void Awake()
    {
        Assert.IsNotNull(target);
    }

    void Start()
    {
        offset = transform.position - target.position;
    }

    void Update()
    {
        Vector3 targetCameraPosition = target.position + offset; //the position that we want the camera to move to
        transform.position = Vector3.Lerp(transform.position, targetCameraPosition, smoothing * Time.deltaTime); 
    }
}
