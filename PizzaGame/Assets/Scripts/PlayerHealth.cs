using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    
    public int MaxHealth = 3; 
    public int Health; 

    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if(Health <= 0)
        {
            //GameOverCanvas.SetActive(true);
        }
    }
}
