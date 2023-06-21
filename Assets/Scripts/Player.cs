using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    float speed = 15f;
    public int hp = 150;

    public GameObject bulletPrefab;
    public GameObject flashlight;

    bool IsFlashlightOn = false;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = GameObject.FindGameObjectWithTag("manager").transform.position;
        GameObject.FindGameObjectWithTag("MainCamera").transform.rotation = transform.rotation;

        flashlight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * speed, 0f, Input.GetAxis("Vertical") * Time.deltaTime * speed);


        transform.Rotate(0f, Input.GetAxis("Mouse X") * 5f, 0f);

        if (Input.GetMouseButtonDown(0) )
        {
            Fire();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (IsFlashlightOn)
            {
                IsFlashlightOn = false;
                flashlight.SetActive(false);
            }
            else if (!IsFlashlightOn)
            {
                IsFlashlightOn = true;
                flashlight.SetActive(true);
            }


        }

    }

    public void Fire()
    {

        Vector3 offset = transform.forward * 0.6f + transform.up * 0.2f + transform.right * 0.3f;


        Vector3 bulletPos = transform.position + offset;

        GameObject bullet = Instantiate(bulletPrefab, bulletPos, Quaternion.identity);


        Vector3 playerRotation = transform.rotation.eulerAngles;
        bullet.transform.rotation = Quaternion.Euler(90f, playerRotation.y, playerRotation.z);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {

            rb.AddForce(transform.forward * 1000f);
        }

    }

    // if it collides with th object with tag "key"
        void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("key"))
        {
            Destroy(other.gameObject);
            
        }
        else if (other.gameObject.CompareTag("chest"))
        {
            //if there is no object with tag "key"
            if (GameObject.FindGameObjectWithTag("key") == null)
            {
                SceneManager.LoadScene(3); /// win scene
            }
            

        }

    }



    

}
