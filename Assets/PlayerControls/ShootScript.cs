using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    [SerializeField] new ParticleSystem particleSystem;

    private float currentAmmo = 30;
    private float totalAmmo = 210;

    public void OnShootPressed() => particleSystem.Play(); // Play particles
}
