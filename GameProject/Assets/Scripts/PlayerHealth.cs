using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int startingHealth = 100;
    [SerializeField] float timeSinceLastHit = 2f;
    [SerializeField] Slider healthSlider;

    private float timer = 0f;
    private CharacterController characterController;
    private Animator anim;
    private int currentHealth;
    private AudioSource audio;

    void Awake(){
        Assert.IsNotNull(healthSlider);
    }
    
    void Start()
    {
        anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController> ();
        currentHealth = startingHealth;

        audio = GetComponent<AudioSource>();
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
            healthSlider.value = currentHealth;
            audio.PlayOneShot(audio.clip);
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
        audio.PlayOneShot(audio.clip);
    }
}
