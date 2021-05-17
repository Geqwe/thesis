using UnityEngine;
using UnityEngine.EventSystems;

public class Pointer : MonoBehaviour
{
    float length = 500.0f;
    public GameObject dot;
    public VRInputModule inputModule;

    LineRenderer lineRenderer = null;

    // Start is called before the first frame update
    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLine();
    }

    void UpdateLine()
    {
        PointerEventData data = inputModule.GetData();
        
        float targetLength = data.pointerCurrentRaycast.distance == 0 ? length : data.pointerCurrentRaycast.distance;

        RaycastHit hit = CreateRaycast();
        Vector3 endPos = transform.position + (transform.forward * length);
        if (hit.collider != null)
            endPos = hit.point;
        dot.transform.position = endPos;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, endPos);
    }

    RaycastHit CreateRaycast()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, length);
        //Debug.Log(hit.transform.name);
        return hit;
    }
}
