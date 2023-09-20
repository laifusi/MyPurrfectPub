using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PanelDrink : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nombre;

    [SerializeField] private TextMeshProUGUI precio;

    [SerializeField] private TextMeshProUGUI prestigio;

    [SerializeField] private GameObject detailsPrefab;

    // Update is called once per frame
    void Update()
    {

    }

    public void AsignarValoresDrink(DrinkSO bebida)
    {
        nombre.text = bebida.name;
        precio.text = bebida.cost.ToString();
        prestigio.text = bebida.prestige.ToString();
    }
}
