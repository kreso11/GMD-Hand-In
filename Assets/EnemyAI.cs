using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    Hp playerHP;

    public float mobHp;

    public AudioSource audioSourceSpot;
    public AudioSource audioSourceAttack;

 

    //Attak
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //State
    public float sightRange, attackRange;
    public bool playerInSight,playerInAttackRange;
    private bool walking;
    private Vector3 point;

    private bool playedSpot;

    public Animator animator;

    private void Awake(){
        player = GameObject.Find("PT_Medieval_Priest_StPatrick").transform;
        playerHP = GameObject.Find("Slider").GetComponent<Hp> ();
        agent = GetComponent<NavMeshAgent>();
        playedSpot = false;
    }

    private void Update(){
        float distToPlayer = Vector3.Distance(transform.position, player.position); 

        if (distToPlayer < sightRange) playerInSight = true;
        else playerInSight = false;
        if (distToPlayer < attackRange) playerInAttackRange = true;
        else playerInAttackRange = false;


        //playerInSight = Physics.CheckSphere(transform.position, sightRange);
        //playerInAttackRange = Physics.CheckSphere(transform.position, sightRange);
        if (playerInSight && !playerInAttackRange){
            Chase();
        }
        else if (playerInSight && playerInAttackRange){
            Attack();
        }
        if (!playerInSight && !playerInAttackRange){
            SearchForPlayer();
        }

        Killed();
    }

    private void SearchForPlayer(){
        //reset spot sound
        playedSpot = false;
        
        if (!walking) {
            point = new Vector3(transform.position.x+Random.Range(-100,100),transform.position.y,transform.position.z+Random.Range(-100,100));
            walking = true;
        }
        if (walking){
            agent.SetDestination(point);
        }

        Vector3 destdist = transform.position - point;
        if (destdist.magnitude < 1f){
            walking = false;
        }
    }

    private void Chase(){
        if (!playedSpot){
            audioSourceSpot.Play();
            playedSpot = true;
        }
        agent.SetDestination(player.position);
    }

    private void Attack(){
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked){

            audioSourceAttack.Play();
            animator.SetTrigger("Attack");
            playerHP.TakeDmg(10);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), 1.0f);
        }
    }

    private void ResetAttack(){
        alreadyAttacked = false;
    }

    private void TakeDmg(float dmg){
        mobHp -= dmg;
        if (mobHp <= 0){
            Invoke(nameof(Killed), .5f);
        }
    }

    private void Killed(){
        if (Vector3.Distance(transform.position, player.position) > 50){
            Destroy(gameObject);
            playerHP.gainScore(5);
        }
    }

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "fireball"){
            Destroy(gameObject);
        }
    }
}
