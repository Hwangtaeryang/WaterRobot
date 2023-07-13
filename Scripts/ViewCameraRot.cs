using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewCameraRot : MonoBehaviour
{
    //public float speed = 3;
    //public GameObject rov;

    //private void FixedUpdate()
    //{
    //    transform.Rotate(0, rov.transform.localEulerAngles.y * Time.deltaTime, 0f);
    //}

    /// <summary>
    /// //////////////////////
    /// </summary>
    //public Transform playerTransform;

    //Vector3 _cameraOffset;

    //public float smoothFactor = 0.5f;
    //public bool lookAtPlayer = false;
    //public bool rotateAroundPalyer = true;
    //public float rotationSpeed;


    //private void Start()
    //{
    //    _cameraOffset = transform.position - playerTransform.position;
    //}

    //private void LateUpdate()
    //{
    //    if (rotateAroundPalyer)
    //    {
    //        Quaternion camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up);
    //        _cameraOffset = camTurnAngle * _cameraOffset;
    //    }

    //    Vector3 newPos = playerTransform.position + _cameraOffset;

    //    transform.position = Vector3.Slerp(transform.position, newPos, smoothFactor);

    //    if (lookAtPlayer || rotateAroundPalyer)
    //        transform.LookAt(playerTransform);
    //}


    /////////////////////////////////////////
    public Transform target;
    public float dist = 0.28f;
    public float height = 0.31f;
    public float dampTrace = 20f;

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position,
            target.position - (target.forward * dist) + (Vector3.up * height),
            Time.deltaTime * dampTrace);
        transform.LookAt(target.position);
    }


}
