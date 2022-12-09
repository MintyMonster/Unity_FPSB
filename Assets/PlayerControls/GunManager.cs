using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunManager : MonoBehaviour
{
    // Variables
    [SerializeField] GameObject noAmmo;
    [SerializeField] GameObject reloadHud;
    [SerializeField] ParticleSystem particleSystem;
    [SerializeField] GameObject currentAmmoUI;
    [SerializeField] GameObject totalAmmoUI;
    [SerializeField] Camera cam;
    private bool outOfAmmo = false;
    private bool reload = false;
    private float currentAmmo = 30;
    private float totalAmmo = 210;
    private RaycastHit hit;


    // Getters and setters
    public float CurrentAmmo
    {
        get { return currentAmmo; }
    }

    public float TotalAmmo
    {
        get { return totalAmmo; }
    }

    public void RefillAmmo()
    {
        float an = 30 - currentAmmo;
        float atn = 210 - totalAmmo;
        currentAmmo += an;
        totalAmmo += atn;
        outOfAmmo = false;
        reload = false;
    }

    private void Update()
    {
        currentAmmoUI.GetComponent<Text>().text = currentAmmo.ToString(); // Set current ammo UI text
        totalAmmoUI.GetComponent<Text>().text = totalAmmo.ToString(); // Set total ammo UI text

        if (outOfAmmo) noAmmo.SetActive(true); // Show message if no ammo
        else noAmmo.SetActive(false); // Disable message

        if (reload) reloadHud.SetActive(true); // Show mesage if need to reload
        else reloadHud.SetActive(false); // Disable messed

        if ((currentAmmo <= 0) && !(totalAmmo > 0) && (!reload)) outOfAmmo = true; // Check if out of ammo entirely
        if ((currentAmmo <= 0) && (totalAmmo > 0) && (!outOfAmmo)) reload = true; // Check if need to reload
    }

    public void Shoot()
    {    
        if(!outOfAmmo)
        {
            if(!reload)
            {
                particleSystem.Play(); // Play gun animation
                currentAmmo--; // Remove 1 ammo

                if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 100.0f)) // Shoot ray
                {
                    //Debug.Log(hit.transform.tag);
                    if(hit.transform.tag == "Enemy") // Check if enemy
                    {
                        GameObject enemy = hit.transform.gameObject; // Get enemy

                        enemy.GetComponent<EnemyManager>().Health -= 10f; // Remove health
                        //Debug.Log(enemy.GetComponent<EnemyManager>().Health);
                    }
                }
            }
        }
    }

    public void Reload()
    {
        // Variables
        float actualCurrentAmmo = currentAmmo;
        float neededAmmo = 30 - actualCurrentAmmo;

        //if(totalAmmo < neededAmmo) currentAmmo += totalAmmo;
        //else currentAmmo += neededAmmo;

        //totalAmmo -= neededAmmo;

        if (totalAmmo < neededAmmo)
        {
            currentAmmo += totalAmmo;
            totalAmmo -= totalAmmo;
        }
        else
        {
            currentAmmo += neededAmmo;
            totalAmmo -= neededAmmo;
        }

        reload = false;
        print("Reloaded!");
    }
}
