using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackUIToTarget : MonoBehaviour
{
    public Transform target;

    private void Update()
    {
        Vector3 wantedPos = Camera.main.WorldToScreenPoint(target.position);
        transform.position = wantedPos;
    }
}
