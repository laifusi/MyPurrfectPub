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

    [SerializeField] private GameManager gameManager;

    public void OnEnable()
    {
        UpdateText(gameManager.GetPrestigeLevel());
    }

    public void UpdateText(int level)
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
    }
}
