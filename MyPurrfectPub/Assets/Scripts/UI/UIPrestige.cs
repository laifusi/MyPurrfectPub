using UnityEngine;
using TMPro;

public class UIPrestige : MonoBehaviour
{
    private TMP_Text text;

    private void Start()
    {
        text = GetComponent<TMP_Text>();

        GameManager.OnPrestigeChange += ChangeText;
    }

    private void ChangeText(int value)
    {
        text.SetText(value.ToString());
    }

    private void OnDestroy()
    {
        GameManager.OnPrestigeChange -= ChangeText;
    }
}
