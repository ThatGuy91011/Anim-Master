using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]public Pawn pawn;
    private Camera cam;
    private Animator anim;
    [SerializeField, Tooltip("The speed the player moves.")] private float speed;
    [SerializeField, Tooltip("The speed the player turns in degrees/second.")] private float turnSpeed;
    public int runSpeed;
    public int walkSpeed;

    public bool isDead;

    public HealthBar healthBar;

    public int maxHealth;

    public int currentHealth;

    public int hurtHealth;

    public int healHealth;

    public int lives;

    public Transform playerSpawn;

    public bool isRespawning = false;

    public GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindWithTag("Game Manager").GetComponent<GameManager>();
        anim = GetComponent<Animator>();
        pawn = GetComponent<Pawn>();
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

        

        healthBar.SetMaxHealth(maxHealth);

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (lives > 0)
        {
            if (!gm.isPaused)
            {
                // If the player's health is greater than the max health...
                if (currentHealth > maxHealth)
                {
                    // Reset it to the max health.
                    currentHealth = maxHealth;
                }
                // If the player loses all their health...
                if (currentHealth <= 0 && !isRespawning)
                {
                    // Play ragdoll
                    GetComponent<Ragdoll>().isDead = true;
                    StartCoroutine(RespawnWait());
                }
                // Constantly set the health bar to the current health.
                healthBar.SetHealth(currentHealth);


                //Get direction of input
                Vector3 stickDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

                //Controls cap of thumbstick direction
                stickDirection = Vector3.ClampMagnitude(stickDirection, 1);

                //Invert movement to world based and not local
                Vector3 animationDirection = transform.InverseTransformDirection(stickDirection);

                //Shift Key to run
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    speed = runSpeed;
                }

                else
                    speed = walkSpeed;

                //Convert input into the correct animation in the animator
                anim.SetFloat("Forward", animationDirection.z * speed);
                anim.SetFloat("Right", animationDirection.x * speed);

                if (!isDead)
                {
                    if (cam != null)
                    {
                        RotateToMousePointer();
                    }
                }
            }
        }
    }
    IEnumerator RespawnWait()
    {
        isRespawning = true;
        yield return new WaitForSeconds(3);
        lives -= 1;
        if (lives > 0)
        {
            transform.position = playerSpawn.transform.position;
            currentHealth = maxHealth;
            GetComponent<Ragdoll>().isDead = false;
            GetComponent<Pawn>().weapon.numberBullets = GetComponent<Pawn>().weapon.maxBullets;
            isRespawning = false;
        }
        else
            Application.Quit();
    }
    // When the player interacts with something...
    private void OnTriggerEnter(Collider other)
    {
        if (this.GetComponent<PlayerController>() != null)
        {
            // If they are hit by an enemy bullet...
            if (other.CompareTag("EnemyBullet"))
            {
                {
                    // ...the player loses health.
                    currentHealth -= 2;
                    // Destroy the bullet.
                    Destroy(other.gameObject);
                }
            }
            // Otherwise, if it was a health kit...
            if (other.CompareTag("HealthKit"))
            {
                // Heal thy self
                currentHealth += healHealth;
                // Destroy the med kit
                Destroy(other.gameObject);
            }
        }

    }


    /// <summary>
    /// Find the mouse pointer in relation to the screen and faces the player towards it.
    /// </summary>
    public void RotateToMousePointer()
    {
        
        //Find game plane
        Plane gamePlane = new Plane(Vector3.up, transform.position);

        //Draw ray from mouse towards game plane
        Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);

        //Using distance to intersection, find point in world space
        float distance;
        gamePlane.Raycast(mouseRay, out distance);
        Vector3 targetPoint = mouseRay.GetPoint(distance);

        //Rotate towards point
        RotateTowards(targetPoint);

    }

    /// <summary>
    /// Rotates an object towards a given target.
    /// </summary>
    /// <param name="lookAtPoint"></param>
    public void RotateTowards(Vector3 lookAtPoint)
    {
        //Find rotation to look at target
        Quaternion goalRotation;
        goalRotation = Quaternion.LookRotation(lookAtPoint - transform.position, Vector3.up);

        //Rotate slighty towards goal
        transform.rotation = Quaternion.RotateTowards(transform.rotation, goalRotation, turnSpeed * Time.deltaTime);
    }
}
