using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool applyCamShake;
    [SerializeField] bool isPlayer;
    [SerializeField] int health = 50;
    [SerializeField] int score = 50;
    [SerializeField] ParticleSystem hitFx;
    
    LevelManager levelManager;
    CamShake camShake;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;

    void Awake()
    {
        camShake = Camera.main.GetComponent<CamShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        if(damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayHitFx();
            audioPlayer.PlayDamageClip();
            ShakeCam();
            damageDealer.Hit();
        }
    }

    public int GetHealth()
    {
        return health;
    }


    void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (!isPlayer)
        {
            scoreKeeper.ModifyScore(score);
        }
        else
        {
            levelManager.LoadGameOver();
        }

        Destroy(gameObject);
    }


    void PlayHitFx()
    {
        if(hitFx != null)
        {
            ParticleSystem instance = Instantiate(hitFx, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, 0.6f);
            //Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    void ShakeCam()
    {
        if(camShake != null && applyCamShake)
        {
            camShake.Play();
        }
    }
}
