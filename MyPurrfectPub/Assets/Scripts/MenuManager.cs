using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private AudioClip buttonClip;
    [SerializeField] private Animator sceneFadeOutAnimator;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Play()
    {
        StartCoroutine(LoadScene(1, buttonClip));
    }

    private IEnumerator LoadScene(int sceneId, AudioClip clip)
    {
        PlayClickSound(clip);
        if(sceneFadeOutAnimator != null)
            sceneFadeOutAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadScene(sceneId);
    }

    public void PlayClickSound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void Menu()
    {
        StartCoroutine(LoadScene(0, buttonClip));
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
