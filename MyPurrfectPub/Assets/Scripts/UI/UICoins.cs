using UnityEngine;
using TMPro;

public class UICoins : MonoBehaviour
{
    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();

        GameManager.OnCoinsChange += ChangeText;
    }

    private void ChangeText(int value)
    {
        text.SetText(value.ToString());
    }

    private void OnDestroy()
    {
        GameManager.OnCoinsChange -= ChangeText;
    }
}
