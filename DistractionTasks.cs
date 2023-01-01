using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DistractionTasks : Singleton<DistractionTasks>
{
    public Accessory[] accessories;
    public TextMeshProUGUI numOfAccessoriesText;
    public int numOfAccessories;
    public bool searchStarted;


    private void Start()
    {
        numOfAccessories = 0;
        searchStarted = false;
    }

    public void EnableAccessories()
    {
        
        if (!searchStarted)
        {
            foreach (Accessory accessory in accessories)
            {
                if (!accessory.isActiveAndEnabled)
                    accessory.gameObject.SetActive(true);
            }
            numOfAccessoriesText.gameObject.SetActive(true);
            searchStarted = true;
        }
    }

    public void FindAcessories(Accessory accessory)
    {
        accessory.gameObject.SetActive(false);
        numOfAccessories++;
        numOfAccessoriesText.text = "Accessories Collected: " +numOfAccessories.ToString();
        if(numOfAccessories == 5)
        {
            numOfAccessoriesText.gameObject.SetActive(false);
            foreach (NPC npc in GameTime.Instance.npcs)
            {
                npc.hasTalked = false;
            }
            GameTime.Instance.IncreaseTime();
        }
    }

    
}
