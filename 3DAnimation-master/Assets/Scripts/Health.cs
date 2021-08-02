using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{

    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    public UnityEvent OnHeal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Heal(float amountToHeal)
    {
        currentHealth += amountToHeal;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        OnHeal.Invoke();
    }

    public void PlayHealthExtras()
    {
        //TODO: Play particle and sound
    }
}
