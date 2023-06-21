using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private GameObject player;
    
    Animator anime;

    public int hp ;
    public int damage ;

    float attackDis = 3f;
    float chaseDis = 14f;

    

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("player");
        anime = GetComponent<Animator>();

        
        
    }

    // Update is called once per frame
    void Update()
    {
        Check();

        

    }

    public void Check()
    {


        float distance = Vector3.Distance(transform.position, player.transform.position);



        if (distance <= attackDis)
        {
            anime.SetBool("attack", true);

            player.GetComponent<Player>().hp -= damage;

            if (player.GetComponent<Player>().hp <= 0)
            {
                
                SceneManager.LoadScene(2); // lose scene

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
        }



    }

    public void Chase()
    {
        anime.SetBool("isClose", true);
        navMeshAgent.SetDestination(player.transform.position);

    }

}
