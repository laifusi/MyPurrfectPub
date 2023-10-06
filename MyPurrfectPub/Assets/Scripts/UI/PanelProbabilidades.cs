using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PanelProbabilidades : MonoBehaviour
{
    [SerializeField] private TMP_Text comun;

    [SerializeField] private TMP_Text poco_comun;

    [SerializeField] private TMP_Text rara;

    [SerializeField] private TMP_Text muyrara;

    [SerializeField] private TMP_Text level_local;

    [SerializeField] private TMP_Text Clients_amount;

    [SerializeField] private TMP_Text purrstige_lost;

    [SerializeField] private GameManager gameManager;

    public void OnEnable()
    {
        UpdateText(gameManager.GetPrestigeLevel(), gameManager.GetClientAmount(), gameManager.GetPrestigeLost());
    }

    public void UpdateText(int level, int c_cantidad, int p_lost)
    {
        switch(level)
        {
            case 1:
                comun.text = "70%";
                poco_comun.text = "20%";
                rara.text = "7%";
                muyrara.text = "3%";
                break;
            case 2:
                comun.text = "55%";
                poco_comun.text = "26%";
                rara.text = "14%";
                muyrara.text = "5%";
                break;
            case 3:
                comun.text = "35%";
                poco_comun.text = "35%";
                rara.text = "20%";
                muyrara.text = "10%";
                break;
            case 4:
                comun.text = "20%";
                poco_comun.text = "30%";
                rara.text = "30%";
                muyrara.text = "20%";
                break;
        }

        level_local.text = level.ToString();

        Clients_amount.text = c_cantidad.ToString();

        purrstige_lost.text = p_lost.ToString();

    }
}
