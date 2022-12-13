using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class Shoot : MonoBehaviour
{
    SpriteRenderer sr;
    AudioSourceManager asm;


    public float projectileSpeed;
    public Transform spawnPointRight;
    public Transform spawnPointLeft;

    public Projectile projectilePrefab;

    public UnityEvent OnProjectileSpawned;

   
    public AudioClip fireSound;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        asm = GetComponent<AudioSourceManager>();

        if (projectileSpeed <= 0)
            projectileSpeed = 7.0f;

        if (!spawnPointLeft || !spawnPointRight || !projectilePrefab)
            Debug.Log("Setup default values on:" + gameObject.name);



    }

    public void Fire()

    {
        if(!sr.flipX)
        {
            Projectile curProjectile = Instantiate(projectilePrefab, spawnPointRight.position, spawnPointRight.rotation);
            curProjectile.speed = projectileSpeed;
        }
        else
        {
            Projectile curProjectile = Instantiate(projectilePrefab, spawnPointLeft.position, spawnPointLeft.rotation);
            curProjectile.speed = -projectileSpeed;

        }

        if (asm)
        asm.PlayOneShot(fireSound, false);
        OnProjectileSpawned?.Invoke();

    }
}
