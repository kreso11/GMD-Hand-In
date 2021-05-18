using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{

    public Camera cam;
    private float camFov;
    public float zoomSpeed;

    private float mouseScrollInput;

    // Start is called before the first frame update
    void Start()
    {
        camFov = cam.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        mouseScrollInput = Input.GetAxis("Mouse ScrollWheel");

        camFov -= mouseScrollInput * zoomSpeed;
        camFov = Mathf.Clamp(camFov, 60 , 100);

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, camFov, zoomSpeed);
    }
}
