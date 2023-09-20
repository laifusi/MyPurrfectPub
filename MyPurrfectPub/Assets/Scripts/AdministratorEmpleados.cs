using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdministratorEmpleados : MonoBehaviour
{

    [SerializeField] private List<EmployeeSO> lstEmployee;
    [SerializeField] private Transform panel;
    [SerializeField] private GameObject PanelPrefab;
    // Start is called before the first frame update
    void Start()
    {
        foreach (EmployeeSO employee in lstEmployee)
        {
            GameObject newPanel = Instantiate(PanelPrefab, panel);
            newPanel.GetComponent<PanelEmployee>().AsignarValoresEmpleado(employee);
            
            //newButton.onClick.AddListener()
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
