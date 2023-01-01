using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimFadeInAndOut : MonoBehaviour
{
    public void ShowPanel()
    {
        GameTime.Instance.HTPPanelShow(); 
    }
    public void HidePanel()
    {
        GameTime.Instance.DisablePanel();
    }

    public void HideSelf()
    {
        gameObject.SetActive(false);
    }

    public void GetRidOfSelf()
    {
        GameTime.Instance.IncreaseTime();
        gameObject.SetActive(false);
    }
}
