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

    [SerializeField] private GameObject detailsPrefab;

    // Update is called once per frame
    void Update()
    {

    }

    public void AsignarValoresFood(FoodSO comida)
    {
        nombre.text = comida.name;
        precio.text = comida.cost.ToString();
        prestigio.text = comida.prestige.ToString();
    }
}
