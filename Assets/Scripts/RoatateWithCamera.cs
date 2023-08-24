using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoatateWithCamera : MonoBehaviour
{
    public Transform playerHand;
    public Transform mainCamera;


    void Update()
    {
        // Calculate the rotation offset needed to align the hand with the camera
        //Quaternion rotationOffset = Quaternion.Euler(offsetX, offsetY, offsetZ);  // Adjust these values as needed

        // Set the hand's rotation to match the camera's rotation with the offset
       // playerHand.rotation = mainCamera.rotation * rotationOffset;
    }
}
