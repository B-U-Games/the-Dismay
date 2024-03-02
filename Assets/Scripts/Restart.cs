using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Restart : MonoBehaviour
{
    public KeyCode RestartKey = KeyCode.R;
    void Update()
    {
        if (Input.GetKeyDown(RestartKey))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
