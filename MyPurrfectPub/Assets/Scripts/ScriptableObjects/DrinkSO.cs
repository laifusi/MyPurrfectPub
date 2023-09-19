using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Drink", menuName = "ScriptableObjects/Drink")]
public class DrinkSO : ScriptableObject
{
    [SerializeField] new string name;
    [SerializeField] int cost;
    [SerializeField] Sprite image;
    [SerializeField] int prestige;

    [HideInInspector] int currentAmount;

    public void Reset()
    {
        currentAmount = 0;
    }
}
