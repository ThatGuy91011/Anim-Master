using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponClass : MonoBehaviour
{
    // Is the player holding down the button?
    public bool isTriggerDown = false;
    
    // Empty game objects that the right and left hands of the player snap to, respectively
    public Transform rightHandPoint;
    public Transform leftHandPoint;

    // The bullet to instantiate
    public GameObject bullet;

    // Speed of the bullet
    public float speed;

    // Array to hold however many/few bullets the gun can shoot
    public GameObject[] bulletSpawn;

    public bool isDead;

    public int numberBullets;

    public int maxBullets;

    private GameManager gm;
    // Start is called before the first frame update
    public virtual void Start()
    {
        gm = GameObject.FindWithTag("Game Manager").GetComponent<GameManager>();
        bullet = GameObject.Find("BulletTest");
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (!gm.isPaused)
        {
            if (!isDead)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    isTriggerDown = true;
                }

                if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    isTriggerDown = false;
                }

                if (isTriggerDown)
                {
                    OnTriggerPull();
                    isTriggerDown = false;
                }
            }
        }
    }

    public abstract void OnTriggerPull();
    public abstract void OnTriggerRelease();
    public abstract void OnTriggerHold();
}
