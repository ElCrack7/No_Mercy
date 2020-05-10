using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] string[] dialogues;
    public int dialogueIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowDialogue()
    {
        if (dialogueIndex > (dialogues.Length - 1))
        {
            dialogueIndex = (dialogues.Length - 1);
            print(name + ": " + dialogues[dialogueIndex]);
        }
        //else
        //{
        //    DialogueManager.instance.PrintOnDialogueBox((name + ": " + dialogues[dialogueIndex]));
        //}
    }
}
