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

    [SerializeField] private Image image;

    [SerializeField] private string detalles;

    [SerializeField] private GameObject detailsPrefab;

    [SerializeField] private NightShowSO nshowAsociado;

    [SerializeField] private GameObject comprar;

    [SerializeField] private GameObject remove;

    [SerializeField] private AudioClip buyClip;
    [SerializeField] private AudioClip cantBuyClip;
    [SerializeField] private AudioClip removeClip;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Inventory.instance.ShowDetails.name_show == nshowAsociado.name)
        {
            comprar.SetActive(false);
            remove.SetActive(true);
        }

        else
        {
            comprar.SetActive(true);
            remove.SetActive(false);
        }
    }

    public void AsignarValoresNightShow(NightShowSO nshow)
    {
        nombre.text = nshow.name;
        precio.text = nshow.cost.ToString();
        prestigio.text = nshow.prestige.ToString();
        image.sprite = nshow.image;
        detalles = nshow.description;
        nshowAsociado = nshow;
    }

    public void Comprar()
    {
        if(nshowAsociado.cost <= GameManager.instance.GetCoins())
        {
            Inventory.instance.AddShow(nshowAsociado);
            GameManager.instance.AddCoins(nshowAsociado.cost * -1);
            audioSource.PlayOneShot(buyClip);
        }
        else
        {
            audioSource.PlayOneShot(cantBuyClip);
        }
    }

    public void CancelarShow()
    {
        Inventory.instance.RemoveShow();
        GameManager.instance.AddCoins(nshowAsociado.cost);
        audioSource.PlayOneShot(removeClip);
    }

    public void Details()
    {
        GameObject newPanelDetail = Instantiate(detailsPrefab, GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>().transform);
        newPanelDetail.GetComponent<DetailPanel>().OpenDetails(detalles);
    }
}
