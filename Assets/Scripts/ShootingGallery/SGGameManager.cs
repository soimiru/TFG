using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SGGameManager : MonoBehaviour
{

    public List<Transform> spawnPoints;
    public List<GameObject> patitos;
    public Canvas mainCanvas;

    private SGUIManager uiMan;
    private bool gameOver = false;
    private int points = 0;
    private int gameSeconds = 30;
    private int randomIndex;

    private void Awake()
    {
        uiMan = mainCanvas.GetComponent<SGUIManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartGame() {
        //timeText.text = "00:" + gameSeconds;
        InvokeRepeating("Spawn", 0f, 1.5f);
        InvokeRepeating("CountDown", 0f, 1f);
    }

    public void Spawn() {
        if (!gameOver) {
            randomIndex = Random.Range(0, spawnPoints.Count);
            GameObject patitoNew;
            if (randomIndex < 3)
            {  //SPAWNS DERECHA
                patitoNew = (GameObject)Instantiate(patitos[0], spawnPoints[randomIndex].position, Quaternion.identity);
                patitoNew.GetComponentInChildren<Target>().myDirection = 0;
            }
            else
            {  //SPAWNS IZQUIERDA
                patitoNew = (GameObject)Instantiate(patitos[1], spawnPoints[randomIndex].position, Quaternion.identity);
                patitoNew.GetComponentInChildren<Target>().myDirection = 1;
            }
        }
    }

    public void CountDown() {
        if (gameSeconds == 0)
        {
            uiMan.TimeManager(-1);
            gameOver = true;
            CancelInvoke("Count");
        }
        else
        {
            gameSeconds -= 1;
            uiMan.TimeManager(gameSeconds);
        }
        
    }


    public void AddPoints(int amount) 
    {
        points += amount;
        uiMan.PointsManager(points);
        //pointsText.text = "Points: " + points;
    }
}
