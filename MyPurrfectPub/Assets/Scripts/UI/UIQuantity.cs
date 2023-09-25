using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIQuantity : MonoBehaviour
{
    [SerializeField] private bool isFood;
    [SerializeField] private bool isDrink;

    private string assignedElement;
    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    public void AssignElement()
    {
        if (isDrink)
        {
            PanelDrink panel = gameObject.GetComponentInParent<PanelDrink>();
            panel.OnDrinkBought += UpdateText;
            assignedElement = panel?.GetDrink().name;
        }

        if (isFood)
        {
            PanelFood panel = gameObject.GetComponentInParent<PanelFood>();
            panel.OnFoodBought += UpdateText;
            assignedElement = panel?.GetFood().name;
        }
    }

    private void OnEnable()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        text.SetText(Inventory.instance.GetQuantity(assignedElement, isFood, isDrink));
    }
}
