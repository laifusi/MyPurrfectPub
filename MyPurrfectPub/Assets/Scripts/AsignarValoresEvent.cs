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

    public void AssignEvent(EventSO newEvent)
    {
        evento = newEvent;

        titulo.text = evento.event_tittle;
        texto.text = evento.event_text;

        foreach (EventSO.option option in evento.options)
        {
            Button newButton = Instantiate(buttonPrefab, panel);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = option.text_option;
            //newButton.onClick.AddListener()
        }
    }
}
