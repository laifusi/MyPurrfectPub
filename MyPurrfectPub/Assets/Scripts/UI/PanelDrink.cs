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

    [SerializeField] private GameObject detailsPrefab;

    [SerializeField] private DrinkSO drinkAsociada;

    [SerializeField] private Image Color_rarity;

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
        Color_rarity.color = bebida.rarityColor;
        drinkAsociada = bebida;
    }

    public void Comprar()
    {
        if(drinkAsociada.cost <= GameManager.instance.GetCoins())
        {
            Inventory.instance.AddDrink(drinkAsociada);
            GameManager.instance.AddCoins(drinkAsociada.cost * -1);
        }
    }

    public void Details()
    {
        GameObject newPanelDetail = Instantiate(detailsPrefab, GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>().transform);
        newPanelDetail.GetComponent<DetailPanel>().OpenDetails(detalles);
    }
}
