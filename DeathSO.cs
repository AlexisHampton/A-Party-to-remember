using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Park Jimin", menuName = "NPC Death")]
public class DeathSO : ScriptableObject
{

    public string npcName;
    public Sprite npcImg;
    [TextArea(12,25)] public string deathDescription;
    [TextArea(12, 25)] public string aliveDescription;


}
