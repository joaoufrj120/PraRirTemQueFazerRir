using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public DialogueTrigger dialogos1;
    void Start()
    {
        dialogos1.TriggerDialogue();
    }
}
