using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public DialogueManager DM;
    public DialogueTrigger dialogo1;
   
    void Start()
    {
        StartCoroutine(Esperar());
    }

    IEnumerator Esperar()
    {
        yield return new WaitForSeconds(5);
        dialogo1.TriggerDialogue();
    }

    void Update()
    {
        if (DM.onGoing == false)
        {

        }   
    }

}
