using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text playerNameText;
    public TMP_Text npcNameText;
    public TMP_Text dialogueText;

    public FadeInOut animator;
    public GameObject player;

    private Queue<string> sentences;
    private Dialogue currentDialogue;

    // Use this for initialization
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.gameObject.SetActive(true);

        currentDialogue = dialogue;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();

        if (IsPlayerSentence(sentence))
        {
            sentence = sentence.Replace("Player", "");
            dialogueText.text = FormatDialogueText(sentence);
            playerNameText.text = "Player";
            npcNameText.text = "";
        }
        else
        {
            dialogueText.text = FormatDialogueText(sentence);
            playerNameText.text = "";
            npcNameText.text = currentDialogue.name;
        }

        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        animator.Hide();
        player.SetActive(true);
        player.GetComponent<StarterAssetsInputs>().SetCursorState(true);
    }

    bool IsPlayerSentence(string sentence)
    {
        // Replace this logic with your own to determine if it's the player's sentence
        return sentence.Contains("Player");
    }

    string FormatDialogueText(string sentence)
    {
        // Add any formatting or customization to the dialogue text here
        return sentence;
    }
}
