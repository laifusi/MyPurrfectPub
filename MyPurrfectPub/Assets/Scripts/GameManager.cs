using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int initialCoins;
    [SerializeField] private int initialPrestige;

    private int coins;
    private int prestige;

    public static Action<int> OnCoinsChange;
    public static Action<int> OnPrestigeChange;

    private IEnumerator Start()
    {
        yield return null;
        AddCoins(initialCoins);
        AddPrestige(initialPrestige);
    }

    public void AddCoins(int value)
    {
        coins += value;
        OnCoinsChange?.Invoke(coins);
    }

    public void AddPrestige(int value)
    {
        prestige += value;
        OnPrestigeChange?.Invoke(prestige);
    }
}
