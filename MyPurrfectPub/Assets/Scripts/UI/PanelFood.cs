using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PanelFood : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nombre;

    [SerializeField] private TextMeshProUGUI precio;

    [SerializeField] private TextMeshProUGUI prestigio;

    [SerializeField] private string detalles;

    [SerializeField] private GameObject detailsPrefab;

    [SerializeField] private FoodSO foodAsociada;

    // Update is called once per frame
    void Update()
    {

    }

    public void AsignarValoresFood(FoodSO comida)
    {
        nombre.text = comida.name;
        precio.text = comida.cost.ToString();
        prestigio.text = comida.prestige.ToString();
        detalles = comida.description;
        foodAsociada = comida;
    }

    public void Comprar()
    {
        if(foodAsociada.cost <= Inventory.instance.moneycount)
        {
            Inventory.instance.AddFood(foodAsociada);
            Inventory.instance.RemoveMichiCoins(foodAsociada.cost);
        }
    }

    public void Details()
    {
        GameObject newPanelDetail = Instantiate(detailsPrefab, GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>().transform);
        newPanelDetail.GetComponent<DetailPanel>().OpenDetails(detalles);
    }
}
