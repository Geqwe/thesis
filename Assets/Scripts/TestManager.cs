using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class TestManager : MonoBehaviour
{
    public static TestManager instance;

    public string neededObj;
    string[] items = { "RAM", "CPU", "HDD", "Motherboard", "SSD", "GPU", "SoundCard", "PowerSupply" };
    string[] hints = { "A computer memory that can be read and changed in any order", "The electronic circuitry within a computer that executes instructions", "A Hard Disk Drive or HDD",
        "The main circuit board of a computer that makes everything work together", "A Solid State Drive or SSD", "The component that generates a feed of output images to a display device",
        "The component that provides input and output of audio signals to and from a computer", "The part that supplies power to all components" };
    int index = 0;
    int wrongAnswers = 3;
    bool gameOver = false;

    public Text hint;
    public Text errors;

    public AudioClip[] clips;
    AudioSource source;

    int wrongIndex = 0;
    public AudioClip[] wrongClips;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        source = GetComponent<AudioSource>();
        StartCoroutine(FirstItem());
    }

    public void NextItem()
    {
        if (index == 8)
        {
            StartCoroutine(GameWin());
            return;
        }
        hint.text = hints[index];
        source.Stop();
        source.clip = clips[index];
        source.Play();
        neededObj = items[index++];
    }

    public void WrongItem()
    {
        if (gameOver)
            return;
        if(wrongAnswers==1)
        {
            GameOver();
            return;
        }

        if(wrongIndex==0)
        {
            source.Stop();
            source.clip = wrongClips[0];
            source.Play();
            wrongIndex = 1;
        }
        else
        {
            source.Stop();
            source.clip = wrongClips[1];
            source.Play();
            wrongIndex = 0;
        }
        wrongAnswers--;
        errors.text = wrongAnswers.ToString();
    }

    void GameOver()
    {
        Debug.Log("Game over");
        gameOver = true;
        StartCoroutine(GameRestart());
    }

    IEnumerator FirstItem()
    {
        yield return new WaitForSeconds(20f);
        NextItem();
    }

    IEnumerator GameRestart()
    {
        GetComponents<DialogueTrigger>()[1].enabled = true;
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(2);
    }

    IEnumerator GameWin()
    {
        GetComponents<DialogueTrigger>()[0].enabled = true;
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);
    }
}
