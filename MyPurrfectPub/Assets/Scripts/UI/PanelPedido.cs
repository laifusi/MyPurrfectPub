using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PanelPedido : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI num_pedido;

    [SerializeField] private TextMeshProUGUI descripcion;

    [SerializeField] private TextMeshProUGUI estado;

    [SerializeField] private Image imagenComida1;

    [SerializeField] private Image imagenRespuesta1;

    [SerializeField] private Image imagenComida2;

    [SerializeField] private Image imagenRespuesta2;

    [SerializeField] private Sprite correct, incorrect;

    [SerializeField] private GameObject imagenes, texto;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AsignarValoresCliente(Client c)
    {

        num_pedido.text = c.id_pedido + "º Pedido";

        if (c.consumiciones.Count > 0)
        {
            texto.SetActive(false);
            imagenes.SetActive(true);

            imagenComida1.sprite = c.consumiciones[0];

            if (c.disponibilidad_consumiciones[0])
                imagenRespuesta1.sprite = correct;
            else
                imagenRespuesta1.sprite = incorrect;

            if (c.consumiciones.Count > 1)
            {
                imagenComida2.sprite = c.consumiciones[1];

                if (c.disponibilidad_consumiciones[1])
                    imagenRespuesta2.sprite = correct;
                else
                    imagenRespuesta2.sprite = incorrect;
            }
            else
            {
                imagenComida2.color = new Color(imagenComida2.color.r, imagenComida2.color.g, imagenComida2.color.b, 0);
                imagenRespuesta2.color = new Color(imagenRespuesta2.color.r, imagenRespuesta2.color.g, imagenRespuesta2.color.b, 0); ;
            }
        }
        else
        {
            texto.SetActive(true);
            imagenes.SetActive(false);

            descripcion.text = c.descripcion;
        }


        if(c.estado)
        {
            estado.text = "Realizado";
            estado.color = new Color32(0, 168, 0, 255);
        }
        else
        {
            estado.text = "Rechazado";
            estado.color = Color.red;
        }
    }
}
