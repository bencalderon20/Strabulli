using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    private float currentHealth;
    private Animator anim;

    private void Awake()
    {
        currentHealth = startingHealth;  
        anim = GetComponent<Animator>();
    }

    public void takeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);
        
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
