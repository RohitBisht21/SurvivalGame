using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CameraFollowingPlayer
{


public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.position + offset;
    }
}
}