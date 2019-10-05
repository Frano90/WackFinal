using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoundController : MonoBehaviour
{
    public Camera arCamera;

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount == 1)
        {
            //Debug.Log("toque");

            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = arCamera.ScreenPointToRay(touch.position);
                RaycastHit hitObject;
                if (Physics.Raycast(ray, out hitObject))
                {
                    Gruya_Anim hittedObject = hitObject.transform.GetComponent<Gruya_Anim>();
                    if (hittedObject != null)
                    {
                        hittedObject.PlayAnimation();
                    }
                }
            }
        } 
    }
}
