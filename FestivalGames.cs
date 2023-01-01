using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FestivalGames : Singleton<FestivalGames>
{
    [Header("Party")]
    public GameObject partyProps;

    [Header("Candy")]
    public int candyEaten = 0;
    public int timeUntilStop = 30;
    public bool isEating = false;

    [Header("Apples")]
    public int applesCollected;
    
    public Apple[] apples;

    [Header("Dress up")]
    public GameObject dressUpPanel;
    public GameObject[] dressUpImgs;
    public TextMeshProUGUI dressUpNameText;

    [Header("NewSpots")]
    public Transform crowNewSpot;
    public Transform ghostNewSpot;
    public Transform crow;
    public Transform ghost;

    [Header("Text")]
    public TextMeshProUGUI applesCollectedText;
    public TextMeshProUGUI candyEatenText;
    

    int currentIndex;
    int prevIndex;
    

    private void Start()
    {
        candyEaten = 0;
        currentIndex = 1;
        prevIndex = 1;
        isEating = false;
        partyProps.SetActive(false);
    }

    private void Update()
    {
        if (isEating)
        {
            EatCandy();
        }
    }

    public void ChangeInTime()
    {

        switch (GameTime.Instance.currentTime)
        {
            case 0:
                GameTime.Instance.StartGame();
                break;
            case 1:
                GameTime.Instance.ShowPanels(0);
                break;
            case 2:
                GameTime.Instance.ShowPanels(1);
                partyProps.SetActive(true);
                EnableApples();
                crow.transform.position = crowNewSpot.position;
                ghost.transform.position = ghostNewSpot.position;
                break;
            case 4:
                GameTime.Instance.ShowPanels(2);
                candyEatenText.gameObject.SetActive(true);
                isEating = true;
                break;
            case 6:
                GameTime.Instance.ShowPanels(3);
                foreach(NPC npc in GameTime.Instance.npcs)
                {
                    if (npc.isDead)
                        npc.OnDeath();
                }

                DressUp();
                break;
            case 9:
                CalculateEnding.Instance.MakeEnding();
                break;
            default:
                GameTime.Instance.ShowPanels(4);
                break;
        }


    }
    public void EnableApples()
    {
        foreach (Apple apple in apples)
        {
            apple.gameObject.SetActive(true);
        }
        applesCollectedText.gameObject.SetActive(true);
    }

    public void PickApples(GameObject apple)
    {
        //disable apples
        apple.SetActive(false);
        applesCollected++;
        applesCollectedText.text = "Apples Collected: " + applesCollected.ToString();
        if(applesCollected == 10)
        {
            applesCollectedText.gameObject.SetActive(false);
            GameTime.Instance.IncreaseTime();
        }
    }

    public void EatCandy()
    {
        timeUntilStop--; //in update
        if (Input.GetKeyUp(KeyCode.Space))
        {
            candyEaten++;
            candyEatenText.text = "Candy Eaten: " + candyEaten.ToString();
        }

        if (timeUntilStop <= 0)
        {
            isEating = false;
            candyEatenText.gameObject.SetActive(false);
            GameTime.Instance.IncreaseTime();

        }
    }

    public void DressUp()
    {
        dressUpPanel.SetActive(true);
        ChangeNameAndSprite(0);
        
    }

    public void ChangeNameAndSprite(int index)
    {
        dressUpImgs[prevIndex].SetActive(false);
       
        prevIndex = index;
        dressUpImgs[index].SetActive(true);
        dressUpNameText.text =  dressUpImgs[index].name;
    }

    public void Forward()
    {
        currentIndex++;
        if (currentIndex > dressUpImgs.Length - 1)
        {
            currentIndex = 5;
            ChangeNameAndSprite(5);
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

    public void FinishDressing()
    {
        GameTime.Instance.IncreaseTime();
        dressUpPanel.SetActive(false);
    }
}
