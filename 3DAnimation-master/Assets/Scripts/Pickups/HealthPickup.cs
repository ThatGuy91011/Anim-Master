using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup
{
    public float amountToHeal = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Health>().OnHeal.Invoke();
        other.GetComponent<Health>().Heal(amountToHeal);
    }

    public override void OnPickup()
    {

    }
}
