using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdministradorPedidos : MonoBehaviour
{

    [SerializeField] private Transform panel;
    [SerializeField] private GameObject PanelPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fillTable()
    {

        foreach(Client c in GameManager.instance.listado_clientes)
        {
            GameObject newPanel = Instantiate(PanelPrefab, panel);
            newPanel.GetComponent<PanelPedido>().AsignarValoresCliente(c);
        }
    }

    public void OnEnable()
    {
        fillTable();
    }

}
