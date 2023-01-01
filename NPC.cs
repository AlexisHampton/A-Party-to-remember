using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public List<DialogueSO> startingDialogue;
    public List<string> regDialogue;
    public int dialogueLevel;

    public List<string> distractingDialogue;

    public DeathSO npcDeath;
    public bool isDistracted;
    public bool isDead;
    public bool isSaved;
    public bool hasTalked;
    private void Start()
    {
        dialogueLevel = 0;
        isDead = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.Return) && !hasTalked)
        {
            hasTalked = true;
            switch (GameTime.Instance.currentTime)
            {
                case 1:
                    CheckDialogueLevel(0);
                    break;
                case 3:
                    CheckDialogueLevel(1);

                    break;
                case 5:
                    CheckDialogueLevel(2);
                    break;
                case 7:
                    CheckDialogueLevel(3);
                    break;
                case 8:
                    CheckDialogueLevel(4);
                    break;


            }
        }
    }

    public void CheckDialogueLevel(int index)
    {
        if (!isDistracted)
        {
            if (dialogueLevel < index)
                DialogueManager.Instance.Talk(regDialogue[index - 1], name);
            else
            {
                DialogueManager.Instance.Talk(this, startingDialogue[index]);

                if (index == 4)
                    isSaved = true;
            }

        }
        else
        {
            DialogueManager.Instance.Talk(distractingDialogue[index - 2], name);
            isDistracted = false;
        }
    }

    public void OnDeath()
    {
        isDead = true;
        gameObject.SetActive(false);
    }
}
