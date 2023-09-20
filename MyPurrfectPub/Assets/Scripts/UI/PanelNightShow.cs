using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PanelNightShow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nombre;

    [SerializeField] private TextMeshProUGUI precio;

    [SerializeField] private TextMeshProUGUI prestigio;

    [SerializeField] private GameObject detailsPrefab;

    // Update is called once per frame
    void Update()
    {

    }

    public void AsignarValoresNightShow(NightShowSO nshow)
    {
        nombre.text = nshow.name;
        precio.text = nshow.cost.ToString();
        prestigio.text = nshow.prestige.ToString();
    }
}
