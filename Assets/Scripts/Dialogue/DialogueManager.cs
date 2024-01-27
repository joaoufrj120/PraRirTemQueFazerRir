using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public Queue<string> sentences;
    public Queue<Sprite> sprites;
    public Queue<bool> choices;
    
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Image imageUi;

    public GameObject continuar;
    public GameObject sim;

    public Animator animator;

    public int index = 0;


    // Start is called before the first frame update
    void Awake()
    {
        sentences = new Queue<string>();
        sprites = new Queue<Sprite>();
        choices = new Queue<bool>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);
        nameText.text = dialogue.name;


        sentences.Clear();
        sprites.Clear();
        choices.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        foreach (Sprite sprite in dialogue.sprites)
        {
            sprites.Enqueue(sprite);
        }
        foreach (bool choice in dialogue.choices)
        {
            choices.Enqueue(choice);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        continuar.SetActive(false);
        sim.SetActive(false);
        if (sentences.Count == 0)
        {
            animator.SetBool("IsOpen", false);
            index++;
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        
        Sprite sprite = sprites.Dequeue();
        imageUi.sprite = sprite;

    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.02f);
        }

        bool choice = choices.Dequeue();
        if (choice)
        {
            continuar.SetActive(false);
            sim.SetActive(true);
        }
        else
        {
            continuar.SetActive(true);
            sim.SetActive(false);
        }

    }

    void EndDialgoue() 
    {
        animator.SetBool("IsOpen", false);
    }
}
