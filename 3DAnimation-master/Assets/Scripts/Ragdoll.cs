using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    private Animator anim;
    private Collider topCol;
    private Rigidbody topRb;
    public List<Collider> ragdollCols;
    private List<Rigidbody> ragdollRbs;

    private PlayerController pc;

    public bool isDead;

    public GameObject weapon;
    public Vector3 weaponPlacement;
    public Quaternion weaponRotation;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        topCol = GetComponent<Collider>();
        topRb = GetComponent<Rigidbody>();
        ragdollCols = new List<Collider>(GetComponentsInChildren<Collider>());
        ragdollRbs = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());

        ragdollCols.Remove(topCol);
        ragdollRbs.Remove(topRb);

        pc = GetComponent<PlayerController>();

        //weapon = GetComponent<Pawn>().weapon;
        weaponPlacement = weapon.transform.localPosition;
        weaponRotation = weapon.transform.localRotation;
        StopRagdoll();
    }

    // Update is called once per frame
    void Update()
    {
        ragdollCols[11] = weapon.GetComponent<BoxCollider>();
        if (isDead)
        {
            StartRagdoll();
            if (pc != null)
            {
                pc.isDead = true;
                pc.GetComponentInChildren<WeaponClass>().isDead = true;
            }

        }

        if (!isDead)
        {
            StopRagdoll();
            if (pc != null)
            {
                pc.isDead = false;
                pc.GetComponentInChildren<WeaponClass>().isDead = false;
            }

        }

        if (Input.GetKey(KeyCode.G))
        {
            isDead = true;
        }
        if (Input.GetKey(KeyCode.H))
        {
            isDead = false;
        }
    }

    public void StartRagdoll()
    {
        weapon.AddComponent<Rigidbody>();
        weapon.GetComponent<Rigidbody>().useGravity = true;

        //Turn off animator
        anim.enabled = false;
        //Turn off big collider
        topCol.enabled = false;
        //Turn off rigidbody
        topRb.isKinematic = true;

        //Turn on ragdoll colliders
        foreach (Collider currentCol in ragdollCols)
        {
            currentCol.isTrigger = false;
        }
        //Turn on ragdoll rigidbodies
        foreach (Rigidbody currentRb in ragdollRbs)
        {
            currentRb.isKinematic = false;
        }
    }

    public void StopRagdoll()
    {
        Destroy(weapon.GetComponent<Rigidbody>());
        weapon.transform.localPosition = weaponPlacement;
        weapon.transform.localRotation = weaponRotation;

        //Turn on animator
        anim.enabled = true;
        //Turn on big collider
        topCol.enabled = true;
        //Turn on rigidbody
        topRb.isKinematic = false;

        //Turn off ragdoll colliders
        foreach (Collider currentCol in ragdollCols)
        {
            currentCol.isTrigger = true;
        }
        //Turn off ragdoll rigidbodies
        foreach (Rigidbody currentRb in ragdollRbs)
        {
            currentRb.isKinematic = true;
        }
    }
}
