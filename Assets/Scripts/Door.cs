using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator anim;
    bool isOpen = false;
    
    public GameObject canvas;
    

    // Start is called before the first frame update
    void Start()
    {
        //get animator component
        anim = GetComponent<Animator>();
        canvas.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //when player enters the trigger
    private void OnTriggerStay(Collider other)
    {
        //if the player has the key and we press E
        if (other.CompareTag("player") && Input.GetKeyDown(KeyCode.E) )
        {
            

            if (isOpen)
            {
                //play the close animation
                anim.Play("door close");

            }
            else
            {
                anim.Play("door open");

            }

            isOpen = !isOpen;

        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            canvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("player"))
        {
            canvas.SetActive(false);
        }
    }


}
