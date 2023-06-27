using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        //udioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "body")
        {
            //get other object's parent's script
            other.gameObject.GetComponentInParent<Enemy>().hp -= 10;
            //other.gameObject.GetComponent<Enemy>().hp -= 10;



            if (other.gameObject.GetComponentInParent<Enemy>().hp <= 0)
            {
                //destroy the parent object of the other object
                
                //audioSource.Play();
                Destroy(other.transform.parent.gameObject);

            }
            

            Destroy(this.gameObject);

        }
        else if (other.gameObject.tag == "head")
        {

            other.gameObject.GetComponentInParent<Enemy>().hp -= 50;
            //other.gameObject.GetComponent<Enemy>().hp -= 50;



            if (other.gameObject.GetComponentInParent<Enemy>().hp <= 0)
            {

                Destroy(other.transform.parent.gameObject);

            }


            Destroy(this.gameObject);

        }

        else
        {
            Destroy(this.gameObject);
        }


    }
}
