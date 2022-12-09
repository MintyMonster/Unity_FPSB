using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractScript : MonoBehaviour
{

    [SerializeField] Camera cam;
    [SerializeField] GameObject gunManager;
    private RaycastHit hit;

    public void OnInteractPressed()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 100f))
        {
            if(hit.transform.tag == "AmmoBox")
            {
                if(Vector3.Distance(transform.position, hit.point) < 15f)
                {
                    gunManager.GetComponent<GunManager>().RefillAmmo();
                }
            }
        }
    }
}
