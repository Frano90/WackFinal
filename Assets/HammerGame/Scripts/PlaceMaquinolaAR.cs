using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using System;
using UnityEngine.EventSystems;

public class PlaceMaquinolaAR : MonoBehaviour
{
    public Camera arCamera;
    public ARSession arSession;
    public GameObject placementIndicator;
    public GameObject maquinola_prefab;
    public GameEvent onMaquinolaDeployed;
    private ARRaycastManager arOrigin;
    private ARPlaneManager arPlaneManager;
    private Pose placementPose;
    private bool placementPoseIsValid = false;
    private bool placeMode = true;

    void Start()
    {
        arOrigin = FindObjectOfType<ARRaycastManager>();
        arPlaneManager = FindObjectOfType<ARPlaneManager>();
    }


    void Update()
    {
        UpdatePlacementIndicator();
        if (placeMode == false)
            return;

        UpdatePlacementPose();
        

        if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                return;

            PlaceObject();
            onMaquinolaDeployed.Raise();
            SwitchPlaceMode();
            SwitchPlaneTrackMode();
        }
    }

    public void SwitchPlaneTrackMode()
    {
        arPlaneManager.enabled = !arPlaneManager.enabled;

        foreach (ARPlane plane in arPlaneManager.trackables)
        {
            plane.gameObject.SetActive(arPlaneManager.enabled);
        }
    }

    public void ResetSession()
    {
        arSession.Reset();
    }

    private void PlaceObject()
    {
        Instantiate(maquinola_prefab, placementPose.position, placementPose.rotation);

    }

    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            //Debug.Log(placementIndicator);
            placementIndicator.SetActive(placeMode);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(placeMode);
        }
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = arCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        arOrigin.Raycast(screenCenter, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;

            var cameraForward = arCamera.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }

    public void SwitchPlaceMode()
    {
        if (placeMode == true)
            TurnOffPlaceMode();
        else
            TurnOnPlaceMode();

    }

    private void TurnOnPlaceMode()
    {
        Debug.Log("Turn on Place mode");
        placeMode = true;
    }

    private void TurnOffPlaceMode()
    {
        Debug.Log("Turn off Place mode");
        placeMode = false;
    }
}
