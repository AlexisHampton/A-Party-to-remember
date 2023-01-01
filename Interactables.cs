using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{
    public string itemName;
    public string desc;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            DialogueManager.Instance.Talk(desc, itemName);
        }
    }
}
