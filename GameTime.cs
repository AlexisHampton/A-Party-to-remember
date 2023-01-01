using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameTime : Singleton<GameTime>
{
    [Header("time")]
    public int hour;
    public int minute;
    public int currentTime; //max 7

    [Header("UI")]
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI totText;
    public GameObject fadeOutPanel;
    public GameObject startingPanel;
    public GameObject startPanel;

    [Header("Controls")]
    public GameObject[] htpPanels;
    public NPC[] npcs;

    public int housesTOTed = 0;

    int panelIndex;


    // Start is called before the first frame update
    void Start()
    {
        //party starts at 5
        hour = 4;
        minute = 30;

        housesTOTed = 0;
    }

    public void IncreaseTime()
    {
        minute += 30;
        currentTime++;
        FestivalGames.Instance.ChangeInTime();
        if (minute == 60)
        {
            minute = 00;
            hour++;
        }
        if (minute == 0)
            timeText.text = hour.ToString() + ":00";
        else
            timeText.text = hour.ToString() + ":" + minute.ToString();

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }


    public void StartGame()
    {
        startingPanel.SetActive(true);
        startPanel.SetActive(false);
    }

    public void ShowPanels(int index)
    {

        panelIndex = index;
        fadeOutPanel.SetActive(true);
        fadeOutPanel.GetComponent<Animation>().Play();
    }

    public void HTPPanelShow()
    {

        htpPanels[panelIndex].SetActive(true);
        startingPanel.SetActive(false);
    }

    public void DisablePanel()
    {
        htpPanels[panelIndex].SetActive(false);

    }

    public void TrickOrTreat()
    {
        housesTOTed++;
        totText.text = housesTOTed.ToString();
    }

    public void CheckIfCanIncreaseTime()
    {
        int numOfNPcstalked = 0;
        int numOfNPCDead = 0;
        foreach (NPC npc in npcs)
        {
            if (npc.hasTalked)
            {
                numOfNPcstalked++;
            }
            if (npc.isDead)
            {
                numOfNPCDead++;
            }
        }

        if (currentTime == 3 && DistractionTasks.Instance.searchStarted)
        {
            return;
        }

        if (numOfNPcstalked == 4 || numOfNPCDead == 4)
        {

            IncreaseTime();
            foreach (NPC npc in npcs)
            {
                npc.hasTalked = false;
            }

        }
        else if (numOfNPCDead + numOfNPcstalked  == 4)
        {
            IncreaseTime();
            foreach (NPC npc in npcs)
            {
                npc.hasTalked = false;
            }
        }


    }
}
