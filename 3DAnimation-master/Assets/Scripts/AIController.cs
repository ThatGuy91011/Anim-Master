using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform followTF;
    public float decisionDelay = 1f;
    private float nextDecisionTime;

    private Animator anim;

    private Enemy enemy;
    private Pawn pawn;

    public GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindWithTag("Game Manager").GetComponent<GameManager>();
        enemy = GetComponent<Enemy>();
        pawn = GetComponent<Pawn>();
        agent = GetComponent<NavMeshAgent>();
        nextDecisionTime = Time.time;
        anim = GetComponent<Animator>();
        followTF = GameObject.FindWithTag("Player").transform;
    }



    // Update is called once per frame
    void Update()
    {
        if (!gm.isPaused)
        {
            if (!enemy.isRespawning)
            {
                if (Time.time >= nextDecisionTime)
                {
                    //Update follow
                    agent.SetDestination(followTF.position);
                    //Set next decision time
                    nextDecisionTime = Time.time + decisionDelay;
                }
                // TODO: animation of enemy
                //Get desired movement from agent
                Vector3 desiredMovement = agent.desiredVelocity;
                //Invert movement to work with root motion
                desiredMovement = this.transform.InverseTransformDirection(desiredMovement);
                //Remove speed by normalizing to 1
                desiredMovement = desiredMovement.normalized;
                //Use pawn speed
                desiredMovement *= pawn.speed; //Pawn speed
                                               //Pass into animator
                anim.SetFloat("Forward", desiredMovement.z);
                anim.SetFloat("Right", desiredMovement.x);
            }
        }
    }

    private void OnAnimatorMove()
    {
        agent.velocity = anim.velocity;
    }



    public Transform GetFollowTarget()
    {
        Transform followTarget;
        followTarget = FindObjectOfType<PlayerController>().pawn.transform;
        return followTarget;
    }
}