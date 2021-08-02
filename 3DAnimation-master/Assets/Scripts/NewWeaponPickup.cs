using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewWeaponPickup : MonoBehaviour
{
    // Player Game Object
    public GameObject player;

    // Array to hold all the guns
    public WeaponClass[] weapons;
    public Sprite[] weaponImages;

    public Ragdoll ragdoll;

    public void Start()
    {
        ragdoll = GetComponent<Ragdoll>();
    }
    public void Update()
    {

    }
    public void OnTriggerEnter(Collider other)
    {
        // Three guns created so far
        // Standard backwards
        if (other.gameObject.CompareTag("Rifle"))
        {
            // Destroy the current weapon the player is using
            Destroy(GameObject.FindWithTag("CurrentWeapon"));

            // Replace it with the appropriate weapon
            player.GetComponent<Pawn>().weapon = weapons[0];

            // Reset the bullet count
            other.GetComponent<Rifle>().numberBullets = other.GetComponent<Rifle>().maxBullets;

            // Make the player its new parent
            other.transform.SetParent(player.transform);

            // Put the gun in the player's hands
            other.transform.localPosition = new Vector3(.265f, 1.149f, .494f);

            // Change the tag so that the next weapon is ready to be picked up
            other.tag = "CurrentWeapon";
            // Disable the weapon's trigger
            other.GetComponent<BoxCollider>().isTrigger = false;
            // Make this weapon part of the ragdoll script
            ragdoll.weapon = other.gameObject;
        }

        // Backwards but thrice
        else if (other.gameObject.CompareTag("3-Gun"))
        {
            Destroy(GameObject.FindWithTag("CurrentWeapon"));
            player.GetComponent<Pawn>().weapon = weapons[1];
            other.GetComponent<ThreeShooter>().numberBullets = other.GetComponent<ThreeShooter>().maxBullets;
            other.transform.SetParent(player.transform);
            other.transform.localPosition = other.transform.forward;
            other.transform.localRotation = new Quaternion(0, 0, 0, 0);
            other.tag = "CurrentWeapon";
            other.GetComponent<BoxCollider>().isTrigger = false;
            ragdoll.weapon = other.gameObject;
        }

        // Forwards, but will shoot a dud every other bullet.
        else if (other.gameObject.CompareTag("Random"))
        {
            Destroy(GameObject.FindWithTag("CurrentWeapon"));
            player.GetComponent<Pawn>().weapon = weapons[2];
            other.GetComponent<RandomGun>().numberBullets = other.GetComponent<RandomGun>().maxBullets;
            other.transform.SetParent(player.transform);
            other.transform.localPosition = new Vector3(.265f, 1.149f, .494f);
            other.tag = "CurrentWeapon";
            other.GetComponent<BoxCollider>().isTrigger = false;
            ragdoll.weapon = other.gameObject;
        }
    }
}
