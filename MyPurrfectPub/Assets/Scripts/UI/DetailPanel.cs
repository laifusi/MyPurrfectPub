using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DetailPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI detailsText;
    [SerializeField] private AudioClip buttonClip;

    public void OpenDetails(string details)
    {
        detailsText.text = details;
    }

    public void CloseDetails()
    {
        GameManager.instance.PlaySound(buttonClip);
        Destroy(this.gameObject);
    }

}
