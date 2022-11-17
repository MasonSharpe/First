using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bullet;
    public float bulletSpeed = 5.0f;
    public float bulletLifetime = 3.0f;
    public AudioClip shootSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.timeScale != 0)
        {
            GameObject bulletSpawn = Instantiate(bullet, transform.position, Quaternion.identity);
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector2 shootDir = mousePosition - transform.position;
            shootDir.Normalize();
            bulletSpawn.GetComponent<Rigidbody2D>().velocity = shootDir * bulletSpeed;
            Destroy(bulletSpawn, bulletLifetime);
            Camera.main.GetComponent<AudioSource>().PlayOneShot(shootSound);
        }
    }
}
