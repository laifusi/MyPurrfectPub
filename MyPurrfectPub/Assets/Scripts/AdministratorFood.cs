using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdministratorFood : MonoBehaviour
{
    [SerializeField] private List<FoodSO> lstFood;
    [SerializeField] private Transform panel;
    [SerializeField] private GameObject PanelPrefab;
    // Start is called before the first frame update
    void Start()
    {
        foreach (FoodSO food in lstFood)
        {
            GameObject newPanel = Instantiate(PanelPrefab, panel);
            newPanel.GetComponent<PanelFood>().AsignarValoresFood(food);

            //newButton.onClick.AddListener()
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
