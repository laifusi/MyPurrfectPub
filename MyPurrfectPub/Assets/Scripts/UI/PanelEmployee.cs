using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PanelEmployee : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI nombre;

    [SerializeField] private TextMeshProUGUI rol;

    [SerializeField] private TextMeshProUGUI precio;

    [SerializeField] private TextMeshProUGUI prestigio;

    [SerializeField] private Button comprar;

    [SerializeField] private GameObject detailsPrefab;

    // Update is called once per frame
    void Update()
    {

    }

    public void AsignarValoresEmpleado(EmployeeSO empleado)
    {
        nombre.text = empleado.name;
        rol.text = empleado.rol;
        precio.text = empleado.costPerTurn.ToString();
        prestigio.text = empleado.prestigePerTurn.ToString();

        if(empleado.bought)
        {
            comprar.enabled = false;
        }
        else
        {
            comprar.enabled = true;
        }
    }
}
