using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanelFinal : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI titulo;

    [SerializeField] private TextMeshProUGUI texto;

    [SerializeField] private Image[] images;

    [SerializeField] private Sprite imagen_victoria;

    [SerializeField] private Sprite imagen_derrota;

    [SerializeField] private Color victoryColor;
    [SerializeField] private Color failureColor;

    public void Ganar()
    {
        titulo.text = "FELICIDADES POR TU VICTORIA";
        titulo.color = victoryColor;
        foreach (Image im in images)
        {
            im.sprite = imagen_victoria;
        }
        texto.text = "Tu local ha conseguido 100 puntos de PURRSTIGIO, posicionándose entre los mejores locales de la ciudad.";
    }

    public void Perder()
    {
        titulo.text = "DERROTA";
        titulo.color = failureColor;
        foreach (Image im in images)
        {
            im.sprite = imagen_derrota;
        }
        texto.text = "Tu local ha acabado en números rojos. No te frustres, los negocios pueden llegar a ser muy impredecibles, sobretodo los llevados por felinos.";
    }

    public void ButtonMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
