using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DetailPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI detailsText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenDetails(string details)
    {
        detailsText.text = details;
    }

    public void CloseDetails()
    {
        Destroy(this.gameObject);
    }

}
