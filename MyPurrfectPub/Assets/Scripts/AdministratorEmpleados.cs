using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdministratorEmpleados : MonoBehaviour
{

    [SerializeField] private List<EmployeeSO> lstEmployee;
    [SerializeField] private Transform panel;
    [SerializeField] private GameObject PanelPrefab;

    void Start()
    {
        foreach (EmployeeSO employee in lstEmployee)
        {
            employee.Reset();

            GameObject newPanel = Instantiate(PanelPrefab, panel);
            newPanel.GetComponent<PanelEmployee>().AsignarValoresEmpleado(employee);
        }
    }
}
