using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerScript : MonoBehaviour
{
    // Music for background
    public AudioClip backgroundMusic;
    // Audio Source
    private AudioSource audioSource;
    // Camera Component
    private Camera mainCamera;
    // Флаг возможности движения камеры
    public bool doMovement = true;
    // Скорость перемещения камеры
    public float panSpeed = 50f;
    // Чувствительность границ камеры к перемещению
    public float panBorderThickness = 20f;

    void Start()
    {
        mainCamera = Camera.main;
        audioSource = mainCamera.gameObject.AddComponent<AudioSource>();
        audioSource.clip = backgroundMusic;
        audioSource.volume = 0.7f;
        if (backgroundMusic != null)
            audioSource.Play(10);
        
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            doMovement = !doMovement;
        }

        if (!doMovement)
        {
            return;
        }

        if (Input.GetMouseButton(0) && Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            mainCamera.transform.Translate(Vector3.up * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetMouseButton(0) && Input.mousePosition.y < panBorderThickness)
        {
            mainCamera.transform.Translate(Vector3.down * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetMouseButton(0) && Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            mainCamera.transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetMouseButton(0) && Input.mousePosition.x < panBorderThickness)
        {
            mainCamera.transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }
    }
 }
