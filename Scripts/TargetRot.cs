using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRot : MonoBehaviour
{




    public Transform target;
    public FPCSwimmer.FPCSwimmer_2 fpc_2;

    float up;

    void Start()
    {
        
    }


    //private void FixedUpdate()
    //{
    //    float x = Input.GetAxis("Horizontal");
    //    float z = Input.GetAxis("Vertical");

    //    Debug.Log("x :" + x + " z :"+ z);
    //    //transform.RotateAround(target.transform.position, Vector3.up, 20 * Time.deltaTime);
    //    if (x != 0)
    //        transform.RotateAround(target.transform.position, new Vector3(0, x, 0), 20 * Time.deltaTime);
    //    else if(z != 0)
    //    {
    //        transform.position = new Vector3(target.position.x - 0.5f, target.position.y, target.position.z - 6f);
    //        transform.LookAt(target);
    //    }

    //}

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        //float z = Input.GetAxis("Vertical");

        //Debug.Log("x :" + z + " z :" + -z);

        //transform.RotateAround(target.transform.position, Vector3.up, 20 * Time.deltaTime);
        if (x != 0)
            transform.RotateAround(target.transform.position, new Vector3(0, -x, 0), 20 * Time.deltaTime);
        //else if(fpc_2.z != 0)
        //    transform.position = new Vector3(target.position.x - 0.5f, target.position.y, target.position.z - 6f);

        //else if (z != 0 && (x != 0 || x == 0))
        //{
        //    Debug.Log("----");
        //    transform.position = new Vector3(target.position.x - 0.5f, target.position.y, target.position.z - 6f);
        //}
        //transform.LookAt(target);


        float xx = Input.GetAxisRaw("Vertical");
        //transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
        transform.Translate(0, 0, xx * Time.deltaTime);


        if (Input.GetKey(KeyCode.E))
        {
            up = 0.2f * Time.deltaTime * 4;
            //transform.Rotate(0, up, 0);
            transform.Translate(0, up, 0);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            up = 0.2f * Time.deltaTime * 4;
            //transform.Rotate(0, -up, 0);
            transform.Translate(0, -up, 0);
        }

    }
}
