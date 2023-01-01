using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonImptNPCs : MonoBehaviour
{
    public List<string> sentences;
    public List<string> altSentences;
    public NPC npcImpacted;
    public List<NPC> npcs;

    bool isImpacted;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {

            switch (GameTime.Instance.currentTime)
            {
                case 1:
                    DialogueManager.Instance.Talk(sentences[0], name);
                    break;
                case 3:
                    DialogueManager.Instance.Talk(sentences[1], name);
                    DistractionTasks.Instance.EnableAccessories();
                    break;
                case 5:
                    TalkToPumpkin(2);
                    npcImpacted.isDead = true;
                    break;
                case 7:
                    TalkToPumpkin(3);
                    GameTime.Instance.CheckIfCanIncreaseTime();
                    break;
                case 8:
                    TalkToPumpkin(4);
                    GameTime.Instance.CheckIfCanIncreaseTime();
                    break;
            }
            if (GameTime.Instance.currentTime != 1)
                npcImpacted.isDistracted = true;

        }
    }

    public void TalkToPumpkin(int index)
    {
        if(npcImpacted.hasTalked)
            DialogueManager.Instance.Talk(altSentences[index-2], name);
        else
            DialogueManager.Instance.Talk(sentences[index], name);

    }
}
