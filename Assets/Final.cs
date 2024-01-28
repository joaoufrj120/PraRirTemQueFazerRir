using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Final : MonoBehaviour
{
    public DialogueManager DM;
    public DialogueTrigger dialogo1;
    public Animator pecanha;

    public GameObject pecachaSprite;

    void Start()
    {
        StartCoroutine(Esperar());
    }

    IEnumerator Esperar()
    {
        yield return new WaitForSeconds(2f);
        pecachaSprite.SetActive(true);
        pecanha.SetBool("pecanhaIdle", true);

        dialogo1.TriggerDialogue();
    }

    private void Update()
    {
        if (DM.index == 1)
        {
            Restart();
            return;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(2);
    }
}
