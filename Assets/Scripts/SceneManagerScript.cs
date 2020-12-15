using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    private bool interfaceIsActive;

    GameObject canvas;

    private void Start()
    {
        canvas = GameObject.Find("Canvas");
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (interfaceIsActive)
            {
                interfaceIsActive = false;
            }
            else
            {
                interfaceIsActive = true;
            }
        }

        if (interfaceIsActive)
        {
            canvas.SetActive(true);
        }
        else
        {
            canvas.SetActive(false);
        }
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadVerlet()
    {
        SceneManager.LoadScene("Verlet");
    }
    public void LoadVerletFree()
    {
        SceneManager.LoadScene("Verlet Follow Free");
    }
    public void LoadVerletConstrained()
    {
        SceneManager.LoadScene("Verlet Follow Constrained");
    }
    public void LoadVerletBridge()
    {
        SceneManager.LoadScene("Verlet Bridge");
    }
}
