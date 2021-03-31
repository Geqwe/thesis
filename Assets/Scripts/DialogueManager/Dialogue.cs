// Author: Giorgos Kletsas (you can delete that when you are making your game)
// --- Instructions ---
// Dialogue class to store audio clips, text sentences, names for each dialogue
// You can even play a song at the end of the dialogue (leave it empty if you don't want)
// All 3 arrays (audio, sentence, name) must have the SAME SIZE
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    // audio clips for every line of dialogue
    public AudioClip[] audio;
    // text for every sentence
    [TextArea(3,10)]
    public string[] sentence;
}
