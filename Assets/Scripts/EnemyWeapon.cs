using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public Transform firepoint;
    public GameObject bulletPrefab;

    public void Shoot(){
        //shooting logic
        Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
    }
}
