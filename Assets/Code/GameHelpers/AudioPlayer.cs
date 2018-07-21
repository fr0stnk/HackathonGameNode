using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource Source;

    public AudioClip ButtonClick;

    public static AudioPlayer Instance;

    private void Start ()
    {
        Instance = this;
    }

    public void PlayButtonClick()
    {
        this.Source.PlayOneShot(this.ButtonClick);
    }
}
