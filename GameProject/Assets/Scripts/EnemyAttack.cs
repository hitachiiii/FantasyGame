using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float range = 3f;
    [SerializeField] private float timeBetweenAttacks = 1f;

    private Animator anim;
    private GameObject player;
    private bool playerInRange; //keep track of whether we are with in the range that we specified
    private BoxCollider[] weaponColliders;

    void Start()
    {
        weaponColliders = GetComponentsInChildren<BoxCollider> ();
        player = GameManager.instance.Player;
        anim = GetComponent<Animator> ();
        StartCoroutine(attack());
    }

    
    void Update()
    {
        if(Vector3.Distance(transform.position, player.transform.position) < range)
        {
            playerInRange = true;
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
}
