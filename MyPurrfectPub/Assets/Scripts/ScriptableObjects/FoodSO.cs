using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Food", menuName = "ScriptableObjects/Food")]
public class FoodSO : ScriptableObject
{
    [SerializeField] new string name;
    [SerializeField] public int cost;
    [SerializeField] public Sprite image;
    [SerializeField] public int prestige;
    [SerializeField] public string description;
    [SerializeField] public int durability;

    [HideInInspector] public int currentAmount;

    public void Reset()
    {
        currentAmount = 0;
    }
}