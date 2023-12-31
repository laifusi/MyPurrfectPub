using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIDrinkCapacity : MonoBehaviour
{
    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();

        GameManager.OnDrinkCapacityChange += ChangeText;
    }

    private void ChangeText(int value)
    {
        text.SetText("Servicio m�ximo de bebidas " + value.ToString());
    }

    private void OnDestroy()
    {
        GameManager.OnDrinkCapacityChange -= ChangeText;
    }
}
