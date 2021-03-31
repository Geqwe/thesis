// Author: Giorgos Kletsas (you can delete that when you are making your game)

using Valve.VR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("From Canvas")]
    public Text dialogueText;  // text to show dialogue

    AudioSource source;
    
    public class Line // class to better compartmentalize each line
    {
        public AudioClip audio;
        [TextArea(3,10)]
        public string sentence;
    }

    private Dialogue dialog; // dialog to store every incoming dialogue 
    private Queue<Line> sentences; // Queue of lines to show
    Line sentenceHere; // line used to store in queue
    Line line; // line used to extract from queue

    // Start is called before the first frame update
    void Awake()
    {
        sentences = new Queue<Line>();
        source = GetComponent<AudioSource>();
    }

    public void StartDialogue(Dialogue dialogue) {
        dialog = dialogue;
        
        for (int i=0;i<dialog.sentence.Length;i++)
        { // Enqueue every line
            sentenceHere = new Line();
            sentenceHere.audio = dialog.audio[i];
            sentenceHere.sentence = dialog.sentence[i];
            sentences.Enqueue(sentenceHere);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence() { // Display each sentence
        if(sentences.Count == 0) {
            return;
        }
        line = sentences.Dequeue();
        TypeSentence(line);
    }

    void TypeSentence(Line line) { // if you want you can type each letter at once for effect (send to me on how to do that)
        dialogueText.text = line.sentence;
        source.PlayOneShot(line.audio);
    }

    void Update()
    {
        if(SteamVR_Actions.default_GrabGrip.GetStateDown(SteamVR_Input_Sources.RightHand)) {
            DisplayNextSentence();
        }
    }
}
