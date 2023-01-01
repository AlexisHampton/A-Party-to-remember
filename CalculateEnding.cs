using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CalculateEnding : Singleton<CalculateEnding>
{
    [Header("Ending")]
    public NPC[] npcs;
    public int goodEndingNum = 5;
    public int trueEndingNum = 20;

    [Header("End UI")]
    public GameObject[] newspaperPages;
    public GameObject newspaperPanel;
    public TextMeshProUGUI[] descrText;
    public TextMeshProUGUI savedText;
    public TextMeshProUGUI subText;
    public Image[] spriteImg;


    int allNPCs;
    int prevIndex;
    int currentIndex;

    public void MakeEnding()
    {
        //calculate num before and then check 
        foreach (NPC npc in npcs)
        {
            if (npc.dialogueLevel == goodEndingNum)
            {
                allNPCs += 5;
                npc.isSaved = true;
            }
        }

        //check if all npcs are good
        if (allNPCs >= trueEndingNum)
        {
            TrueEnding();
            subText.text = "And the fun has just begun";
        }
        else if (allNPCs < goodEndingNum)
        {
            BadEnding();
        }
        else
        {
            OnePersonEnding();
            subText.text = "And the work has just begun";
        }
        newspaperPanel.SetActive(true);
        savedText.text = (allNPCs / 4).ToString() + "/4";
    }

    public void BadEnding()
    {
        for (int i = 0; i < npcs.Length; i++)
        {
            descrText[i].text = npcs[i].npcDeath.deathDescription;
            spriteImg[i].sprite = npcs[i].npcDeath.npcImg;

        }
    }
    public void TrueEnding()
    {
        for (int i = 0; i < npcs.Length; i++)
        {
            if (!npcs[i].isDead)
            {
                descrText[i].text = npcs[i].npcDeath.aliveDescription;
                spriteImg[i].sprite = npcs[i].npcDeath.npcImg;
            }
        }
    }

    public void OnePersonEnding()
    {

        for (int i = 0; i < npcs.Length; i++)
        {
            if (npcs[i].dialogueLevel == goodEndingNum && !npcs[i].isDead)
            {
                descrText[i].text = npcs[i].npcDeath.aliveDescription;
                spriteImg[i].sprite = npcs[i].npcDeath.npcImg;
               
            }
            else
            {
                descrText[i].text = npcs[i].npcDeath.deathDescription;
                spriteImg[i].sprite = npcs[i].npcDeath.npcImg;
            }
        }
    }

    public void ChangeNameAndSprite(int index)
    {
        newspaperPages[prevIndex].SetActive(false);

        prevIndex = index;
        newspaperPages[index].SetActive(true);
    }

    public void Forward()
    {
        currentIndex++;
        if (currentIndex > newspaperPages.Length - 1)
        {
            currentIndex = 3;
            ChangeNameAndSprite(3);
        }
        else
            ChangeNameAndSprite(currentIndex);
    }

    public void Backward()
    {
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = 0;
            ChangeNameAndSprite(0);
        }
        else
            ChangeNameAndSprite(currentIndex);

    }
}

