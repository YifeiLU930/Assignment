using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : Enemy
{
    public float projectileFireRate;
    float timeSinceLastFire;
    Shoot shootScript;
     AudioSourceManager asm;


     public AudioClip TurretDieSound;

    // Start is called before the first frame update



    public override void Start()
    {


        base.Start();
          asm = GetComponent<AudioSourceManager>();
        shootScript = GetComponent<Shoot>();
        shootScript.OnProjectileSpawned.AddListener(UpdateTimeSinceLastFire);

    }

    private void OnDisable()
    {
        shootScript.OnProjectileSpawned.RemoveListener(UpdateTimeSinceLastFire);
    }



    void Update()
    {
        AnimatorClipInfo[] currentClips = anim.GetCurrentAnimatorClipInfo(0);

        if (currentClips[0].clip.name != "Fire")
        {
            if (Time.time >= timeSinceLastFire + projectileFireRate)
            {
                anim.SetTrigger("Fire");

            }
        }
    }



    void UpdateTimeSinceLastFire()
    {
        timeSinceLastFire = Time.time;
    }

   private void OnTriggerEnter2D(Collider2D collision)
   {
        if (collision.CompareTag("PlayerProjectile"))
       {

            Destroy(gameObject);

       }

        if (TurretDieSound)
        
    {
    asm.PlayOneShot(TurretDieSound, false);
        }
    }

}
