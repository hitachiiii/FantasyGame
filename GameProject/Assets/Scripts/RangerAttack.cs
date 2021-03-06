﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class RangerAttack : MonoBehaviour
{
    [SerializeField] private float range = 3f;
    [SerializeField] private float timeBetweenAttacks = 1f;
    [SerializeField] Transform fireLocation;

    private Animator anim;
    private GameObject player;
    private bool playerInRange; //keep track of whether we are with in the range that we specified
    private EnemyHealth enemyHealth;
    private GameObject arrow;
    

    void Start()
    {
        arrow = GameManager.instance.Arrow;
        enemyHealth = GetComponent<EnemyHealth>();
        player = GameManager.instance.Player;
        anim = GetComponent<Animator> ();
        StartCoroutine(attack());
    }

    
    void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < range && enemyHealth.IsAlive)
        {
            playerInRange = true;
            RotateTowards(player.transform);

        }
        else
        {
            playerInRange = false;
        }
    }

    IEnumerator attack()
    {
        if(playerInRange && !GameManager.instance.GameOver)
        {
            anim.Play("attack");
            yield return new WaitForSeconds(timeBetweenAttacks);
        }

        yield return null;
        StartCoroutine(attack());
    }
    
    private void RotateTowards(Transform player){
        Vector3 diraction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(diraction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
    }
    public void FireArrow(){
        GameObject newArrow = Instantiate (arrow) as GameObject;
        newArrow.transform.position = fireLocation.position;
        newArrow.transform.rotation = transform.rotation;
        newArrow.GetComponent<Rigidbody>().velocity = transform.forward * 25f;


    }
}
