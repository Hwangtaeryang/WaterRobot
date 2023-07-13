using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightCogWheelRotation : MonoBehaviour
{
    public Transform rightCogwell;  //오른쪽 톱니

    [Header("Finger")]
    public Transform finger;    //값 받아올 대상
    public Transform palm;  //손 움직이나 안움직이나 확인


    float palmStartPos, palmLastPos;

    void Start()
    {
        palmStartPos = palm.localRotation.x;
    }

    
    void Update()
    {
        palmLastPos = palm.localRotation.x;

        //Debug.Log(UnityEditor.TransformUtils.GetInspectorRotation(finger.transform).x);

        if(UnityEditor.TransformUtils.GetInspectorRotation(finger.transform).x > 0f)
        {
            rightCogwell.Rotate(Vector3.back * 5f);
        }
        else if(UnityEditor.TransformUtils.GetInspectorRotation(finger.transform).x < 0f)
        {

        }
    }
}
