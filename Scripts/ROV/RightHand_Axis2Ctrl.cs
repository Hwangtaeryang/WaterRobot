using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHand_Axis2Ctrl : MonoBehaviour
{
    public Transform rightHandAxis2; //움직일 오브젝트
    public Transform palm;  //값 받아올 오브젝트

    float rightHandAxisValue;    //그자리에서 움직이기 위한 변수(로봇팔)
    float palmStartPos, palmLastPos;    //움직임이 있을 때를 알기 위한 변수들 처음시작값, 나중값
    float rot, joint2;

    public float a = 95f, b = -40f;


    void Start()
    {
        palmStartPos = palm.localPosition.x;
        rightHandAxisValue = palm.localPosition.z;
    }

    
    void Update()
    {
        palmLastPos = palm.localPosition.x;
        //Debug.Log(palmListPos);

        rot = Mathf.Clamp(Mathf.DeltaAngle(0, palm.localPosition.x), -10f, 10f);
        joint2 = Mathf.Lerp(a, b, Mathf.InverseLerp(-10f, 10f, rot));
        joint2 = joint2 * 10;


        if (Mathf.Abs(palmStartPos - palmLastPos) >= 0.01f)
        {
            RightAxis2_Moving(joint2);
            palmStartPos = palmLastPos;

        }
        else
        {
            palmStartPos = palmLastPos;
        }
    }


    void RightAxis2_Moving(float _move)
    {
        rightHandAxis2.Rotate(0f, 0f, (_move - rightHandAxisValue));

        rightHandAxisValue = _move;
    }
}
