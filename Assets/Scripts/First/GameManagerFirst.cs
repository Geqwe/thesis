using UnityEngine;

public class GameManagerFirst : MonoBehaviour
{
    public static GameManagerFirst instance;

    public GameObject desktop;
    public GameObject partsAlone;
    public GameObject btn1, btn2, quiz, title, body;

    // Start is called before the first frame update
    void Awake()
    {
        //instance = this;
    }

    public void ChangeDesktopToParts()
    {
        desktop.SetActive(false);
        partsAlone.SetActive(true);
        //dialogue trigger
        btn1.SetActive(false);
    }

    public void ChangeToQuiz()
    {
        btn2.SetActive(false);
        quiz.SetActive(true);
        title.SetActive(true);
        body.SetActive(true);
        //dialogue trigger
    }
}
