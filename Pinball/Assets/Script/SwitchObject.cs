using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMaterial : MonoBehaviour
{
    // Variabel untuk menyimpan referensi ke Renderer objek yang materialnya ingin diubah
    public Renderer objekRenderer;

    // Warna ketika objek di luar trigger (blinking antara warna hitam dan putih)
    public Color warnaHitam = Color.black;
    public Color warnaPutih = Color.white;

    // Partikel yang akan diputar saat switch disentuh
    public ParticleSystem switchParticles;

    // Durasi partikel aktif
    public float particleDuration = 1f;

    // Variabel untuk menyimpan sound effect
    public AudioClip switchOnSound;  // Suara ketika switch menyala
    public AudioClip switchOffSound; // Suara ketika switch mati
    private AudioSource audioSource;  // AudioSource untuk memutar suara

    // Variabel untuk blinking dan switch status
    private bool isSwitchOn = false;  // Apakah switch dalam keadaan menyala
    private bool isBlinking = true;   // Apakah objek sedang blinking

    // Fungsi ini dipanggil sekali ketika game dimulai
    void Start()
    {
        // Mendapatkan komponen AudioSource dari objek ini
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // Menambahkan AudioSource jika belum ada
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Mulai blinking saat awal
        StartCoroutine(Blinking());
    }

    // Coroutine untuk blinking objek
    IEnumerator Blinking()
    {
        while (isBlinking)
        {
            // Ganti warna antara hitam dan putih setiap 0.5 detik
            objekRenderer.material.color = warnaPutih;
            yield return new WaitForSeconds(0.5f);
            objekRenderer.material.color = warnaHitam;
            yield return new WaitForSeconds(0.5f);
        }
    }

    // Fungsi ini akan dipanggil ketika objek memasuki area trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            // Hentikan blinking
            isBlinking = false;  
            StopCoroutine(Blinking());
            
            // Ganti kondisi switch (nyala/mati)
            if (isSwitchOn)
            {
                // Matikan switch (jadi warna hitam)
                PlaySwitchSound(switchOffSound);
                PlaySwitchParticles();
                objekRenderer.material.color = warnaHitam;
                isSwitchOn = false;
            }
            else
            {
                // Nyalakan switch (jadi warna putih)
                PlaySwitchSound(switchOnSound);
                PlaySwitchParticles();
                objekRenderer.material.color = warnaPutih;
                isSwitchOn = true;
            }
        }
    }

    // Fungsi untuk memutar suara
    void PlaySwitchSound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    // Fungsi untuk memutar partikel
    void PlaySwitchParticles()
    {
        if (switchParticles != null)
        {
            switchParticles.transform.position = this.transform.position;  // Pastikan partikel muncul di posisi yang tepat
            switchParticles.Play();  // Memutar partikel

            // Mulai coroutine untuk menghentikan partikel setelah durasi tertentu
            StartCoroutine(StopParticlesAfterDuration());
        }
    }

    // Coroutine untuk menghentikan partikel setelah waktu tertentu
    IEnumerator StopParticlesAfterDuration()
    {
        // Tunggu selama particleDuration detik
        yield return new WaitForSeconds(particleDuration);

        // Hentikan partikel
        switchParticles.Stop();
    }
}
