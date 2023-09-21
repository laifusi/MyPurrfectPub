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

    [SerializeField] private string detalles;

    [SerializeField] private Image image;

    [SerializeField] private GameObject detailsPrefab;

    [SerializeField] private DrinkSO drinkAsociada;

    // Update is called once per frame
    void Update()
    {

    }

    public void AsignarValoresDrink(DrinkSO bebida)
    {
        nombre.text = bebida.name;
        precio.text = bebida.cost.ToString();
        prestigio.text = bebida.prestige.ToString();
        detalles = bebida.description;
        drinkAsociada = bebida;
        image.sprite = bebida.image;
    }

    public void Comprar()
    {
        if(drinkAsociada.cost <= Inventory.instance.moneycount)
        {
            Inventory.instance.AddDrink(drinkAsociada);
            Inventory.instance.RemoveMichiCoins(drinkAsociada.cost);
        }
    }

    public void Details()
    {
        GameObject newPanelDetail = Instantiate(detailsPrefab, GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>().transform);
        newPanelDetail.GetComponent<DetailPanel>().OpenDetails(detalles);
    }
}
