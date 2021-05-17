using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TrueFalseScript : MonoBehaviour
{
    public class Query
    {
        public string query;
        public bool correctness;
        public AudioClip clip;
        public Query(string q, bool correct, AudioClip cli)
        {
            query = q;
            correctness = correct;
            clip = cli;
        }
    }

    List<Query> questions = new List<Query>();
    Query currQuery;

    public Text quizText;
    AudioSource source;
    public AudioClip[] clips;
    public AudioSource sfxSource;
    public AudioClip wrong, correct, ending;

    // Start is called before the first frame update
    void Awake()
    {
        source = GetComponent<AudioSource>();
        InitQuestions();
        StartCoroutine(TriggerDial());
    }

    IEnumerator TriggerDial()
    {
        yield return new WaitForSeconds(10f);
        GetComponent<DialogueTrigger>().enabled = true;
    }

    void InitQuestions()
    {
        questions.Add(new Query("If two devices are connected to the same Switch then they are in the same LAN (Local Area Network).", true, clips[0]));
        questions.Add(new Query("The internet is not a network.", false, clips[1]));
        questions.Add(new Query("A router handles the routing of data between networks.", true, clips[2]));
        questions.Add(new Query("The internet is a Local Area Network (LAN).", false, clips[3]));
        questions.Add(new Query("A computer needs a Network Interface Card (NIC) in order to connect to networks.", true, clips[4]));
        questions.Add(new Query("If two devices are connected to the same Access Point, then they are in the same LAN(Local Area Network).", true, clips[5]));
        questions.Add(new Query("Routers today tend to work as Access Points as well.", true, clips[6]));
        questions.Add(new Query("A computer can connect with and without a wire to a network if they DO NOT have a Network Interface Card(NIC).", false, clips[7]));
        questions.Add(new Query("Wireless connection is possible through electromagnetic waves.", true, clips[8]));
        NextQuestion();
    }

    public void CheckTrueFalse(bool ans)
    {
        if(ans==currQuery.correctness)
        {
            Correct();
        }
        else
        {
            Wrong();
        }
    }

    void Correct()
    {
        if(questions.Count==1)
        {
            quizText.text = "Very Well! Good job! That would be all for this lesson.";
            source.Stop();
            source.clip = ending;
            source.Play();
            StartCoroutine(End());
            return;
        }
        sfxSource.PlayOneShot(correct);
        questions.Remove(currQuery);
        NextQuestion();
    }

    void Wrong()
    {
        sfxSource.PlayOneShot(wrong);
        NextQuestion();
    }

    void NextQuestion()
    {
        int rand = Random.Range(0, questions.Count - 1);
        currQuery = questions[rand];
        quizText.text = currQuery.query;
        source.Stop();
        source.clip = currQuery.clip;
        source.Play();
    }

    IEnumerator End()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);
    }
}
