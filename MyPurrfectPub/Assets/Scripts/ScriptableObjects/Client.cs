using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{
    [SerializeField] public int id_pedido = 0;

    [SerializeField] public List<Sprite> consumiciones = new List<Sprite>();

    [SerializeField] public List<bool> disponibilidad_consumiciones = new List<bool>();

    [SerializeField] public string descripcion = "";

    [SerializeField] public bool estado = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddConsumicines(int id, string c, bool disponibilidad, bool activarTexto)
    {
        id_pedido = id;

        if(!activarTexto)
        {
            foreach (Inventory.inventoryDrink d in Inventory.instance.drinklist)
            {
                if (d.name_drink == c)
                    consumiciones.Add(d.imagen);
            }

            foreach (Inventory.inventoryFood f in Inventory.instance.foodlist)
            {
                if (f.name_food == c)
                    consumiciones.Add(f.imagen);
            }

            disponibilidad_consumiciones.Add(disponibilidad);
        }

        else
        {
            descripcion = c;
        }

        
        if (!disponibilidad)
            estado = false;
    }

    public void ResetLists()
    {
        consumiciones.Clear();
        disponibilidad_consumiciones.Clear();
    }
}
