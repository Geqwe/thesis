using UnityEngine;

public class GameManagerFirst : MonoBehaviour
{
    public static GameManagerFirst instance;

    public GameObject desktop;
    public GameObject partsAlone;
    public GameObject btn2, quiz, title, body;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    public void ChangeDesktopToParts()
    {
        desktop.SetActive(false);
        partsAlone.SetActive(true);
    }

    public void ChangeToQuiz()
    {
        btn2.SetActive(false);
        quiz.SetActive(true);
        title.SetActive(true);
        body.SetActive(true);
    }
}
