using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private GameObject player;

    AudioSource audioSource;
    
    Animator anime;

    public int hp ;
    public int damage ;

    float attackDis = 5f;
    float chaseDis = 20f;
    float distance;

    float attackCooldown = 1.5f;  // The cooldown between each attack
    float attackTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("player");
        anime = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        distance =  Vector3.Distance(transform.position, player.transform.position);

        Check();

        

    }

    public void Check()
    {


        if (distance <= attackDis)
        {
            anime.SetBool("attack", true);
            navMeshAgent.isStopped = false;

            if (Time.time >= attackTime + attackCooldown)
            {
                audioSource.Play();
                player.GetComponent<Player>().hp -= damage;
                attackTime = Time.time;

                if (player.GetComponent<Player>().hp <= 0)
                {

                    SceneManager.LoadScene(2); // lose scene

                }
            }

                
        }
        else if (distance <= chaseDis) 
        {
            Chase();

        }
        else
        {
            anime.SetBool("isClose", false);
            anime.SetBool("attack", false);
            navMeshAgent.isStopped = true;
        }



    }

    public void Chase()
    {
        

        anime.SetBool("isClose", true);
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(player.transform.position);

    }

}
