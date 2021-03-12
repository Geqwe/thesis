using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spill : MonoBehaviour
{
    public int pourThreshold = 15;
    public Transform origin = null;
    public GameObject streamPrefab = null;

    bool isPouring = false;
    Stream currentStream = null;

    // Update is called once per frame
    void Update()
    {
        bool pourCheck = CalculatePourAngle() < pourThreshold;

        if(isPouring!=pourCheck)
        {
            isPouring = pourCheck;

            if(isPouring)
            {
                StartPour();
            }
            else
            {
                EndPour();
            }
        }
    }

    void StartPour()
    {
        Debug.Log("start");
        currentStream = CreateStream();
        currentStream.Begin();
    }

    void EndPour()
    {
        Debug.Log("end");
        currentStream.End();
        currentStream = null;
    }

    float CalculatePourAngle()
    {
        return transform.forward.y * Mathf.Rad2Deg;
    }
    
    Stream CreateStream()
    {
        GameObject streamObj = Instantiate(streamPrefab, origin.position, Quaternion.identity, transform);
        return streamObj.GetComponent<Stream>();
    }
}
