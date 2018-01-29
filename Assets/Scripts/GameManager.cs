using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public GameObject playerObj;
    //private GameObject player;

    private static Transform playerPos;
    private static Transform cameraPos;
    private static PlayerState _ps;
    private static PlayerInventory _pi;
    private Text missles;
    private Text energy;


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
	
    public void updateCheckpoint(GameObject player, Transform camera)
    {
        playerPos.position = new Vector3(player.transform.position.x, player.transform.position.y);
        cameraPos = camera;
        _ps = player.GetComponent<PlayerState>();
        _pi = player.GetComponent<PlayerInventory>();
        missles = _pi.missleCount;
        energy = player.GetComponent<UnitHealth>().energy;
    }

    public void resetGame()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.transform.position = new Vector3(playerPos.position.x, playerPos.position.y);
        Camera.main.transform.position = new Vector3(cameraPos.position.x, cameraPos.position.y, cameraPos.position.z);
        player.GetComponent<UnitHealth>().resetGame();
        //GameObject player = Instantiate(playerObj, playerPos.transform.position, Quaternion.identity);
        //StartCoroutine(setStuff());
    }

    //IEnumerator setStuff()
    //{
    //    //yield return new WaitForEndOfFrame();
    //    PlayerState ps = player.GetComponent<PlayerState>();
    //    ps.bufferLookUp = _ps.bufferLookUp;
    //    ps.checkStuff = _ps.checkStuff;
    //    PlayerInventory pi = player.GetComponent<PlayerInventory>();
    //    pi.hasMorphBall = _pi.hasMorphBall;
    //    pi.hasLongShot = _pi.hasLongShot;
    //    pi.hasTuckBall = _pi.hasTuckBall;
    //    pi.hasPowerJump = _pi.hasPowerJump;
    //    pi.hasMisslePower = _pi.hasMisslePower;
    //    pi.missleCount = missles;
    //    player.GetComponent<UnitHealth>().energy = energy;
    //    player.GetComponent<UnitHealth>().healthTotal = 30;
    //    player.GetComponent<PlayerSprite>().Awaken();
    //    Camera.main.transform.position = cameraPos.position;
    //    yield break;
    //}

}
