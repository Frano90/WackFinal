using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{

    public Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        LookCamera();
    }

    private void LookCamera()
    {
        if (camera == null)
            return;

        Vector3 cameraPos = new Vector3(camera.transform.position.x, transform.position.y, camera.transform.position.z);

        transform.LookAt(cameraPos);
    }
}
