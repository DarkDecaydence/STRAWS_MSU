﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Source: http://wiki.unity3d.com/index.php?title=MouseOrbitZoom
/// </summary>
public class CameraController : MonoBehaviour {

    public Transform target;
    public Vector3 targetOffset;
    public float distance = 5.0f;
    public float maxDistance = 20;
    public float minDistance = 0.6f;
    public float xSpeed = 200.0f;
    public float ySpeed = 200.0f;
    public int yMinLimit = -80;
    public int yMaxLimit = 80;
    public int zoomRate = 40;
    public float panSpeed = 0.3f;
    public float zoomDampening = 5.0f;

    private float xDeg = 0.0f;
    private float yDeg = 0.0f;
    private float currentDistance;
    private float desiredDistance;
    private Quaternion currentRotation;
    private Quaternion desiredRotation;
    private Quaternion rotation;
    private Vector3 position;

    //private Camera MainCamera;

	// Use this for initialization
	void Start () {
        Init();
        //MainCamera = Camera.main;
	}
    
    void OnEnable()
    {
        Init();
    }
	
	// Update is called once per frame
	//void Update () {
 //       var altModifier = Input.GetKey(KeyCode.LeftAlt);
 //       var ctrlModifier = Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl);

	//    if (Input.GetMouseButton(0))
 //       {
 //           if (altModifier)
 //           {
 //               RaycastHit hit;
 //               var mousePoint = MainCamera.ScreenPointToRay(Input.mousePosition);
 //               if (Physics.Raycast(mousePoint, out hit))
 //               {
 //                   MainCamera.transform.RotateAround(hit.point, new Vector3(0, 1, 0), Input.GetAxis("Mouse X") * 4);
 //                   MainCamera.transform.RotateAround(hit.point, new Vector3(1, 0, 0), Input.GetAxis("Mouse Y") * 4);
 //               }
 //           }
 //           if (ctrlModifier)
 //           {
 //               MainCamera.transform.Rotate(new Vector3(0, 1, 0), Input.GetAxis("Mouse X"));
 //               MainCamera.transform.Rotate(new Vector3(1, 0, 0), Input.GetAxis("Mouse Y"));
 //           }
 //       }
 //       MainCamera.transform.position += MainCamera.transform.forward * Input.GetAxis("Mouse ScrollWheel") * 10;
 //   }

    public void Init()
    {
        if (!target)
        {
            GameObject go = new GameObject("Cam Target");
            go.transform.position = transform.position + (transform.forward * distance);
            target = go.transform;
        }

        distance = Vector3.Distance(transform.position, target.position);
        currentDistance = distance;
        desiredDistance = distance;

        position = transform.position;
        rotation = transform.rotation;
        currentRotation = transform.rotation;
        desiredRotation = transform.rotation;

        xDeg = Vector3.Angle(Vector3.right, transform.right);
        yDeg = Vector3.Angle(Vector3.up, transform.up);
    }

    void LateUpdate()
    {
        if (Input.GetMouseButton(1) && Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftAlt))
        {
            desiredDistance -= Input.GetAxis("Mouse Y") * Time.deltaTime * (zoomRate * 0.125f) * Mathf.Abs(desiredDistance);
        }
        else if (Input.GetMouseButton(1) && Input.GetKey(KeyCode.LeftAlt))
        {
            xDeg += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            yDeg -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            yDeg = ClampAngle(yDeg, yMinLimit, yMaxLimit);
            desiredRotation = Quaternion.Euler(yDeg, xDeg, 0);
            currentRotation = transform.rotation;

            rotation = Quaternion.Lerp(currentRotation, desiredRotation, Time.deltaTime * zoomDampening);
            transform.rotation = rotation;
        }
        else if (Input.GetMouseButton(1))
        {
            target.rotation = transform.rotation;
            target.Translate(Vector3.right * -Input.GetAxis("Mouse X") * panSpeed);
            target.Translate(Vector3.up * -Input.GetAxis("Mouse Y") * panSpeed, Space.World);
        }

        desiredDistance -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * zoomRate * Mathf.Abs(desiredDistance);
        desiredDistance = Mathf.Clamp(desiredDistance, minDistance, maxDistance);
        currentDistance = Mathf.Lerp(currentDistance, desiredDistance, Time.deltaTime * zoomDampening);

        position = target.position - (rotation * Vector3.forward * currentDistance + targetOffset);
        transform.position = position;
    }

    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}
