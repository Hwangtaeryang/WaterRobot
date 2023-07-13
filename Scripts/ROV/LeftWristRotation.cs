using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftWristRotation : MonoBehaviour
{
    public Transform wrist;
    public Transform palm;  //값 받아올 오브젝트

    float leftHandAxisValue;    //그자리에서 움직이기 위한 변수(로봇팔)
    float palmStartPos, palmLastPos;    //움직임이 있을 때를 알기 위한 변수들 처음시작값, 나중값
    float rot, joint2;

    public float a =180f, b = -180f;

    private void Start()
    {
        palmStartPos = palm.localRotation.x;
        leftHandAxisValue = palm.localPosition.z;
    }

    void Update()
    {
        //if(Input.GetKey(KeyCode.T))
        //{
        //    wrist.Rotate(Vector3.up * 1f);
        //}

        palmLastPos = palm.localRotation.x;
        //Debug.Log(palmLastPos);

        rot = Mathf.Clamp(Mathf.DeltaAngle(0, palm.localRotation.y), -10f, 10f);
        joint2 = Mathf.Lerp(a, b, Mathf.InverseLerp(-10f, 10f, rot));
        joint2 = joint2 * 10;


        if (Mathf.Abs(palmStartPos - palmLastPos) >= 0.01f)
        {
            LeftWrist_Rotation(joint2);
            palmStartPos = palmLastPos;

        }
        else
        {
            palmStartPos = palmLastPos;
        }
    }


    void LeftWrist_Rotation(float _move)
    {
        wrist.Rotate(0f, (_move - leftHandAxisValue),0f);

        leftHandAxisValue = _move;
    }
}
