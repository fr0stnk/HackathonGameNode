using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerScript : MonoBehaviour
{
    // Music for background
    public AudioClip BackgroundMusic;
    // Audio Source
    private AudioSource _audioSource;

    // Флаг возможности движения камеры
    public bool DoMovement = true;
    // Скорость перемещения камеры
    public float DragSpeed = 10f;

    void Start()
    {
        _audioSource = Camera.main.gameObject.AddComponent<AudioSource>();
        _audioSource.clip = BackgroundMusic;
        _audioSource.volume = 0.7f;
        if (BackgroundMusic != null)
            _audioSource.Play();
    }

    void LateUpdate ()
    {
        if (!DoMovement)
        {
            return;
        }

        float speed = DragSpeed * Time.deltaTime;

        if (Input.GetMouseButton(0))
        {
            Camera.main.transform.position += new Vector3(-Input.GetAxis("Mouse X") * speed, -Input.GetAxis("Mouse Y") * speed, 0);
        }
    }
 }
