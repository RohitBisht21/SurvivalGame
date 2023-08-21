using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : MonoBehaviour
{
    public float damage = 2f;
    public float range = 100f;
    public ParticleSystem muzzleFlash;
    private ParticleSystem impactParticles;
    public GameObject impactEffect;
    public Camera fpsCam;
    public float fireCooldown = 0.5f;
    private float lastShotTime = 0f;

    private void Start()
    {
        impactParticles = impactEffect.GetComponent<ParticleSystem>();
    }
    void Update()
    {
        if (Input.GetButton("Fire1") && PickUpController.Instance.gunPicked == true)
        {
            Shoot();
        }
    }
    void Shoot()
    {
        if (Time.time - lastShotTime >= fireCooldown)
        {
            lastShotTime = Time.time; // Update the last shot time
            muzzleFlash.Play();
            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name);

                EnemyController enemyController = hit.transform.GetComponent<EnemyController>();
                if (enemyController != null)
                {
                    enemyController.TakeEnemyDamage(damage);

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
