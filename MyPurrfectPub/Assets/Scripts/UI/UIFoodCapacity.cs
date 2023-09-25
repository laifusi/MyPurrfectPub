using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIFoodCapacity : MonoBehaviour
{
    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();

        GameManager.OnFoodCapacityChange += ChangeText;
    }

    private void ChangeText(int value)
    {
        text.SetText(value.ToString() + " capacidad de comida");
    }

    private void OnDestroy()
    {
        GameManager.OnFoodCapacityChange -= ChangeText;
    }
}
