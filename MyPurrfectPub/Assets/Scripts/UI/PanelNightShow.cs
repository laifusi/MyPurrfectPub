using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PanelNightShow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nombre;

    [SerializeField] private TextMeshProUGUI precio;

    [SerializeField] private TextMeshProUGUI prestigio;

    [SerializeField] private string detalles;

    [SerializeField] private GameObject detailsPrefab;

    [SerializeField] private NightShowSO nshowAsociado;

    [SerializeField] private Button comprar;

    // Update is called once per frame
    void Update()
    {
        if(Inventory.instance.ShowActive)
        {
            comprar.enabled = false;
        }

        else
        {
            comprar.enabled = true;
        }
    }

    public void AsignarValoresNightShow(NightShowSO nshow)
    {
        nombre.text = nshow.name;
        precio.text = nshow.cost.ToString();
        prestigio.text = nshow.prestige.ToString();
        detalles = nshow.description;
        nshowAsociado = nshow;
    }

    public void Comprar()
    {
        if(nshowAsociado.cost <= Inventory.instance.moneycount)
        {
            Inventory.instance.AddShow(nshowAsociado);
            Inventory.instance.RemoveMichiCoins(nshowAsociado.cost);
        }
    }

    public void Details()
    {
        GameObject newPanelDetail = Instantiate(detailsPrefab, GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>().transform);
        newPanelDetail.GetComponent<DetailPanel>().OpenDetails(detalles);
    }
}
