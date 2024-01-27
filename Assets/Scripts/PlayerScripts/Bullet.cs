using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform firePoint, rotationPoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, rotationPoint.rotation);
        bullet.tag = "Bullet";
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(rotationPoint.right * bulletForce, ForceMode2D.Impulse);
    }
}
