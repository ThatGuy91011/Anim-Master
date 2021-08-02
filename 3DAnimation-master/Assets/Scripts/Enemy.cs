using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject bullet;

    public float bulletSpeed;
    public GameObject bulletSpawn;
    public bool isRespawning;

    public int respawnTime;
    public int bulletDespawnTime;
    public int timeBetweenShots;

    public DropManager dm;

    private Ragdoll ragdoll;

    private GameObject esp;
    // Start is called before the first frame update
    void Start()
    {
        bullet = GameObject.FindWithTag("Bullet");
        StartCoroutine(bulletDespawn());
        ragdoll = GetComponent<Ragdoll>();
        esp = GameObject.FindWithTag("Enemy Spawnpoint");
        dm = GetComponent<DropManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        // If the enemy is shot...
        if (other.CompareTag("Bullet"))
        {
            //Drop a random item
            Instantiate(dm.DropItem(), transform.position + new Vector3(0, 3, 0), Quaternion.identity);
            // "Destroy" the enemy
            ragdoll.isDead = true;
            // Destroy the bullet
            Destroy(other.gameObject);
            //Add to player's score
            GameObject.Find("Canvas").GetComponent<TimerAndScore>().score += 1;
            // Start the timer for enemy respawn
            StartCoroutine(enemyRespawn());
        }
    }

    IEnumerator enemyRespawn()
    {
        //While respawning...
        isRespawning = true;
        //Once the enemy is defeated, they will wait a certain amount of time before respawning at spawnpoint.
        yield return new WaitForSeconds(respawnTime);
        esp.GetComponent<EnemySpawnPoint>().isEnemySpawned = false;
        Destroy(this.gameObject);
    }

    void enemyShoot()
    {
        if (isRespawning)
        {
            //Do nothing.
        }
        else
        {
            //TODO: Shoot single bullet
            //New game object based on a prefab
            GameObject instBullet = Instantiate(bullet, bulletSpawn.transform.position, Quaternion.identity) as GameObject;
            instBullet.tag = "EnemyBullet";
            //New rigidbody based on prefab
            Rigidbody instBulletRigidbody = instBullet.GetComponent<Rigidbody>();
            //Set the velocity of the bullet to the relative forward of the bullet spawn object
            instBulletRigidbody.velocity = transform.forward * bulletSpeed;
            //Timer for bullet despawn
            StartCoroutine(bulletDespawn());
        }

    }
    IEnumerator bulletDespawn()
    {
        GameObject instBullet = GameObject.FindWithTag("EnemyBullet");
        //Once the bullet is shot, wait two seconds before destroying it.
        yield return new WaitForSeconds(bulletDespawnTime);
        if (instBullet != null)
        {
            Destroy(instBullet);
        }
        yield return new WaitForSeconds(timeBetweenShots);
        enemyShoot();
    }

}
