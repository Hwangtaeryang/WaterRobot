//using AQUAS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ROV_RobotCtrl : MonoBehaviour
{
    float x, z, up;
    float speed = 1f;
    float rotSpeed = 100f;
    bool seaInState;    //바다속인지 상태
    bool seaUp; //바다 위


    public float minSpeed = 1f;
    public float maxSpeed = 5f;
    float currentSpeed = 0;
    float minTurnSpeed = 10f;
    float maxTurnSpeed = 180f;



    Rigidbody rigi;
    //public AQUAS_Buoyancy aquas_buoyancy;   //수중 중력 스크립트


    private void Start()
    {
        rigi = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        TurnMove();
        SetSpeed();

        //올라가기
        if (Input.GetKey(KeyCode.E))
        {
            up = 0.2f * Time.deltaTime * rotSpeed;
            transform.Rotate(up, 0, 0);
        }

        //내려가기
        if (Input.GetKey(KeyCode.Q))
        {
            up = 0.2f * Time.deltaTime * rotSpeed;
            transform.Rotate(-up, 0, 0);
        }
    }

    void SetSpeed()
    {
        z = Input.GetAxis("Vertical");// * 10f * Time.deltaTime;
        Debug.Log(z);
        //currentSpeed = Mathf.Clamp(currentSpeed + speedInput, minSpeed, maxSpeed);

        if (z == 0f)
        {
            if (currentSpeed > 0f)
            {
                currentSpeed -= 0.4f;

                if (currentSpeed < 0)
                    currentSpeed = 0;
            }

        }
        else if (z > 0f)
        {
            if (currentSpeed <= maxSpeed)
            {
                currentSpeed += 0.05f;

                if (currentSpeed > maxSpeed)
                    currentSpeed = maxSpeed;
            }

        }

        Vector3 movement = transform.forward * currentSpeed * Time.deltaTime;
        rigi.MovePosition(rigi.position + movement);
    }


    void TurnMove()
    {
        x = Input.GetAxis("Horizontal");
        float multiplier = Mathf.Lerp(minTurnSpeed, maxTurnSpeed, 2 / maxSpeed);
        float turn = x * multiplier * Time.deltaTime;

        Quaternion turnRot = Quaternion.Euler(0f, turn, 0f);

        rigi.MoveRotation(rigi.rotation * turnRot);
    }



    //private void Update()
    //{
    //ROV_Moving();
    //RigidBody_OnOff();
    //}

    //로봇 이동
    //void ROV_Moving()
    //{
    //    x = Input.GetAxis("Horizontal") * Time.deltaTime * rotSpeed;
    //    z = Input.GetAxis("Vertical") * Time.deltaTime * speed;

    //    transform.Rotate(0, x, 0);
    //    transform.Translate(0, 0, z);


    //    //올라가기
    //    if(Input.GetKey(KeyCode.E))
    //    {
    //        up = 0.2f * Time.deltaTime * rotSpeed;
    //        transform.Rotate(up, 0, 0);
    //    }

    //    //내려가기
    //    if (Input.GetKey(KeyCode.Q))
    //    {
    //        up = 0.2f * Time.deltaTime * rotSpeed;
    //        transform.Rotate(-up, 0, 0);
    //    }
    //}

    //void RigidBody_OnOff()
    //{
    //    if (Input.GetKey(KeyCode.E) && seaInState.Equals(false))
    //    {
    //        //Debug.Log("들어옴");
    //        rigi.useGravity = false;    //중력 오프
    //        aquas_buoyancy.enabled = false; //스크립트 비활성화
    //    }
    //    else if (!Input.GetKey(KeyCode.E) && seaUp.Equals(true))
    //    {
    //        //Debug.Log("바다 위_바다 밑으로 갈껴!");
    //        seaInState = false;
    //        rigi.useGravity = true; //중력 온
    //        aquas_buoyancy.enabled = true;  //스크립트 활성화
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.CompareTag("Sea"))
    //    {
    //        Debug.Log("바다에 들어옴");
    //        seaInState = true;
    //        rigi.useGravity = false;    //중력 오프
    //        aquas_buoyancy.enabled = false; //스크립트 비활성화
    //    }

    //    if(other.CompareTag("SeaUp"))
    //    {
    //        Debug.Log("바다위");
    //        seaUp = true;
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if(other.CompareTag("Sea"))
    //    {
    //        Debug.Log("바다나옴(바다위)");
    //        seaInState = false; //바다안이 아님
    //        rigi.useGravity = true; //중력 온
    //        aquas_buoyancy.enabled = true;  //스크립트 활성화
    //    }

    //    if (other.CompareTag("SeaUp"))
    //    {
    //        Debug.Log("바다속(바다위아님)");
    //        seaUp = false;  //바다위가 아님
    //        seaInState = true;  //바다속
    //        rigi.useGravity = false;    //중력 오프
    //        aquas_buoyancy.enabled = false; //스크립트 비활성화
    //    }
    //}

}
