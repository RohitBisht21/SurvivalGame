using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : MonoBehaviour
{
    public float damage;
    public float range = 100f;
    public ParticleSystem muzzleFlash;
    private ParticleSystem impactParticles;
    public GameObject impactEffect;
    public Camera fpsCam;
    public float fireCooldown = 0.25f;
    private float lastShotTime = 0f;

    private void Start()
    {
        damage = 2f;
        impactParticles = impactEffect.GetComponent<ParticleSystem>();
    }
    void FixedUpdate()
    {
        if (Input.GetButton("Fire1") && PickUpController.Instance.gunPicked == true)
        {
            Shoot();
        }
        else
        {
            ManageControls.Instance.animator.SetBool("shoot", false); // Stop the shoot animation when not firing
        }
    }
    void Shoot()
    {
        if (Time.time - lastShotTime >= fireCooldown)
        {
            lastShotTime = Time.time; // Update the last shot time
            muzzleFlash.Play();
            RaycastHit hit;
            AudioManager.Instance.Play("GunFire");

            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                ManageControls.Instance.animator.SetBool("shoot", true);
                Debug.Log(hit.transform.name);

                EnemyController enemyController = hit.transform.GetComponent<EnemyController>();
                if (enemyController != null)
                {
                    enemyController.TakeEnemyDamage(damage, hit.point);

                    if (enemyController.bloodParticles != null)
                    {
                        enemyController.bloodParticles.transform.position = hit.point;
                        enemyController.bloodParticles.Play();
                    }
                }

                impactEffect.transform.position = hit.point;
                impactEffect.transform.rotation = Quaternion.LookRotation(hit.normal);
                impactParticles.Stop();
                impactParticles.Play();
            }
           
        }
    }
}
