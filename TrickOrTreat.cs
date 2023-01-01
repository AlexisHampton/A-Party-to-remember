using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrickOrTreat : MonoBehaviour
{
    public Collider2D coli;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.Return))
        {
           if( GameTime.Instance.currentTime == 8)
            {
                Debug.Log("You trick or treated!");
                coli.isTrigger = false;
                GameTime.Instance.TrickOrTreat();

            }
        }
    }



}
