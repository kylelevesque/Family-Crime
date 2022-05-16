using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotateEffect : MonoBehaviour
{
    GameObject player;
    PlayerMovement pm;

    float mod = 0.12f;
    float storeMod;
    float zVal = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pm = player.GetComponent<PlayerMovement>();
        storeMod = mod;
    }


    //making camera wiggle right now. Why? 
    // Update is called once per frame
    void Update()
    {
        if (pm.moving == true && Input.GetKey(KeyCode.LeftShift))
        {
            Vector3 rot = new Vector3(0, 0, zVal);
            this.transform.eulerAngles = rot;

            zVal += mod;
            
            if(transform.eulerAngles.z >= 5.0f && transform.eulerAngles.z < 10.0f)
            {
                mod = -0.1f;
            }
            else if (transform.eulerAngles.z < 355.0f && transform.eulerAngles.z > 350.0f)
            {
                mod = storeMod;
            }
        }
        else if(!Input.GetKey(KeyCode.LeftShift))
        {
            Vector3 rot = new Vector3(0, 0, 0);
            this.transform.eulerAngles = rot;
        }
    }
}
