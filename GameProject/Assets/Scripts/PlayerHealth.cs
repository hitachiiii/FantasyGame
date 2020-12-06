using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int startingHealth = 100;
    [SerializeField] float timeSinceLastHit = 2f;

    private float timer = 0f;
    private CharacterController characterController;
    private Animator anim;
    private int currentHealth;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController> ();
        currentHealth = startingHealth;
    }

    
    void Update()
    {
        timer += Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if(timer >= timeSinceLastHit && !GameManager.instance.GameOver)
        {
            if(other.tag == "Weapon")
            {
                takeHit();
                timer = 0;
            }
        }
    }

    void takeHit()
    {
        if(currentHealth > 0)
        {
            GameManager.instance.PlayerHit(currentHealth);
            anim.Play("hurt");
            currentHealth -= 10;
        }
        if(currentHealth <= 0)
        {
            killPlayer();
        }
    }
    void killPlayer()
    {
        GameManager.instance.PlayerHit(currentHealth);
        anim.SetTrigger("HeroDie");
        characterController.enabled = false;
    }
}
