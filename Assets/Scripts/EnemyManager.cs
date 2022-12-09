using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{

    // Variables
    private GameObject destination;
    private GameObject gm;

    private float health = 100f;
    private NavMeshAgent agent;

    // Getter and setter
    public float Health
    {
        get { return health; }
        set { health = value; }
    }

    private void Awake()
    {
        agent = transform.GetComponent<NavMeshAgent>(); // Get the navmeshagent
        destination = GameObject.FindGameObjectWithTag("Destination"); // Get the destination
        gm = GameObject.FindGameObjectWithTag("GameManager");

    }

    void Update()
    {
        agent.SetDestination(destination.transform.position); // Start pathfinding

        if(Vector3.Distance(destination.transform.position, agent.transform.position) < 2)
        {
            gm.GetComponent<GameManagerScript>().Health -= 10f;
            Destroy(transform);
        }
    }
}
