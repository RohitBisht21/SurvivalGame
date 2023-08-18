using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public ParticleSystem muzzleFlash;
    private ParticleSystem impactParticles;
    public GameObject impactEffect;
    public Camera fpsCam;

    private void Start()
    {
        impactParticles = impactEffect.GetComponent<ParticleSystem>();
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && PickUpController.Instance.gunPicked == true)
        {
            Shoot();
        }
    }
    void Shoot()
    {
        muzzleFlash.Play();
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            impactEffect.transform.position= hit.point;
            impactEffect.transform.rotation = Quaternion.LookRotation(hit.normal);
            impactParticles.Stop();
            impactParticles.Play();


        }

    }
}
