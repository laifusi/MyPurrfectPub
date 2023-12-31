using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PanelEmployee : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI nombre;

    [SerializeField] private TextMeshProUGUI rol;

    [SerializeField] private TextMeshProUGUI capacidad;

    [SerializeField] private TextMeshProUGUI precio;

    [SerializeField] private TextMeshProUGUI prestigio;

    [SerializeField] private string detalles;

    [SerializeField] private Button comprar;
    [SerializeField] private Button despedir;

    [SerializeField] private Image image;

    [SerializeField] private GameObject detailsPrefab;

    [SerializeField] private EmployeeSO empleadoAsociado;

    [SerializeField] private AudioClip buyClip;
    [SerializeField] private AudioClip removeClip;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (empleadoAsociado.bought)
        {
            comprar.gameObject.SetActive(false);
            despedir.gameObject.SetActive(true);
        }
        else
        {
            comprar.gameObject.SetActive(true);
            despedir.gameObject.SetActive(false);
        }
    }

    public void AsignarValoresEmpleado(EmployeeSO empleado)
    {
        nombre.text = empleado.name;
        rol.text = empleado.rol;
        if(empleado.rol == "Coctelero" || empleado.rol == "Coctelera")
            capacidad.text = empleado.capacity.ToString() + " bebidas m�ximas por noche";
        else if(empleado.rol == "Camarero" || empleado.rol == "Camarera")
            capacidad.text = empleado.capacity.ToString() + " comidas m�ximas por noche";
        precio.text = empleado.costPerTurn.ToString() + "/turno";
        prestigio.text = empleado.prestigePerTurn.ToString() + "/turno";
        detalles = empleado.description;
        image.sprite = empleado.image;

        empleadoAsociado = empleado;
    }

    public void Contratar()
    {
        empleadoAsociado.bought = true;
        Inventory.instance.AddEmployee(empleadoAsociado);
        audioSource.PlayOneShot(buyClip);
    }

    public void Despedir()
    {
        empleadoAsociado.bought = false;
        Inventory.instance.RemoveEmployee(empleadoAsociado);
        audioSource.PlayOneShot(removeClip);
    }

    public void Details()
    {
        GameObject newPanelDetail = Instantiate(detailsPrefab, GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>().transform);
        newPanelDetail.GetComponent<DetailPanel>().OpenDetails(detalles);
    }
}
