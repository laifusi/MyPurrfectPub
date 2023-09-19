using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Night Show", menuName = "ScriptableObjects/Night Show")]
public class NightShowSO : ScriptableObject
{
    [SerializeField] new string name;
    [SerializeField] int prestigePerTurn;
    [SerializeField] int costPerTurn;
    [SerializeField] Sprite image;
}
