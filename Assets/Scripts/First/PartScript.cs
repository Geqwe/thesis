using UnityEngine;

public class PartScript : MonoBehaviour
{
    Vector3 startPos;
    Quaternion startRot;

    // Start is called before the first frame update
    void Awake()
    {
        startPos = transform.position;
        startRot = transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name.StartsWith("Depo"))
        {
            //gameObject.SetActive(false);
            if (TestManager.instance.neededObj.StartsWith(gameObject.name))
            {
                TestManager.instance.NextItem();
                Destroy(gameObject);
            }
            else
            {
                gameObject.SetActive(false);
                transform.position = startPos;
                transform.rotation = startRot;
                gameObject.SetActive(true);
                TestManager.instance.WrongItem();
            }
        }
    }
}
