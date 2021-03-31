// Author: Giorgos Kletsas (you can delete that when you are making your game)
// --- Instructions ---
// Apply this to any gameobject
// On enable the dialogue starts
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue() {
        // can also be done with Singleton Pattern or as a variable
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    void Start() {
        TriggerDialogue();
    }
}
