using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : Singleton<DialogueManager>
{
    public List<string> dialogue;

    [Header("text stuff")]
    public GameObject dialoguePanel;
    public GameObject continueButton;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    public float timeUntilNextLetter = 0.03f;
    public float multiplier = .15f;

    [Header("Choice stuff")]
    public GameObject choicePanel;
    public GameObject[] choiceGOs;

    int index = 0;
    bool firstTime;
    ChoiceSO[] choices;
    DialogueSO dialogueSO;
    NPC currentNPC;
    DialogueSO npcDialogueSO;
    bool singleSentence;

    private void Start()
    {
        index = 0;
        singleSentence = false;
    }

    public void Talk(NPC npc, DialogueSO dialogueSOFromNPC)
    {
        OpenDialogue();
        firstTime = true;
        currentNPC = npc;
        npcDialogueSO = dialogueSOFromNPC;

        nameText.text = npc.name;

        if (index >= dialogueSOFromNPC.dialogue.Count)
        {
            choices = dialogueSOFromNPC.choices;
            index = 0;
            if (choices.Length > 0)
                PresentChoices();
            else
            {
                CloseDialogue();
            }
        }
        else
        {
            //dialogueText.text = dialogueSOFromNPC.dialogue[index];
            StartCoroutine(NextLetter(dialogueSOFromNPC.dialogue[index]));
        }
    }

    public void Talk()
    {
        OpenDialogue();
        firstTime = false;

        if (index >= dialogue.Count)
        {
            choices = dialogueSO.choices;

            if (choices.Length > 0)
                PresentChoices();
            else
            {
                currentNPC.dialogueLevel++;
                CloseDialogue();
            }
        }
        else
        {
            //dialogueText.text = dialogue[index];
            StartCoroutine(NextLetter(dialogue[index]));
        }
    }

    public void Talk(string sentence, string name)
    {
        singleSentence = true;
        OpenDialogue();

        nameText.text = name;
        
        //dialogueText.text = sentence;
        StartCoroutine(NextLetter(sentence));
    }

    IEnumerator NextLetter(string sentence)
    {
        continueButton.SetActive(false);

        dialogueText.text = " ";
        foreach (char letter in sentence)
        {
            if (letter == '.')
                 timeUntilNextLetter += multiplier;
            dialogueText.text += letter;
            yield return new WaitForSeconds(timeUntilNextLetter);
            if (letter == '.')
                timeUntilNextLetter -= multiplier;
        }
        
        StopAllCoroutines();
        continueButton.SetActive(true);
    }


    public IEnumerator LoadSentence(string sentence)
    {
        continueButton.SetActive(false);
        /*foreach (char letter in sentence.ToCharArray())
        {
            Debug.Log(letter);
            ///
            dialogueText.text += letter;
            
            //timeUntilNextLetter += .015f;
        }*/
        yield return new WaitForSeconds(timeUntilNextLetter);
        continueButton.SetActive(true);
    }

    public void PresentChoices()
    {

        for (int i = 0; i < choices.Length; i++)
        {
            choiceGOs[i].GetComponentInChildren<TextMeshProUGUI>().text =
                choices[i].choiceBlurb;
        }

        choicePanel.SetActive(true);
    }

    public void NextDialogue()
    {
        if (singleSentence)
        {
            singleSentence = false;
            CloseDialogue();
        }
        else
        {
            index++;
            continueButton.SetActive(false);

            if (firstTime)
                Talk(currentNPC, npcDialogueSO);
            else
                Talk();
        }

    }

    public void SelectChoice(int choiceIndex)
    {
        index = 0;

        if (choices[choiceIndex].isLeaveChoice)
        {
            CloseDialogue();
            
            if (choices[choiceIndex].isKillChoice)
                currentNPC.OnDeath();
        }
        else
        {
            dialogue = choices[choiceIndex].nextDialogue.dialogue;
            dialogueSO = choices[choiceIndex].nextDialogue;
            choicePanel.SetActive(false);
            Talk();

        }

        
    }

    public void OpenDialogue()
    {
        if(index == 0)
            dialogueText.text = "";
        dialoguePanel.SetActive(true);
        PlayerMovement.Instance.canMove = false;
    }

    public void CloseDialogue()
    {
        dialoguePanel.SetActive(false);
        index = 0;
        PlayerMovement.Instance.canMove = true;
        choicePanel.SetActive(false);
        GameTime.Instance.CheckIfCanIncreaseTime();
    }
}
