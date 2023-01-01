using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Min Yoongi", menuName = "Dialogue")]
public class DialogueSO : ScriptableObject
{
    public List<string> dialogue = new List<string>();
    public int level;

    public ChoiceSO[] choices = new ChoiceSO[2];




}
