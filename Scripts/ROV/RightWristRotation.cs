using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightWristRotation : MonoBehaviour
{
    public Transform wrist;
    public Transform palm;  //값 받아올 오브젝트

    float rightHandAxisValue;    //그자리에서 움직이기 위한 변수(로봇팔)
    float palmStartPos, palmLastPos;    //움직임이 있을 때를 알기 위한 변수들 처음시작값, 나중값
    float rot, joint2;

    public float a = 180f, b = -180f;




    void Start()
    {
        palmStartPos = palm.localRotation.x;
        rightHandAxisValue = palm.localPosition.z;
    }

    // Update is called once per frame
    void Update()
    {
        palmLastPos = palm.localRotation.x;
        //Debug.Log(palmLastPos);

        rot = Mathf.Clamp(Mathf.DeltaAngle(0, palm.localRotation.y), -10f, 10f);
        joint2 = Mathf.Lerp(a, b, Mathf.InverseLerp(-10f, 10f, rot));
        joint2 = joint2 * 10;


        if (Mathf.Abs(palmStartPos - palmLastPos) >= 0.01f)
        {
            RightWrist_Rotation(joint2);
            palmStartPos = palmLastPos;

        }
        else
        {
            palmStartPos = palmLastPos;
        }
    }

    void RightWrist_Rotation(float _move)
    {
        wrist.Rotate(0f, (_move - rightHandAxisValue), 0f);

        rightHandAxisValue = _move;
    }
}
