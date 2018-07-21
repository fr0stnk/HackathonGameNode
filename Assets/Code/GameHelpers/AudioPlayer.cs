using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource Source;

    public AudioClip ButtonClick;

    public AudioClip Building;

    public AudioClip TrainingUnits;

    public static AudioPlayer Instance;

    private void Start ()
    {
        Instance = this;
    }

    public void PlayButtonClick()
    {
        this.Source.PlayOneShot(this.ButtonClick);
    }

    public void PlayBuilding()
    {
        this.Source.PlayOneShot(this.Building, 0.6f);
    }

    public void PlayTraining()
    {
        this.Source.PlayOneShot(this.TrainingUnits, 0.3f);
    }
}
