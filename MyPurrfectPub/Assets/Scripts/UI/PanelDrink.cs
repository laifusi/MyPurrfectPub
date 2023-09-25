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

    [SerializeField] private Image Color_rarity;

    [SerializeField] private UIQuantity quantityElement;

    public System.Action OnDrinkBought;

    private void Start()
    {
        quantityElement.AssignElement();
    }

    public DrinkSO GetDrink()
    {
        return drinkAsociada;
    }

    public void AsignarValoresDrink(DrinkSO bebida)
    {
        nombre.text = bebida.name;
        precio.text = bebida.cost.ToString();
        prestigio.text = bebida.prestige.ToString();
        detalles = bebida.description;
        Color_rarity.color = bebida.rarityColor;
        drinkAsociada = bebida;
        image.sprite = bebida.image;
    }

    public void Comprar()
    {
        if(drinkAsociada.cost <= GameManager.instance.GetCoins())
        {
            Inventory.instance.AddDrink(drinkAsociada);
            GameManager.instance.AddCoins(drinkAsociada.cost * -1);
            OnDrinkBought?.Invoke();
        }
    }

    public void Details()
    {
        GameObject newPanelDetail = Instantiate(detailsPrefab, GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>().transform);
        newPanelDetail.GetComponent<DetailPanel>().OpenDetails(detalles);
    }
}
