using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    public GameObject bullet;
    public float speed;


    // Start is called before the first frame update
    void Start()
    {
        bullet = GameObject.FindWithTag("Bullet");
    }

    // Update is called once per frame
    void Update()
    {
        //If player presses the left mouse button...
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }
    void Shoot()
    {
        //New game object based on a prefab
        GameObject instBullet = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
        //New rigidbody based on prefab
        Rigidbody instBulletRigidbody = instBullet.GetComponent<Rigidbody>();
        //Set the velocity of the bullet to the relative forward of the bullet spawn object
        instBulletRigidbody.velocity = transform.forward * speed;
        //Timer for bullet despawn
        StartCoroutine(bulletDespawn(instBullet));
    }
    IEnumerator bulletDespawn(GameObject instBullet)
    {
        //Once the bullet is shot, wait two seconds before destroying it.
        yield return new WaitForSeconds(2);
        Destroy(instBullet);
    }
}
