using UnityEngine;
using UnityEngine.UI;

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

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        NextItem();
    }

    public void NextItem()
    {
        Debug.Log("Next Item");
        if (index == 8)
        {
            Debug.Log("You win!");
            return;
        }
        hint.text = hints[index];
        neededObj = items[index++];
    }

    public void WrongItem()
    {
        Debug.Log("Wrong Item");
        if (gameOver)
            return;
        if(wrongAnswers==1)
        {
            GameOver();
            return;
        }
        wrongAnswers--;
    }

    void GameOver()
    {
        Debug.Log("Game over");
        gameOver = true;
    }
}
