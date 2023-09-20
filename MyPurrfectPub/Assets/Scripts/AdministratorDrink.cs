using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdministratorDrink : MonoBehaviour
{
    [SerializeField] private List<DrinkSO> lstDrink;
    [SerializeField] private Transform panel;
    [SerializeField] private GameObject PanelPrefab;
    // Start is called before the first frame update
    void Start()
    {
        foreach (DrinkSO drink in lstDrink)
        {
            GameObject newPanel = Instantiate(PanelPrefab, panel);
            newPanel.GetComponent<PanelDrink>().AsignarValoresDrink(drink);

            //newButton.onClick.AddListener()
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
