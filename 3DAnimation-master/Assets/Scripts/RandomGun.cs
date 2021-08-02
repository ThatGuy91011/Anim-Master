using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGun : WeaponClass
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        Random.seed = System.Environment.TickCount;
        //Each gun has a unique number of bullets
        maxBullets = 50;
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
            // If the random number is odd...
            if ((Random.Range(0, 10) % 2) > 0)
            {
                //TODO: Shoot single bullet
                //New game object based on a prefab
                GameObject instBullet = Instantiate(bullet, bulletSpawn[0].transform.position, Quaternion.identity) as GameObject;
                //New rigidbody based on prefab
                Rigidbody instBulletRigidbody = instBullet.GetComponent<Rigidbody>();
                //Set the velocity of the bullet to the relative forward of the bullet spawn object
                instBulletRigidbody.velocity = transform.forward * speed; // Real Bullet
                //Timer for bullet despawn
                StartCoroutine(bulletDespawn(instBullet));
            }
            // Otherwise...
            else
            {
                //TODO: Shoot single bullet
                //New game object based on a prefab
                GameObject instBullet = Instantiate(bullet, bulletSpawn[0].transform.position, Quaternion.identity) as GameObject;
                //New rigidbody based on prefab
                Rigidbody instBulletRigidbody = instBullet.GetComponent<Rigidbody>();
                //Set the velocity of the bullet to the relative forward of the bullet spawn object
                instBulletRigidbody.velocity = transform.forward * 0; // Dud Bullet
                //Timer for bullet despawn
                StartCoroutine(bulletDespawn(instBullet));
            }
            numberBullets--;
        }

    }

    IEnumerator bulletDespawn(GameObject instBullet)
    {
        //Once the bullet is shot, wait two seconds before destroying it.
        yield return new WaitForSeconds(2);
        Destroy(instBullet);
    }
}
