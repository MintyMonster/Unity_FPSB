using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirStrikeHandler : MonoBehaviour
{
    // Variables
    [SerializeField] GameObject aspos;
    private GameObject as1;
    private GameObject as2;
    private GameObject as3;
    private GameObject as4;
    private GameObject as5;

    public void AirStrike(Vector3 position)
    {
        // Instantiate objects to spawn particles from
        as1 = Instantiate(aspos, position, Quaternion.identity);
        as2 = Instantiate(aspos, new Vector3(position.x, position.y, position.z + 2), Quaternion.identity);
        as3 = Instantiate(aspos, new Vector3(position.x, position.y, position.z + 4), Quaternion.identity);
        as4 = Instantiate(aspos, new Vector3(position.x, position.y, position.z - 2), Quaternion.identity);
        as5 = Instantiate(aspos, new Vector3(position.x, position.y, position.z - 4), Quaternion.identity);

        // Play the particle system
        as1.GetComponentInChildren<ParticleSystem>().Play();
        as2.GetComponentInChildren<ParticleSystem>().Play();
        as3.GetComponentInChildren<ParticleSystem>().Play();
        as4.GetComponentInChildren<ParticleSystem>().Play();
        as5.GetComponentInChildren<ParticleSystem>().Play();

        Invoke("DestroyThings", 2f);

    }

    // Destroy particles
    private void DestroyThings()
    {
        Destroy(as1);
        Destroy(as2);
        Destroy(as3);
        Destroy(as4);
        Destroy(as5);
    }
}
