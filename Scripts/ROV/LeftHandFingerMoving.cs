using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHandFingerMoving : MonoBehaviour
{
    [Header("LeftHand")]
    public Transform bigCogwellLeft;    //큰 톱니
    public Transform sideCogwellUp; //사이드 바 위
    public Transform sideCogwellDown;   //사이드 바 아래
    public Transform bigholdLeft;   //큰 톱니 나사
    public Transform sideholdLeft;  //사이드 나사

    [Header("RightHand")]
    public Transform bigCogwellRight;    //큰 톱니
    public Transform bigholdLefRightt;   //큰 톱니 나사
    public Transform sideholdLeftRight;  //사이드 나사

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
        //Debug.Log(Mathf.Abs(palmStartPos - palmLastPos));

        //손이 립모션 위에서 있을 경우 집개 움직인다.(0이면 움직이지 않음, 립모션 위에서 손이 나간 상태)
        if (Mathf.Abs(palmStartPos - palmLastPos) != 0f)
        {
            palmStartPos = palmLastPos;

            ///inspector에서 rotation값 들고 오는데 360로 넘는걸 있는 그대로 들고 오기 위한 변환 
            //Debug.Log(UnityEditor.TransformUtils.GetInspectorRotation(finger.transform).x);


            if (UnityEditor.TransformUtils.GetInspectorRotation(bigCogwellLeft.transform).z <= -20.319f
            && UnityEditor.TransformUtils.GetInspectorRotation(finger.transform).x > 0f)//&& Input.GetKey(KeyCode.Alpha1) && 
            {
                bigCogwellLeft.Rotate(Vector3.forward * 0.3f);
                sideholdLeft.Rotate(Vector3.back * 0.3f);
                bigholdLeft.Rotate(Vector3.forward * 0.3f);

                bigCogwellRight.Rotate(Vector3.back * 0.3f);
                sideholdLeftRight.Rotate(Vector3.back * 0.3f);
                bigholdLefRightt.Rotate(Vector3.forward * 0.3f);
                //Debug.Log(bigCogwellLeft.rotation.z + " right " + bigCogwellRight.rotation.z);
            }
            else if (UnityEditor.TransformUtils.GetInspectorRotation(bigCogwellLeft.transform).z >= -51.820f
                && UnityEditor.TransformUtils.GetInspectorRotation(finger.transform).x < 0f)// && Input.GetKey(KeyCode.Alpha2))
            {
                bigCogwellLeft.Rotate(Vector3.back * 0.3f);
                sideholdLeft.Rotate(Vector3.forward * 0.3f);
                bigholdLeft.Rotate(Vector3.back * 0.3f);

                bigCogwellRight.Rotate(Vector3.forward * 0.3f);
                sideholdLeftRight.Rotate(Vector3.forward * 0.3f);
                bigholdLefRightt.Rotate(Vector3.back * 0.3f);
            }
        }
        else
            palmStartPos = palmLastPos;
        //Debug.Log(bigCogwellLeft.rotation.z + " right " + bigCogwellRight.rotation.z);
        //Debug.Log(finger.localEulerAngles.x);
        //Debug.Log(UnityEditor.TransformUtils.GetInspectorRotation(bigCogwellLeft.transform).z);

        

        //Debug.Log(bigCogwellLeft.rotation);
        //sideCogwellUp.Rotate(Vector3.back * 0.1f);
        //sideCogwellDown.Rotate(Vector3.back * 0.1f);
    }
}
