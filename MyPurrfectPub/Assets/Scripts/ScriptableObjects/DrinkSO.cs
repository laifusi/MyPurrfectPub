using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Drink", menuName = "ScriptableObjects/Drink")]
public class DrinkSO : ScriptableObject
{
    [SerializeField] public new string name;
    [SerializeField] public int cost;
    [SerializeField] public Sprite image;
    [SerializeField] public int prestige;
    [TextArea(2, 2)]
    [SerializeField] public string description;
    [SerializeField] public string rarity;
    [SerializeField] public int minprobability;
    [SerializeField] public int maxprobability;
    [SerializeField] public Color rarityColor;

    [HideInInspector] public int currentAmount;

    public void Reset()
    {
        currentAmount = 0;
    }
}
