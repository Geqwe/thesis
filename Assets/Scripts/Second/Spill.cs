using UnityEngine;

public class Spill : MonoBehaviour
{
    public static Spill instance;
    public int pourThreshold = 15;
    public Transform origin = null;
    public GameObject streamPrefab = null;

    bool isPouring = false;
    Stream currentStream = null;

    float timeLeft = 5f;
    bool hasPoured = false;
    public GameObject mix;
    public Material mat;
    public Material yellowMat;
    public bool StartTimer = false;

    private void Awake()
    {
        instance = this;
        mix.GetComponent<MeshRenderer>().material = mat;
    }

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

        if (hasPoured)
            return;
        if (!StartTimer)
            return;

        if (isPouring)
        {
            CountPour();
        }
    }

    void CountPour()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0f)
        {
            mix.GetComponent<MeshRenderer>().material = yellowMat;
            hasPoured = true;
            Mix.instance.MilkNextTrigger();
        }
        Debug.Log(timeLeft);
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
        StartTimer = false;
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

    public void ResetMilk()
    {
        timeLeft = 5f;
        mix.GetComponent<MeshRenderer>().material = mat;
        hasPoured = false;
    }
}
