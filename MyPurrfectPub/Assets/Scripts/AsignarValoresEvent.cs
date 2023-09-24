using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AsignarValoresEvent : MonoBehaviour
{
    [SerializeField] private EventSO evento;

    [SerializeField] private TextMeshProUGUI titulo;

    [SerializeField] private TextMeshProUGUI texto;

    [SerializeField] private Transform panel;

    [SerializeField] private Button buttonPrefab;

    private GameManager gameManager;

    public void AssignEvent(EventSO newEvent, GameManager gameManager)
    {
        evento = newEvent;
        this.gameManager = gameManager;

        titulo.text = evento.event_tittle;
        texto.text = evento.event_text;

        for(int i = 0; i < evento.options.Length; i++)
        {
            EventSO.option option = evento.options[i];
            Button newButton = Instantiate(buttonPrefab, panel);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = option.text_option;
            newButton.onClick.AddListener(() => EndEvent(option.id_option));
        }
    }

    private void EndEvent(int optionId)
    {
        evento.options[optionId].selected_option = true;
        EventSO.option option = evento.options[optionId];
        GameManager.instance.costEventCoins += option.michicoins;
        GameManager.instance.costEventPrestige += option.purrstige;
        GameManager.instance.AddCapacityDrink(option.capacity_drink);
        GameManager.instance.AddCapacityDrink(option.capacity_food);
        GameManager.instance.AddClients(option.new_clients);
        GameManager.instance.AddListCalculationCoins(option.michicoins);
        GameManager.instance.AddListCalculationPrestiege(option.purrstige);
        GameManager.instance.DoNightCalculations();
        //gameManager.AddCoins(option.michicoins);
        //gameManager.AddPrestige(option.purrstige);
        Destroy(gameObject);
    }
}
