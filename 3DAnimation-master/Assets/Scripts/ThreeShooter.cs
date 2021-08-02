using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeShooter : WeaponClass
{

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        //Each gun has a unique number of bullets
        maxBullets = 30;
        numberBullets = maxBullets;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
    public override void OnTriggerPull()
    {
        Shoot();
    }

    public override void OnTriggerRelease()
    {

    }
    public override void OnTriggerHold()
    {

    }

    public void Shoot()
    {
        // If this bullet came from the player...
        if (this.gameObject.GetComponentInParent<Pawn>() != null && numberBullets > 0)
        {
            //TODO: Shoot single bullet
            //New game object based on a prefab
            GameObject instBullet1 = Instantiate(bullet, bulletSpawn[0].transform.position, Quaternion.identity) as GameObject;
            GameObject instBullet2 = Instantiate(bullet, bulletSpawn[1].transform.position, Quaternion.identity) as GameObject;
            GameObject instBullet3 = Instantiate(bullet, bulletSpawn[2].transform.position, Quaternion.identity) as GameObject;
            //New rigidbody based on prefab
            Rigidbody instBulletRigidbody1 = instBullet1.GetComponent<Rigidbody>();
            Rigidbody instBulletRigidbody2 = instBullet2.GetComponent<Rigidbody>();
            Rigidbody instBulletRigidbody3 = instBullet3.GetComponent<Rigidbody>();
            //Set the velocity of the bullet to the relative forward of the bullet spawn object
            instBulletRigidbody1.velocity = transform.forward * -speed;
            instBulletRigidbody2.velocity = transform.forward * -speed;
            instBulletRigidbody3.velocity = transform.forward * -speed;
            //Timer for bullet despawn
            StartCoroutine(bulletDespawn(instBullet1));
            StartCoroutine(bulletDespawn(instBullet2));
            StartCoroutine(bulletDespawn(instBullet3));
        }
        else
        {
            //Nothing
        }
        numberBullets = numberBullets - 3;

    }

    IEnumerator bulletDespawn(GameObject instBullet)
    {
        //Once the bullet is shot, wait two seconds before destroying it.
        yield return new WaitForSeconds(2);
        Destroy(instBullet);
    }
}
