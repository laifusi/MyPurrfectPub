using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Night Show", menuName = "ScriptableObjects/Night Show")]
public class NightShowSO : ScriptableObject
{
    [SerializeField] public new string name;
    [SerializeField] public int prestige;
    [SerializeField] public int cost;
    [SerializeField] public string description;
    [SerializeField] public Sprite image;
}
