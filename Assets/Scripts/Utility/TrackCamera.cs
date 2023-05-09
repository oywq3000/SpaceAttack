using System;
using UnityEngine;
using System.Collections;

public class TrackCamera : MonoBehaviour
{
    public Transform targetObject;
    public float followTightness;
    private Vector3 _wantedPosition;


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        _wantedPosition = targetObject.position;
        _wantedPosition.z = transform.position.z;
        transform.position = _wantedPosition;
        /*
        if (Vector3.Distance(_wantedPosition,transform.position)>1)
        {
            transform.position = Vector3.Lerp(transform.position, _wantedPosition, Time.deltaTime * followTightness);
        }
        */
    }

   
}