using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewPhobia", menuName = "Phobias", order =1)]
public class PhobiasSO : ScriptableObject
{
    public Phobias phobiaID;
    public string phobiaName;
    public string description;
    public string developed;
    public string[] attackAnxiety;
}
