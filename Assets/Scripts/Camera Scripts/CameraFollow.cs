using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    GameObject target;
    //super simple inter-script communication. Why did no one teach me it this way?? Is it shitty? It makes these coupled - do some research.
    PlayerMovement pm;
    Camera cam;

    float lookAheadSpeed = 2;

    bool followPlayer = true;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        pm = target.GetComponent<PlayerMovement>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.F))
        {
            followPlayer = false;
            pm.SetMoving(false);
        }
        else
        {
            followPlayer = true;
        }

        if (followPlayer)
        {
            CameraFollowPlayer();
        }
        else
        {
            LookAhead();
        }
    }

    void SetFollowPlayer(bool val)
    {
        followPlayer = true;
    }

    void CameraFollowPlayer()
    {
        Vector3 newPos = new Vector3(target.transform.position.x, target.transform.position.y, this.transform.position.z);
        this.transform.position = newPos;
        
    }

    void LookAhead()
    {
        Vector3 camPos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
        camPos.z = -10;

        Vector3 dir = camPos -= this.transform.position;
        
        //fix this. I want it to stop with player still in frame. Also get component call here is shit. 
        if(target.GetComponentInChildren<SpriteRenderer>().isVisible == true)
        {
            transform.Translate(dir * lookAheadSpeed * Time.deltaTime);
        }
    }
}
