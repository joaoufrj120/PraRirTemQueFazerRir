using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public DialogueManager DM;
    public DialogueTrigger dialogo1;
    public DialogueTrigger dialogo2;
    public DialogueTrigger dialogo3;
    public DialogueTrigger dialogo4;
    public DialogueTrigger dialogo5;
    public DialogueTrigger dialogo6;
    public DialogueTrigger dialogo7;
    public Animator pecanha;
   
    void Start()
    {
        StartCoroutine(Esperar());
    }

    IEnumerator Esperar()
    {
        yield return new WaitForSeconds(1);
 
        yield return new WaitForSeconds(0.5f);
        pecanha.SetBool("pecanhaIdle", true);

        dialogo1.TriggerDialogue();
    }

    private void Update()
    {
        if (DM.index== 1)
        {
            //Dialogos 2
            StartCoroutine(Pausa());
            DM.index++;
            return;
        }
        else if(DM.index== 3) 
        {
            StartCoroutine(Pausa2());
            DM.index++;
            return;
        }
        else if (DM.index == 5)
        {
            StartCoroutine(Pausa3());
            DM.index++;
            return;
        }
        else if (DM.index == 7)
        {
            StartCoroutine(Pausa4());
            DM.index++;
            return;
        }
        else if (DM.index == 9)
        {
            StartCoroutine(Pausa5());
            DM.index++;
            return;
        }
        else if (DM.index == 11)
        {
            StartCoroutine(Pausa6());
        }
    }
    IEnumerator Pausa()
    {
        yield return new WaitForSeconds(0.8f);
        pecanha.SetBool("pisquerindo", true);
        yield return new WaitForSeconds(0.7f);
        pecanha.SetBool("pisquerindo", false);
        dialogo2.TriggerDialogue();
    }

    IEnumerator Pausa2()
    {
        yield return new WaitForSeconds(1f);
        dialogo3.TriggerDialogue();
    }
    IEnumerator Pausa3()
    {
        yield return new WaitForSeconds(1f);
        dialogo4.TriggerDialogue();
    }
    IEnumerator Pausa4()
    {
        yield return new WaitForSeconds(0.8f);
        pecanha.SetBool("pisquerindo", true);
        yield return new WaitForSeconds(0.7f);
        pecanha.SetBool("pisquerindo", false);
        dialogo5.TriggerDialogue();
    }
    IEnumerator Pausa5()
    {
        yield return new WaitForSeconds(0.1f);
        pecanha.SetBool("olhofechado", true);
        yield return new WaitForSeconds(1.4f);
        pecanha.SetBool("olhofechado", false);
        dialogo6.TriggerDialogue();
    }

    IEnumerator Pausa6()
    {
        yield return new WaitForSeconds(0.1f);
        pecanha.SetBool("piscando", true);
        yield return new WaitForSeconds(1.4f);
        //Chamar Scene do jogo
    }
}
