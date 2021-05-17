using System.Collections;
using UnityEngine;
using Valve.VR;

public class BluePlatformTrigger : MonoBehaviour
{
    bool check = true;

    private void OnTriggerEnter(Collider other)
    {
        if(other.name.StartsWith("Head"))
        {
            GetComponent<DialogueTrigger>().enabled = true;
            GetComponent<BoxCollider>().enabled = false;
            check = false;
        }
    }

    void Update()
    {
        if (check) return;

        if (SteamVR_Actions.default_GrabGrip.GetStateDown(SteamVR_Input_Sources.RightHand) || SteamVR_Actions.default_GrabGrip.GetStateDown(SteamVR_Input_Sources.LeftHand))
        {
            StartCoroutine(ChangeParts());
        }
    }

    IEnumerator ChangeParts()
    {
        check = true;
        yield return new WaitForSeconds(13f);
        PickupItem.instance.canPickUp = true;
        GameManagerFirst.instance.ChangeDesktopToParts();
    }
}
