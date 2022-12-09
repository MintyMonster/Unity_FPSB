using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AirstrikeScript : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] AirStrikeHandler handler;
    [SerializeField] GameObject gm;

    private RaycastHit hit;

    public void OnQPressed()
    {
        if(gm.GetComponent<GameManagerScript>().Money >= 50) // Check if player has enough money
        {
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 100f)) // Shoot ray towards area
            {
                Vector3 position = hit.point; // Get hit position of ray
                handler.AirStrike(position); // Create an airstrike

                GameObject.FindGameObjectsWithTag("Enemy").ToList().ForEach(e => // Foreach enemy in the scene, find it
                {
                    if(Vector3.Distance(position, e.transform.position) < 20) // If distance between airstrike and enemy is less than 20
                    {
                        e.GetComponent<EnemyManager>().Health -= 100; // Kill it
                    }
                });
            }
        }
    }
}
