using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] Transform player;
    private NavMeshAgent nav;
    private Animator anim;
    private EnemyHealth enemyHealth;


    void Awake()
    {
        Assert.IsNotNull(player);
    }

    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (!GameManager.instance.GameOver && enemyHealth.IsAlive)
        {
            nav.SetDestination(player.position);
        }
        else if((!GameManager.instance.GameOver || GameManager.instance.GameOver) && !enemyHealth.IsAlive)
        {
            nav.enabled = false;
        }
        else
        {
            nav.enabled = false;
            anim.Play("idle");
        }
    }
}
