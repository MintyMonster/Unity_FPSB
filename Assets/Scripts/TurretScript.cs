using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    [SerializeField] GameObject gun;
    [SerializeField] ParticleSystem psystem;

    private Transform muzzle;
    private GameObject target;

    private List<GameObject> enemyList = new List<GameObject>();
    private float awarenessRadius = 10f;
    private float fireRate = 1f;
    private float rotateSpeed = 1f;
    private float cooldown = 2f;
    private float accuracy = 1f;

    private RaycastHit hitOut;

    private void Update()
    {
        GameObject.FindGameObjectsWithTag("Enemy").ToList().ForEach(e => enemyList.Add(e));
        enemyList.ForEach(e => print(e.tag));
    }
}
