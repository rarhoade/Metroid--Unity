using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public GameObject playerObj;

    private static Transform playerPos;
    private static Transform cameraPos;

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start () {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        cameraPos = Camera.main.transform;
	}
	
    public void updateCheckpoint(Transform player, Transform camera)
    {
        playerPos = player;
        cameraPos = camera;
    }

    public void resetGame()
    {
        Instantiate(playerObj, playerPos.transform.position, Quaternion.identity);
        Camera.main.transform.position = cameraPos.position;
    }
}
