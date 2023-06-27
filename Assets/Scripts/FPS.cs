using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour
{
    public Transform player;
    public float mouseSensitivity = 2f;
    float VertRotation = 0f;

    public GameObject pistol; // Reference to the pistol object
    float pistolRotationLimit = 20f;

    Quaternion initialPistolRotation;

    // Start is called before the first frame update
    void Start()
    {
        //pistol = GameObject.FindWithTag("pistol");
        if (pistol == null)
        {
            Debug.LogError("No object with tag 'pistol' was found. Make sure your pistol object has the 'pistol' tag.");
        }
        else
        {
            initialPistolRotation = pistol.transform.localRotation; // Save the initial rotation of the pistol
        }

        // Hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        VertRotation -= inputY;
        VertRotation = Mathf.Clamp(VertRotation, -60f, 60f);
        transform.localEulerAngles = Vector3.right * VertRotation;

        player.Rotate(Vector3.up * inputX);

        if (pistol != null)
        {
            float pistolRotation = VertRotation;
            // Clamp the pistol rotation so it doesn't rotate too much
            pistolRotation = Mathf.Clamp(pistolRotation, -pistolRotationLimit, pistolRotationLimit);
            pistol.transform.localRotation = initialPistolRotation * Quaternion.Euler(-Vector3.forward * pistolRotation);
        }

    }

    //unhide the cursor when the game is over
    private void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
