using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceObject : MonoBehaviour
{
    public float explosionStrength = 100f;
    public AudioClip bounceSound;  // Variabel untuk menyimpan sound effect
    private AudioSource audioSource;  // AudioSource untuk memutar suara

    // Ukuran awal objek
    private Vector3 originalScale;
    // Faktor pengecilan objek
    public float shrinkFactor = 0.5f;
    // Waktu pengecilan
    public float shrinkDuration = 1f;

    // Partikel yang akan diputar saat tabrakan
    public ParticleSystem bounceParticles;

    // Durasi partikel aktif
    public float particleDuration = 1f;

    // Start is called before the first frame update
    void Start()
    {
        // Mendapatkan komponen AudioSource dari objek ini
        audioSource = GetComponent<AudioSource>();
        // Menyimpan ukuran awal objek
        originalScale = transform.localScale;
    }

    // This function is called when the object collides with another
    void OnCollisionEnter(Collision _other)
    {
        // Mengecek apakah objek yang bersentuhan memiliki tag "Ball"
        if (_other.gameObject.CompareTag("Ball"))
        {
            // Memutar suara saat terjadi tabrakan
            PlayBounceSound();

            // Menambahkan poin ke ScoreManager
            ScoreManager.instance.AddPoint();

            // Check if the collided object has a Rigidbody
            if (_other.rigidbody != null)
            {
                // Apply explosion force to the collided object's Rigidbody
                _other.rigidbody.AddExplosionForce(explosionStrength, this.transform.position, 5f);
            }

            // Mengecilkan objek, lalu kembali ke ukuran semula
            StartCoroutine(ShrinkAndGrow());

            // Memunculkan partikel
            PlayBounceParticles();
        }
    }

    // Function to play the bounce sound
    void PlayBounceSound()
    {
        if (bounceSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(bounceSound);
        }
    }

    // Coroutine untuk mengecilkan objek dan kembali ke ukuran semula
    IEnumerator ShrinkAndGrow()
    {
        // Mengecilkan objek
        Vector3 targetScale = originalScale * shrinkFactor;
        float timer = 0f;

        while (timer < shrinkDuration)
        {
            timer += Time.deltaTime;
            transform.localScale = Vector3.Lerp(originalScale, targetScale, timer / shrinkDuration);
            yield return null;
        }

        // Tunggu sejenak setelah objek mengecil
        yield return new WaitForSeconds(0.5f);

        // Memulihkan ukuran objek
        timer = 0f;
        while (timer < shrinkDuration)
        {
            timer += Time.deltaTime;
            transform.localScale = Vector3.Lerp(targetScale, originalScale, timer / shrinkDuration);
            yield return null;
        }
    }

    // Function to play the particle effect
    void PlayBounceParticles()
    {
        if (bounceParticles != null)
        {
            bounceParticles.transform.position = this.transform.position;  // Pastikan partikel muncul di posisi yang tepat
            bounceParticles.Play();  // Memutar partikel

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
        bounceParticles.Stop();
    }
}
