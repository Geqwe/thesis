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
        public AudioClip clip;
    }

    List<Question> questions = new List<Question>();
    List<Question> questionsLeft = new List<Question>();
    List<Choice> options = new List<Choice>( new Choice[3] );
    int questionCounter;
    Question currQ;
    bool checkEndQuiz = false;
    Choice ch = new Choice();

    public Text desc, choice1, choice2, choice3;

    public AudioClip[] clips;
    AudioSource source;
    public AudioClip correct, wrong;
    public AudioSource sfxSource;

    public GameObject answerLight;
    public Material def, green, red;
    MeshRenderer meshRenderer;
    Material[] reds, greens, defs;

    // Start is called before the first frame update
    void Awake()
    {
        source = GetComponent<AudioSource>();
        InitQuiz();
        InitMaterials();
    }

    void OnEnable()
    {
        NextQuestion();
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
        q = NewQuestion("Which component is responsible for the output of a computer monitor?", false, q, "CPU", "Video Card", "HDD", false, true, false, clips[0]);
        questions.Add(q);
        q = NewQuestion("Which of these components can store data even after the computer is shut down?", false, q, "Case", "RAM", "HDD", false, false, true, clips[1]);
        questions.Add(q);
        q = NewQuestion("Which component is responsible for executing instructions that make up computer programs?", false, q, "RAM", "SSD", "CPU", false, false, true, clips[2]);
        questions.Add(q);
        q = NewQuestion("Which of these components is responsible for supplying other components with power?", false, q, "Power Supply", "Video Card", "Motherboard", true, false, false, clips[3]);
        questions.Add(q);
        q = NewQuestion("Which of these components can read and write data faster?", false, q, "SSD", "Video Card", "HDD", true, false, false, clips[4]);
        questions.Add(q);
        q = NewQuestion("Which component is the enclosure that contains most of the components of a personal computer?", false, q, "Case", "HDD", "RAM", true, false, false, clips[5]);
        questions.Add(q);
        q = NewQuestion("Which component is the main circuit board of a computer and makes everything work together?", false, q, "Video Card", "Motherboard", "Power Supply", false, true, false, clips[6]);
        questions.Add(q);
        q = NewQuestion("Which component provides input and output of audio signals to and from a computer?", false, q, "Sound Card", "Case", "RAM", true, false, false, clips[7]);
        questions.Add(q);
        questionsLeft = questions;
        questionCounter = questions.Count;
    }

    Question NewQuestion(string descr, bool isAnswered, Question q, string ch1, string ch2, string ch3, bool cho1, bool cho2, bool cho3, AudioClip clip)
    {
        q.desc = descr;
        q.answered = isAnswered;
        q.clip = clip;

        ch = ChangeChoice(ch1, cho1, ch);
        options[0] = ch;
        ch = ChangeChoice(ch2, cho2, ch);
        options[1] = ch;
        ch = ChangeChoice(ch3, cho3, ch);
        options[2] = ch;
        
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
        int rand = Random.Range(0, questionsLeft.Count-1);
        currQ = questionsLeft[rand];
        desc.text = currQ.desc;
        source.Stop();
        source.clip = currQ.clip;
        source.Play();
        choice1.text = currQ.choices[0].ans;
        choice2.text = currQ.choices[1].ans;
        choice3.text = currQ.choices[2].ans;
    }

    public void CheckCorrectness(int index)
    {
        if (checkEndQuiz)
            return;

        if(currQ.choices[index].correct)
        {
            sfxSource.PlayOneShot(correct);
            StartCoroutine(Correct());
            questionCounter--;
            questionsLeft.Remove(currQ);
            if (questionCounter == 0)
            {
                checkEndQuiz = true;
                StartCoroutine(ToTest());
                return;
            }
            NextQuestion();
        }
        else
        {
            sfxSource.PlayOneShot(wrong);
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
        yield return new WaitForSeconds(10f);
        GetComponent<DialogueTrigger>().enabled = true;
        SceneManager.LoadScene(2);
    }
}
