using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    [Header("Rifle Things")]
    public Camera cam;
    public float giveDamageOf = 10f;
    public float shootingRange = 100f;
    // public float fireCharge = 15f;
    // private float nextTimeToShoot = 0f;
    // public Animator animator;
    // public PlayerScript player;
    // public Transform hand;
    
    [Header("Rifle Ammunition and shooting")]
    private int maximumAmmunition = 32;
    public int mag = 10;
    private int presentAmmunition;
    public float reloadingTime = 1.3f;
    private bool setReloading = false;
    
    [Header("Rifle Effects")]
    public ParticleSystem muzzleSpark;
    public GameObject WoodedEffect;
    public GameObject goreEffect;
    
    private void Start()
    {
        presentAmmunition = maximumAmmunition;
    }
    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            
            Shoot();
        }
    }
    private void Shoot()
    {
        if(mag == 0)
        {
            return;
        }   

        presentAmmunition --;

        if(presentAmmunition == 0)
        {
            mag--;
            presentAmmunition = maximumAmmunition;
        }

        AmmoCount.occurrence.UpdateAmmoText(presentAmmunition);
        AmmoCount.occurrence.UpdateMagText(mag);

        muzzleSpark.Play();
        RaycastHit hitInfo;

        
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, shootingRange))
        {
            Debug.Log(hitInfo.transform.name);

            ObjectToHit objectToHit = hitInfo.transform.GetComponent<ObjectToHit>();
            Zombie1 zombie1 = hitInfo.transform.GetComponent<Zombie1>();
            Zombie2 zombie2 = hitInfo.transform.GetComponent<Zombie2>();

            if(objectToHit != null)
            {
                objectToHit.ObjectHitDamage(giveDamageOf);
                GameObject WoodGo = Instantiate(WoodedEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(WoodGo, 1f);
            }
            else if(zombie1 != null)
            {
                zombie1.zombieHitDamage(giveDamageOf);
                GameObject goreEffectGo = Instantiate(goreEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(goreEffectGo, 1f);
            }
            else if(zombie2 != null)
            {
                zombie2.zombieHitDamage(giveDamageOf);
                GameObject goreEffectGo = Instantiate(goreEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(goreEffectGo, 1f);
            }
        }
        
    }

    
}
