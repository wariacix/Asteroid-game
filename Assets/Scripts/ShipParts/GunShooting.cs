using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    private Rigidbody2D shipRb;

    private float delta = 0.0f;
    [SerializeField] private float gunSpeed = 10.0f;
    [SerializeField] private float bulletSpeed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        shipRb = gameObject.transform.parent.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        delta += Time.fixedDeltaTime;
        if (Input.GetButton("Fire1") & delta > gunSpeed)
        {
            delta = 0;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

            bulletRb.velocity = (shipRb.velocity + (Vector2)bulletRb.transform.up * bulletSpeed);
        }
    }
}
