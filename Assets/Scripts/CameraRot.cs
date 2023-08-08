using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRot : MonoBehaviour
{
    public float mouseSensitivity;
    private Transform parent;
    Vector3 locRot;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        locRot.x = locRot.x - mouseY;
        locRot.x = Mathf.Clamp(locRot.x, -20, 60);
        locRot.y = transform.rotation.eulerAngles.y;
        locRot.z = 0;

        transform.rotation = Quaternion.Euler(locRot);

        parent.Rotate(Vector3.up, mouseX);
        
    }
}
