using UnityEngine;
using UnityEngine.UI;

public class AudioMachine : MonoBehaviour
{
    [SerializeField] AudioSource sourceBGM;
    [SerializeField] AudioSource sourceSFX;
    [SerializeField] AudioClip defaultBGM;

    void Start()
    {
        PlayBGM(defaultBGM);
    }

    public void PlayBGM(AudioClip clip)
    {
        sourceBGM.Stop();
        sourceBGM.clip = clip;
        sourceBGM.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sourceSFX.Stop();
        sourceSFX.clip = clip;
        sourceSFX.Play();
    }

    public void ButtonHover(AudioClip clip, AudioSource obj)
    {
        if (obj.GetComponent<Button>().isActiveAndEnabled) PlaySFX(clip);
    }
}
