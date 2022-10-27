using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : MonoBehaviour
{

    public int lenght;
    public LineRenderer lineRend;
    public Transform lineRendTransform;
    private Vector3[] segmentPoses;
    private Vector3[] realSegmentPoses;
    private Vector3[] segmentV;
    private float adjustment = 1f;

    public Transform targetDir;
    public float targetDist;
    public float smoothSpeed;
    
    public float wiggleSpeed;
    public float wiggleMagnitude;
    public float phaseMultiplier;
    
    // Start is called before the first frame update
    void Start()
    {
        lineRend.positionCount = lenght;
        segmentPoses = new Vector3[lenght];
        realSegmentPoses = new Vector3[lenght];
        segmentV = new Vector3[lenght];
        lineRendTransform.transform.rotation = Quaternion.Euler(0,0,-90);
    }

    // Update is called once per frame
    void Update()
    {
        segmentPoses[0] = targetDir.position;
        
        //segmentPoses[0].y = targetDir.position.y + Mathf.Sin(Time.time * wiggleSpeed) * wiggleMagnitude;
        //segmentPoses[lenght/2].y = Mathf.Sin(Time.time * wiggleSpeed) * wiggleMagnitude;
        
        //for (int i = 1; i < segmentPoses.Length/2; i++)
        for (int i = 1; i < segmentPoses.Length; i++)
        {
            segmentPoses[i] = Vector3.SmoothDamp(segmentPoses[i], segmentPoses[i - 1] + targetDir.right * targetDist, ref segmentV[i], smoothSpeed);
            //segmentPoses[i+segmentPoses.Length/2] = Vector3.SmoothDamp(segmentPoses[i+segmentPoses.Length/2], segmentPoses[i+segmentPoses.Length/2 - 1] + targetDir.right * targetDist, ref segmentV[i+segmentPoses.Length/2], smoothSpeed);
            //segmentPoses[-i+segmentPoses.Length/2] = Vector3.SmoothDamp(segmentPoses[-i+segmentPoses.Length/2], segmentPoses[-i+segmentPoses.Length/2 + 1] + targetDir.right * targetDist, ref segmentV[-i+segmentPoses.Length/2], smoothSpeed);
        }

        for (int i = segmentPoses.Length - 1; i >= 0; i--)
        {
            float value = (float)(i+1) / (float)(segmentPoses.Length);
            realSegmentPoses[i] = segmentPoses[i];
            float phase = phaseMultiplier * i;
            realSegmentPoses[i].y = segmentPoses[i].y + value * Mathf.Sin(Time.time * wiggleSpeed + phase) * wiggleMagnitude * adjustment;
        }
        lineRend.SetPositions(realSegmentPoses);

        //lineRend.SetPositions(segmentPoses);
        
        /*
        if (segmentPoses[1].x < segmentPoses[0].x)
        {
            lineRendTransform.transform.rotation = Quaternion.Euler(0,0,180);
        }
        else
        {
            lineRendTransform.transform.rotation = Quaternion.Euler(0,0,0);
        }
        */
        //lineRendTransform.transform.rotation = Quaternion.Euler(0,0,-90);
    }

    private void FixedUpdate()
    {
        
    }
    
}
