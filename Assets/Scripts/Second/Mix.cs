using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mix : MonoBehaviour
{
    public static Mix instance;

    bool waitingSugar = true;
    bool waitingEggs = false;
    public bool waitingMilk = false;
    bool firstTime = true;

    short sugarCubes = 2;
    short eggs = 2;

    public GameObject DialogueManager;
    DialogueTrigger[] triggers;
    short index = 1;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        triggers = DialogueManager.GetComponents<DialogueTrigger>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.name.StartsWith("Sugar"))
        {
            Debug.Log("sugar destroy");
            Destroy(other);
            if(waitingSugar)
            {
                sugarCubes--;
                Debug.Log("sugars left to put "+sugarCubes);
                if (sugarCubes==0)
                {
                    Debug.Log("all sugars in");
                    waitingSugar = false;
                    waitingMilk = true;
                    triggers[index++].enabled = true;
                }
            }
            else
            {
                //play sound wrong
            }
            return;
        }
        
        if(other.name.StartsWith("eggBr"))
        {
            Debug.Log("eggs destroy");
            if (waitingEggs)
            {
                eggs--;
                Debug.Log("eggs left to put " + sugarCubes);
                if (eggs == 0)
                {
                    Debug.Log("all eggs in");
                    waitingEggs = false;
                    waitingMilk = true;
                    if(firstTime)
                    {
                        ResetValues();
                    }
                    else
                    {
                        StartCoroutine(BackToMenu());
                    }
                    triggers[index++].enabled = true;
                }
            }
            else
            {
                //play sound wrong
            }
            other.gameObject.SetActive(false);
            return;
        }
    }

    IEnumerator BackToMenu()
    {
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene(0);
    }

    void ResetValues()
    {
        //change the light
        sugarCubes = 1;
        eggs = 3;
        waitingSugar = true;
    }

    public void MilkNextTrigger()
    {
        triggers[index++].enabled = true;
    }
}
