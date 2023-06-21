using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartB()
    {
        SceneManager.LoadScene(1); // game scene
    }

    public void QuitB() 
    {
        Application.Quit();   
    }

    public void MenuB()
    {
        SceneManager.LoadScene(0); // menu scene
    }

}
