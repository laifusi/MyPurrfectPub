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

    [SerializeField] private Image[] images;

    [SerializeField] private Transform panel;

    [SerializeField] private Button buttonPrefab;

    [SerializeField] private AudioClip buttonClip;

    [SerializeField] private Color commonColor, uncommonColor, rareColor, veryRareColor;

    private GameManager gameManager;

    public void AssignEvent(EventSO newEvent, GameManager gameManager)
    {
        evento = newEvent;
        this.gameManager = gameManager;

        switch(evento.rarity)
        {
            case EventSO.Rarity.Common:
                titulo.color = commonColor;
                titulo.text = "Evento Común";
                break;
            case EventSO.Rarity.Uncommon:
                titulo.color = uncommonColor;
                titulo.text = "Evento Poco Común";
                break;
            case EventSO.Rarity.Rare:
                titulo.color = rareColor;
                titulo.text = "Evento Raro";
                break;
            case EventSO.Rarity.VeryRare:
                titulo.color = veryRareColor;
                titulo.text = "Evento Muy Raro";
                break;
        }
        texto.text = evento.event_text;
        
        foreach(Image im in images)
        {
            im.sprite = evento.image;
        }

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
        GameManager.instance.AddActualCapacityDrink(option.capacity_drink);
        GameManager.instance.AddActualCapacityDrink(option.capacity_food);
        GameManager.instance.AddClients(option.new_clients);
        GameManager.instance.AddListCalculationCoins(option.michicoins);
        GameManager.instance.AddListCalculationPrestiege(option.purrstige);
        GameManager.instance.DoNightCalculations();
        GameManager.instance.PlaySound(buttonClip);
        //gameManager.AddCoins(option.michicoins);
        //gameManager.AddPrestige(option.purrstige);
        Destroy(gameObject);
    }
}
