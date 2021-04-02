using System.Collections;
using UnityEngine;

public class BluePlatformTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.name.StartsWith("Head"))
        {
            GetComponent<DialogueTrigger>().enabled = true;
            GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(ChangeParts());
        }
    }

    IEnumerator ChangeParts()
    {
        yield return new WaitForSeconds(10f);
        GameManagerFirst.instance.ChangeDesktopToParts();
    }
}
