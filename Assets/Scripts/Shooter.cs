using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 2f;
    [SerializeField] float baseFireRate = .2f;

    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float fireRateVariance = 0f;
    [SerializeField] float minFireRate = 0.1f;

    [HideInInspector] public bool isFiring;

    Coroutine fireCoroutine;
    AudioPlayer audioPlayer;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }


    void Start()
    {
        if (useAI)
        {
            isFiring = true;
        }
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isFiring && fireCoroutine == null)
        {
            fireCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!isFiring && fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
            fireCoroutine = null;
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject instance = Instantiate(projectilePrefab,
                transform.position,
                Quaternion.identity);

            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                rb.velocity = transform.up * projectileSpeed;
            }

            Destroy(instance, projectileLifetime);

            float timeToNextLaser = Random.Range(baseFireRate - fireRateVariance,
                baseFireRate + fireRateVariance);
            timeToNextLaser = Mathf.Clamp(timeToNextLaser, minFireRate, float.MaxValue);
            audioPlayer.PlayShootingClip();

            yield return new WaitForSeconds(timeToNextLaser);
        }
    }
}
