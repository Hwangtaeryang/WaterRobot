using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collder : MonoBehaviour
{
    public GameObject net;
    public GameObject breakNet;


    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("L_Hand"))
        {
            Debug.Log("접속했음");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("L_Hand"))
        {
            Debug.Log("_접속했음");
            Invoke("Net_UnShow", 1f);
        }

        if(other.CompareTag("R_Hand"))
        {
            Debug.Log("오른_접속했음");
            
            
        }
    }

    void Net_UnShow()
    {
        net.SetActive(true);
        breakNet.SetActive(false);
    }
}
