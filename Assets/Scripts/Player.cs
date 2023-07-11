using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    float speed;
    float jumpForce;
    public int hp = 150;
    public Animator anim;
    Rigidbody rb;

    public GameObject bulletPrefab;
    public GameObject flashlight;
    public GameObject pistol;

    public ParticleSystem fire;

    bool IsFlashlightOn = false;

    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = GameObject.FindGameObjectWithTag("manager").transform.position;
        pistol.transform.localRotation = Quaternion.Euler(0, -90, 0);
        //GameObject.FindGameObjectWithTag("MainCamera").transform.rotation = transform.rotation;

        flashlight.SetActive(false);

        rb = GetComponent<Rigidbody>();
        anim.SetBool("isGrounded", true);
        speed = 6f;
        jumpForce = 7f;

        audioSource = GetComponent<AudioSource>();

        PlayerPrefs.SetInt("key", 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * speed, 0f, Input.GetAxis("Vertical") * Time.deltaTime * speed);

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //transform.Rotate(0f, Input.GetAxis("Mouse X") * 5f, 0f);

        Vector3 movement = new Vector3(horizontal, 0.0f, vertical) * speed * Time.deltaTime;
        movement = transform.TransformDirection(movement);

        rb.MovePosition(transform.position + movement);
        anim.SetFloat("vertical", vertical);


        if (Input.GetKeyDown(KeyCode.Space) && anim.GetBool("isGrounded"))
        {
            //I want to add a force to my player to make it jump
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            anim.SetTrigger("jumped");
            anim.SetBool("isGrounded", false);
        }

        // if I press shift I want to play the run animation
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            anim.SetBool("isRunning", true);
            speed = 9f;
        }
        // if I release shift I want to play the idle animation
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            anim.SetBool("isRunning", false);
            speed = 6f;
        }

        if (Input.GetMouseButtonDown(0) && PlayerPrefs.GetInt("canShoot") == 1 )
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

        Vector3 offset = new Vector3(0.5f, 0.05f, 0f);

        audioSource.Play();


        Vector3 bulletPos = pistol.transform.TransformPoint(offset);
        //Quaternion bulletRot = Quaternion.LookRotation(pistol.transform.forward);

        GameObject bullet = Instantiate(bulletPrefab, bulletPos, pistol.transform.rotation);


        bullet.transform.Rotate(90,90, 0);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {

            rb.AddForce(pistol.transform.right * 1000f);
        }

        fire.Play();

        StartCoroutine(ShakePistol(0.2f, 0.1f));

    }

    public IEnumerator ShakePistol(float duration, float magnitude)
    {
        Quaternion originalRotation = pistol.transform.localRotation;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float angle = Random.Range(-10, 10) * magnitude;
            Quaternion rotation = originalRotation * Quaternion.Euler(0,0, angle);
            pistol.transform.localRotation = rotation;

            elapsed += Time.deltaTime;

            yield return null;
        }

        pistol.transform.localRotation = originalRotation;
    }

    // if it collides with th object with tag "key"
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("key"))
        {
            Destroy(other.gameObject);
            int key = PlayerPrefs.GetInt("key");
            key++;
            PlayerPrefs.SetInt("key", key);
            

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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            anim.SetBool("isGrounded", true);
        }
    }





}
