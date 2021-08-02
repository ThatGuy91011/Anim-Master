using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : WeaponClass
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        //Each gun has a unique number of bullets
        maxBullets = 10;
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
            GameObject instBullet = Instantiate(bullet, bulletSpawn[0].transform.position, Quaternion.identity) as GameObject;
            //New rigidbody based on prefab
            Rigidbody instBulletRigidbody = instBullet.GetComponent<Rigidbody>();
            //Set the velocity of the bullet to the relative forward of the bullet spawn object
            instBulletRigidbody.velocity = transform.forward * -speed;
            //Timer for bullet despawn
            StartCoroutine(bulletDespawn(instBullet));
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
