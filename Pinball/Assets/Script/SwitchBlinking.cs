using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBlinking : MonoBehaviour
{
    public Color colorOn = Color.white;  // Warna saat switch aktif
    public Color colorOff = Color.black; // Warna saat switch nonaktif
    public AudioClip soundOn;  // Suara saat switch diaktifkan
    public AudioClip soundOff; // Suara saat switch dinonaktifkan
    private AudioSource audioSource;
    private Renderer rend;
    private bool isBallOnSwitch = false;
    private bool isSwitchOn = false;
    private bool isBlinking = false;

    void Start()
    {
        rend = GetComponent<Renderer>();
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(BlinkSwitch()); // Mulai proses blinking
    }

    void Update()
    {
        // Jika bola tidak menyentuh switch, lakukan blinking
        if (!isBallOnSwitch && !isBlinking)
        {
            StartCoroutine(BlinkSwitch());
        }
    }

    // Fungsi untuk mendeteksi ketika Ball menyentuh switch
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);  // Debug
        if (collision.gameObject.CompareTag("Ball"))
        {
            Debug.Log("Ball touched the switch!");  // Debug
            isBallOnSwitch = true;
            isBlinking = false;
            StopCoroutine(BlinkSwitch()); // Hentikan blinking saat Ball menyentuh

            if (isSwitchOn)
            {
                rend.material.color = colorOff;
                audioSource.PlayOneShot(soundOff);
                isSwitchOn = false;
                Debug.Log("Switch turned off");  // Debug
            }
            else
            {
                rend.material.color = colorOn;
                audioSource.PlayOneShot(soundOn);
                isSwitchOn = true;
                Debug.Log("Switch turned on");  // Debug
            }
        }
    }

    // Fungsi untuk mendeteksi ketika Ball berhenti menyentuh switch
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            isBallOnSwitch = false;
            isBlinking = false;
            StartCoroutine(BlinkSwitch()); // Mulai kembali blinking setelah Ball tidak menyentuh
        }
    }

    // Coroutine untuk blinking switch
    IEnumerator BlinkSwitch()
    {
        isBlinking = true;
        while (!isBallOnSwitch)
        {
            // Ganti warna switch antara hitam dan putih
            rend.material.color = colorOn;
            yield return new WaitForSeconds(0.5f); // Tunggu 0.5 detik
            rend.material.color = colorOff;
            yield return new WaitForSeconds(0.5f); // Tunggu 0.5 detik
        }
        isBlinking = false;
    }
}
