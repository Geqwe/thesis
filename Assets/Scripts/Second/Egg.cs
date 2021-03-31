using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    [SerializeField] GameObject brokenEgg;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Break")
        {
            Instantiate(brokenEgg, collision.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
        }
    }
}
