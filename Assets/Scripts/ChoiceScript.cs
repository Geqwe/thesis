using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChoiceScript : MonoBehaviour
{
    public struct Choice
    {
        public string ans;
        public bool correct;
        public Choice(string an, bool corr)
        {
            ans = an;
            correct = corr;
        }
    }
    public struct Question
    {
        public string desc;
        public bool answered;
        public List<Choice> choices;
    }

    List<Question> questions = new List<Question>();
    List<Question> questionsLeft = new List<Question>();
    List<Choice> options = new List<Choice>( new Choice[3] );
    int questionCounter;
    Question currQ;
    bool checkEndQuiz = false;
    Choice ch = new Choice();

    public Text desc, choice1, choice2, choice3;

    public GameObject answerLight;
    public Material def, green, red;
    MeshRenderer meshRenderer;
    Material[] reds, greens, defs;

    // Start is called before the first frame update
    void Awake()
    {
        InitQuiz();
        InitMaterials();
    }

    void InitMaterials()
    {
        meshRenderer = answerLight.GetComponent<MeshRenderer>();
        reds = meshRenderer.materials;
        reds[1] = red;
        greens = meshRenderer.materials;
        greens[1] = green;
        defs = meshRenderer.materials;
        defs[1] = def;
        meshRenderer.materials = defs;
    }

    void InitQuiz()
    {
        Question q = new Question();
        q = NewQuestion("Which component is responsible for the output of a computer monitor?", false, q, "CPU", "Video Card", "HDD", false, true, false);
        questions.Add(q);
        q = NewQuestion("Which of these components can store data even after the computer is shut down?", false, q, "Case", "RAM", "HDD", false, false, true);
        questions.Add(q);
        q = NewQuestion("Which component is responsible for executing instructions that make up computer programs?", false, q, "RAM", "SSD", "CPU", false, false, true);
        questions.Add(q);
        q = NewQuestion("Which of these components is responsible for supplying other components with power?", false, q, "Power Supply", "Video Card", "Motherboard", true, false, false);
        questions.Add(q);
        q = NewQuestion("Which of these components can read and write data faster?", false, q, "SSD", "Video Card", "HDD", true, false, false);
        questions.Add(q);
        q = NewQuestion("Which component is the enclosure that contains most of the components of a personal computer?", false, q, "Case", "HDD", "RAM", true, false, false);
        questions.Add(q);
        q = NewQuestion("Which component is the main circuit board of a computer and makes everything work together?", false, q, "Video Card", "Motherboard", "Power Supply", false, true, false);
        questions.Add(q);
        q = NewQuestion("Which component provides input and output of audio signals to and from a computer?", false, q, "Sound Card", "Case", "RAM", true, false, false);
        questions.Add(q);
        questionsLeft = questions;
        questionCounter = questions.Count;
        NextQuestion();
    }

    Question NewQuestion(string descr, bool isAnswered, Question q, string ch1, string ch2, string ch3, bool cho1, bool cho2, bool cho3)
    {
        q.desc = descr;
        q.answered = isAnswered;

        ch = ChangeChoice(ch1, cho1, ch);
        options[0] = ch;
        ch = ChangeChoice(ch2, cho2, ch);
        options[1] = ch;
        ch = ChangeChoice(ch3, cho3, ch);
        options[2] = ch;
        
        //Debug.Log(options[2].ans);
        q.choices = new List<Choice>(new Choice[3]);
        for (int i=0;i<3;i++)
        {
            q.choices[i] = SetChoice(q.choices[i], options[i].ans, options[i].correct);
        }
        return q;
    }

    Choice ChangeChoice(string ch, bool corr, Choice choice)
    {
        choice.ans = ch;
        choice.correct = corr;
        return choice;
    }

    Choice SetChoice(Choice choice, string ans, bool correctness)
    {
        choice.ans = ans;
        choice.correct = correctness;
        return choice;
    }

    void NextQuestion()
    {
        if (checkEndQuiz)
            return;
        Debug.Log("count " + questionsLeft.Count);
        int rand = Random.Range(0, questionsLeft.Count-1);
        currQ = questionsLeft[rand];
        desc.text = currQ.desc;
        choice1.text = currQ.choices[0].ans;
        choice2.text = currQ.choices[1].ans;
        choice3.text = currQ.choices[2].ans;
        //play clip
    }

    public void CheckCorrectness(int index)
    {
        if (checkEndQuiz)
            return;

        if(currQ.choices[index].correct)
        {
            //green box or sound
            StartCoroutine(Correct());
            questionCounter--;
            questionsLeft.Remove(currQ);
            //Debug.Log("Correct questions left: " + questionCounter);
            if (questionCounter == 0)
            {
                checkEndQuiz = true;
                StartCoroutine(ToTest());
                //Debug.Log(checkEndQuiz + " end ");
                return;
            }
            NextQuestion();
        }
        else
        {
            // red box or sound
            StartCoroutine(Wrong());
            NextQuestion();
        }
        
    }

    IEnumerator Correct()
    {
        meshRenderer.materials = greens;
        yield return new WaitForSeconds(1f);
        meshRenderer.materials = defs;
    }

    IEnumerator Wrong()
    {
        meshRenderer.materials = reds;
        yield return new WaitForSeconds(1f);
        meshRenderer.materials = defs;
    }

    IEnumerator ToTest()
    {
        GetComponent<DialogueTrigger>().enabled = true;
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(2);
    }
}
