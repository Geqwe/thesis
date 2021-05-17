using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mix : MonoBehaviour
{
    public static Mix instance;

    [SerializeField] Light spotLight;

    bool waitingSugar = true;
    bool waitingEggs = false;
    public bool waitingMilk = false;
    bool firstTime = true;

    short sugarCubes = 2;
    short eggs = 2;

    public GameObject DialogueManager;
    DialogueTrigger[] triggers;
    short index = 1;

    int wrongIndex = 0;
    public AudioClip[] wrongClips;
    AudioSource source;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        source = GetComponent<AudioSource>();
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
                if (wrongIndex == 0)
                {
                    source.Stop();
                    source.clip = wrongClips[0];
                    source.Play();
                    wrongIndex = 1;
                }
                else if (wrongIndex == 1)
                {
                    source.Stop();
                    source.clip = wrongClips[1];
                    source.Play();
                    wrongIndex = 2;
                }
                else
                {
                    source.Stop();
                    source.clip = wrongClips[2];
                    source.Play();
                    wrongIndex = 0;
                }
            }
            return;
        }
        
        if(other.name.StartsWith("eggBr"))
        {
            Debug.Log("eggs destroy");
            Destroy(other);
            if (waitingEggs)
            {
                eggs--;
                Debug.Log("eggs left to put " + sugarCubes);
                if (eggs == 0)
                {
                    Debug.Log("all eggs in");
                    waitingEggs = false;
                    if(firstTime)
                    {
                        StartCoroutine(ResetValues());
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
                if (wrongIndex == 0)
                {
                    source.Stop();
                    source.clip = wrongClips[0];
                    source.Play();
                    wrongIndex = 1;
                }
                else if (wrongIndex == 1)
                {
                    source.Stop();
                    source.clip = wrongClips[1];
                    source.Play();
                    wrongIndex = 2;
                }
                else
                {
                    source.Stop();
                    source.clip = wrongClips[2];
                    source.Play();
                    wrongIndex = 0;
                }
            }
            //other.gameObject.SetActive(false);
            return;
        }
    }

    IEnumerator BackToMenu()
    {
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene(0);
    }

    IEnumerator ResetValues()
    {
        sugarCubes = 1;
        eggs = 3;
        waitingSugar = true;
        firstTime = false;
        yield return new WaitForSeconds(7f);
        Spill.instance.ResetMilk();
        triggers[index++].enabled = true;
        spotLight.color = new Color(255f / 255f, 116f / 255f, 66f / 255f);
    }

    public void MilkNextTrigger()
    {
        triggers[index++].enabled = true;
        waitingMilk = false;
        waitingEggs = true;
    }
}
