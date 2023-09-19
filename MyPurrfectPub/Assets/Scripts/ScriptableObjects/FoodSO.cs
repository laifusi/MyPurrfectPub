using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Food", menuName = "ScriptableObjects/Food")]
public class FoodSO : ScriptableObject
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
