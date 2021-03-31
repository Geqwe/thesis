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

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
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
        //dialogue trigger
        yield return new WaitForSeconds(5f);
        NextItem();
    }

    IEnumerator GameRestart()
    {
        //dialogue trigger
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(2);
    }

    IEnumerator GameWin()
    {
        //dialogue trigger
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);
    }
}
