using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdministratorNightShow : MonoBehaviour
{
    [SerializeField] private List<NightShowSO> lstNightShow;
    [SerializeField] private Transform panel;
    [SerializeField] private GameObject PanelPrefab;
    // Start is called before the first frame update
    void Start()
    {
        foreach (NightShowSO nshow in lstNightShow)
        {
            GameObject newPanel = Instantiate(PanelPrefab, panel);
            newPanel.GetComponent<PanelNightShow>().AsignarValoresNightShow(nshow);

            //newButton.onClick.AddListener()
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
