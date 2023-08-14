using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenadeFunction : MonoBehaviour
{
    public GameObject explosionPrefab;
    public AudioClip explosionSound;
    [SerializeField] private AudioSource audioSource;
    public float explosionRadius = 5f; // Radius in which explosion affects
    public int damageAmount = 50; // Damage amount applied to objects in the explosion radius

    private void Start()
    {
        if (!audioSource)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        CreateExplosion(transform.position);

        if (explosionSound && audioSource)
        {
            yield return new WaitForSeconds(explosionSound.length);
        }

        Destroy(gameObject);
    }

    private void CreateExplosion(Vector3 position)
    {
        if (explosionSound && audioSource)
        {
            Debug.Log("Playing boom sound");
            audioSource.PlayOneShot(explosionSound);
        }

        GameObject explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        ParticleSystem particles = explosion.GetComponent<ParticleSystem>();
        var mainModule = particles.main;
        mainModule.loop = false;
        Destroy(explosion, mainModule.duration);

        DealExplosionDamage(position);  // Call the function to deal damage
    }

    private void DealExplosionDamage(Vector3 explosionCenter)
    {
        // Get all colliders within the explosion radius
        Collider[] hitColliders = Physics.OverlapSphere(explosionCenter, explosionRadius);

        foreach (Collider hitCollider in hitColliders)
        {
            IDamageable damageableObject = hitCollider.GetComponent<IDamageable>();
            if (damageableObject != null)
            {
                damageableObject.TakeDamage(damageAmount);
            }
        }
    }

    public void LetGoOfGrenade()
    {
        StartCoroutine(DestroyAfterDelay(5f));
    }
}
