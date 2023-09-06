using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRot : MonoBehaviour
{
    public float mouseSensitivity;
    private Transform parent;
    public Transform playerSpine;
    Vector3 locRot;
    float mouseX;
    float mouseY;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        locRot.x = locRot.x - mouseY;
        locRot.x = Mathf.Clamp(locRot.x, -50, 40);
        locRot.y = transform.rotation.eulerAngles.y;
        locRot.z = 0;

        // Apply rotation to the player's spine component
        if (playerSpine != null)
        {
            Vector3 spineRot = playerSpine.localRotation.eulerAngles;
            spineRot.x = locRot.x;
            playerSpine.localRotation = Quaternion.Euler(spineRot);
        }
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(locRot);
        parent.Rotate(Vector3.up, mouseX);

       
    }
}
