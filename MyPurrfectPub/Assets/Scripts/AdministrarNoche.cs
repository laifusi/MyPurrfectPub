using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AdministrarNoche : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI clientes_totales;

    [SerializeField] private TextMeshProUGUI clientes_atendidos;

    [SerializeField] private TextMeshProUGUI clientes_desatendidos;
    [SerializeField] private TextMeshProUGUI clientes_desatendidos_prestige;

    [SerializeField] private TextMeshProUGUI bebidas_consumidas;
    [SerializeField] private TextMeshProUGUI bebidas_consumidas_coins;
    [SerializeField] private TextMeshProUGUI bebidas_consumidas_prestige;

    [SerializeField] private TextMeshProUGUI alimentos_consumidos;
    [SerializeField] private TextMeshProUGUI alimentos_consumidos_coins;
    [SerializeField] private TextMeshProUGUI alimentos_consumidos_prestige;

    [SerializeField] private TextMeshProUGUI coste_empleados_coins;
    [SerializeField] private TextMeshProUGUI coste_empleados_prestige;

    [SerializeField] private TextMeshProUGUI ganancias_eventos_coins;
    [SerializeField] private TextMeshProUGUI ganancias_eventos_prestige;

    [SerializeField] private TextMeshProUGUI ganancias_totales_coins;
    [SerializeField] private TextMeshProUGUI ganancias_totales_prestige;

    [SerializeField] private TextMeshProUGUI recursos_totales_prestige;
    [SerializeField] private TextMeshProUGUI recursos_totales_coins;
    [SerializeField] private int i_recursos_totales_coins;
    [SerializeField] private int i_recursos_totales_prestige;

    [SerializeField] private AudioClip buttonClip;

    public void GetClientesTotales(int ct)
    {
        clientes_totales.text = ct.ToString();
    }

    public void GetClientesAtendidos(int ca)
    {
        clientes_atendidos.text = ca.ToString();
    }

    public void GetClientesDesatendidos(int ct, int prestige)
    {
        clientes_desatendidos.text = ct.ToString();
        clientes_desatendidos_prestige.text = prestige.ToString();
    }

    public void GetBebidasConsumidas(int ct, int coins, int prestige)
    {
        bebidas_consumidas.text = ct.ToString();
        bebidas_consumidas_coins.text = coins.ToString();
        bebidas_consumidas_prestige.text = prestige.ToString();
    }

    public void GetAlimentosConsumidos(int ct, int coins, int prestige)
    {
        alimentos_consumidos.text = ct.ToString();
        alimentos_consumidos_coins.text = coins.ToString();
        alimentos_consumidos_prestige.text = prestige.ToString();
    }

    public void GetCosteEmpleados(int coins, int prestige)
    {

        coste_empleados_coins.text = coins.ToString();
        coste_empleados_prestige.text = prestige.ToString();
    }

    public void GetGananciasEvento(int coins, int prestige)
    {
        ganancias_eventos_coins.text = coins.ToString();
        ganancias_eventos_prestige.text = prestige.ToString();
    }

    public void GetGananciasTotales(int coins, int prestige)
    {
        ganancias_totales_coins.text = coins.ToString();
        ganancias_totales_prestige.text = prestige.ToString();
    }

    public void GetRecursosTotales(int coins, int prestige)
    {
        recursos_totales_coins.text = coins.ToString();
        i_recursos_totales_coins = coins;
        recursos_totales_prestige.text = prestige.ToString();
        i_recursos_totales_prestige = prestige;
    }

    public void ReturnBar()
    {
        if (i_recursos_totales_prestige >= 100)
        {
            GameManager.instance.Perder();
        }
        else if (i_recursos_totales_coins < 0)
        {
            GameManager.instance.Perder();
        }
        else
        {
        GameManager.instance.PlaySound(buttonClip);
        GameManager.instance.ResetAdministratorNight();
        Destroy(gameObject);
        }
    }
}
