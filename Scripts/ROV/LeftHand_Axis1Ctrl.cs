using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHand_Axis1Ctrl : MonoBehaviour
{
    public Transform leftHandAxis1; //움직일 오브젝트
    public Transform palm;  //값 받아올 오브젝트

    float leftHandAxisValue;    //그자리에서 움직이기 위한 변수(로봇팔)
    float palmStartPos, palmLastPos;    //움직임이 있을 때를 알기 위한 변수들 처음시작값, 나중값
    float rot, joint1;
    public float a = -70f, b = 90f;

    void Start()
    {
        palmStartPos = palm.localPosition.y;
        leftHandAxisValue = palm.localPosition.z;
    }

    
    void Update()
    {
        palmLastPos = palm.localPosition.y;
        //Debug.Log(palmListPos);

        rot = Mathf.Clamp(Mathf.DeltaAngle(0, palm.localPosition.y), -10f, 10f);
        joint1 = Mathf.Lerp(a, b, Mathf.InverseLerp(-10f, 10f, rot));
        joint1 = joint1 * 10;

        //Debug.Log("rot "+ rot + " joint1 " + joint1);
        //Debug.Log(palmStartPos - palmLastPos);
        if(Mathf.Abs(palmStartPos - palmLastPos) >= 0.01f)
        {
            LeftAxis1_Moving(joint1);
            palmStartPos = palmLastPos;
        }
        else
        {
            palmStartPos = palmLastPos;
        }
    }

    void LeftAxis1_Moving(float _move)
    {
        leftHandAxis1.Rotate(0f, 0f, (_move - leftHandAxisValue));

        leftHandAxisValue = _move;

        
    }
}
