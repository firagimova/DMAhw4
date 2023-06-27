using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    //get tmpro
    public TextMeshProUGUI key;
    public TextMeshProUGUI hp;
    GameObject player;

    public GameObject cam1;
    public GameObject cam2;

    


    // Start is called before the first frame update
    void Start()
    {
       player = GameObject.FindWithTag("player");
        cam2.SetActive(false);

        PlayerPrefs.SetInt("canShoot", 0);
    }

    // Update is called once per frame
    void Update()
    {
        key.text = PlayerPrefs.GetInt("key").ToString() + "/3";

        
        hp.text = player.GetComponent<Player>().hp.ToString();

        //when I press M, change the camera 
        if (Input.GetKeyDown(KeyCode.M))
        {
            cam1.SetActive(!cam1.activeSelf);
            cam2.SetActive(!cam2.activeSelf);
        }

        //when cam 2 is active, make everything stop
        if (cam2.activeSelf)
        {
            Time.timeScale = 0;
            PlayerPrefs.SetInt("canShoot", 0);
        }
        else
        {
            PlayerPrefs.SetInt("canShoot", 1);
            Time.timeScale = 1;
        }


    }


    //ondestroy make the player go back to the start
    private void OnDestroy()
    {
        PlayerPrefs.SetInt("key", 0);
    }
}
