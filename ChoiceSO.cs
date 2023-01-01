using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Kim Namjoon", menuName = "Choice")]
public class ChoiceSO : ScriptableObject
{
    public string choiceBlurb;
    public DialogueSO nextDialogue;

    public bool isLeaveChoice;
    public bool isKillChoice;


}
