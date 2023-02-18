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
    private int gameSeconds;
    private float spawnTime = 1f;
    private int randomIndex, randomDuckId;

    public Slider timeSlider;
    public Toggle infiniteMode, timeMode;

    public int mode;

    private void Awake()
    {
        uiMan = mainCanvas.GetComponent<SGUIManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //ConfigureGame();
        //StartGame();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ToggleCheck(bool active) {
        if (infiniteMode.isOn) {
            //timeMode.isOn = !timeMode.isOn;
            timeMode.transform.Find("Checkmark").gameObject.SetActive(false);
        }
        else if (timeMode.isOn)
        {
            //infiniteMode.isOn = !infiniteMode.isOn;
            infiniteMode.transform.Find("Checkmark").gameObject.SetActive(false);

        }
    }
    public void CustomizeGame() {

        //MODO INFINITO
        if (infiniteMode.isOn)
        {
            mode = 0;
            gameSeconds = 0;
        }

        //MODO TIEMPO
        else if (timeMode.isOn)
        {
            mode = 1;
            //Define el tiempo que durará la partida
            gameSeconds = (int)(timeSlider.value * 30);

        }
        StartGame();
    }

    public void StartGame() {
        //MODO INFINITO
        if (mode == 0)
        {
            InvokeRepeating("Spawn", 0f, spawnTime);
            InvokeRepeating("CountUp", 0f, 1f);
        }
        //MODO TIEMPO
        else if (mode == 1) {
            InvokeRepeating("Spawn", 0f, spawnTime);
            InvokeRepeating("CountDown", 0f, 1f);
        }
        uiMan.StartGame();
    }

    public void Spawn() {
        if (!gameOver) {
            randomIndex = Random.Range(0, spawnPoints.Count);
            if (randomIndex < 3)
            {  //SPAWNS DERECHA
                SpawnRight(randomIndex);
            }
            else
            {  //SPAWNS IZQUIERDA
                SpawnLeft(randomIndex);
            }
        }
    }

    void SpawnRight(int r) {
        GameObject patitoNew;
        randomDuckId = Random.Range(0, 3);
        patitoNew = (GameObject)Instantiate(patitos[randomDuckId], spawnPoints[r].position, Quaternion.identity);
        patitoNew.GetComponentInChildren<Target>().myDirection = 0;
    }

    void SpawnLeft(int r) {
        GameObject patitoNew;
        randomDuckId = Random.Range(3, 6);
        patitoNew = (GameObject)Instantiate(patitos[randomDuckId], spawnPoints[r].position, Quaternion.identity);
        patitoNew.GetComponentInChildren<Target>().myDirection = 1;
    }

    public void CountDown() {
        if (gameSeconds == 0)
        {
            uiMan.TimeManager(-1);
            gameOver = true;
            CancelInvoke("CountDown");
        }
        else
        {
            gameSeconds -= 1;
            uiMan.TimeManager(gameSeconds);
        }
        
    }

    public void CountUp() {
        gameSeconds += 1;
        uiMan.TimeManager(gameSeconds);
    }


    public void AddPoints(int amount) 
    {
        points += amount;
        uiMan.PointsManager(points);
        //pointsText.text = "Points: " + points;
    }

    public void GameOver()
    {
        uiMan.TimeManager(-2);
        gameOver = true;
        CancelInvoke("CountUp");
    }
}
