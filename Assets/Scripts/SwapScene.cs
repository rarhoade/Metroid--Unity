using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SwapScene : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SceneManager.LoadScene("Pinball Pete");
        }
    }
}
