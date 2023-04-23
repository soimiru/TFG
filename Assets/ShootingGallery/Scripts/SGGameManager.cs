using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SGGameManager : MonoBehaviour
{

    public List<Transform> spawnPoints;
    public List<GameObject> duckies;

    private SGUIManager uiMan;
    public Canvas mainCanvas;
    public Slider timeSlider;
    public Toggle infiniteMode, timeMode;

    private bool gameOver = false;

    public int mode;
    private int points = 0;
    private int gameSeconds;
    private int randomIndex, randomDuckId;

    private float spawnTime = 1f;


    private void Awake()
    {
        uiMan = mainCanvas.GetComponent<SGUIManager>();
    }

    private void Start()
    {
        SoundManager.Instance.PlayMusic("Ducks");
    }

    /// <summary>
    /// Comprueba el toogle que está activo.
    /// </summary>
    /// <param name="active"></param>
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

    /// <summary>
    /// Customiza el juego con los parámetros seleccionados.
    /// </summary>
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

    /// <summary>
    /// Comienza el juego.
    /// </summary>
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

    /// <summary>
    /// Gestiona el spawn de patitos.
    /// </summary>
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

    /// <summary>
    /// Spawnea un patito a la derecha.
    /// </summary>
    /// <param name="r">Índice del array de spawnpoints.</param>
    void SpawnRight(int r) {
        GameObject patitoNew;
        randomDuckId = Random.Range(0, 3);
        patitoNew = (GameObject)Instantiate(duckies[randomDuckId], spawnPoints[r].position, Quaternion.identity);
        patitoNew.GetComponentInChildren<Target>().myDirection = 0;
    }

    /// <summary>
    /// Spawnea un patito a la izquierda.
    /// </summary>
    /// <param name="l"></param>
    void SpawnLeft(int l) {
        GameObject patitoNew;
        randomDuckId = Random.Range(3, 6);
        patitoNew = (GameObject)Instantiate(duckies[randomDuckId], spawnPoints[l].position, Quaternion.identity);
        patitoNew.GetComponentInChildren<Target>().myDirection = 1;
    }

    /// <summary>
    /// Gestiona la cuenta atrás del tiempo.
    /// </summary>
    public void CountDown() {
        if (gameSeconds == 0)
        {
            //uiMan.TimeManager(-1);
            gameOver = true;
            CancelInvoke("CountDown");
            GameOver();
        }
        else
        {
            gameSeconds -= 1;
            uiMan.TimeManager(gameSeconds);
        }
        
    }

    /// <summary>
    /// Gestiona la cuenta del tiempo.
    /// </summary>
    public void CountUp() {
        gameSeconds += 1;
        uiMan.TimeManager(gameSeconds);
    }

    /// <summary>
    /// Añade puntos por cada patito disparado.
    /// </summary>
    public void AddPoints(int amount) 
    {
        points += amount;
        uiMan.PointsManager(points);
    }

    /// <summary>
    /// Gestiona la finalización del juego.
    /// </summary>
    public void GameOver()
    {
        //INFINITE
        if (mode == 0)
        {
            uiMan.TimeManager(-2);
        }
        //TIME
        else
        {
            uiMan.TimeManager(-1);
        }
        gameOver = true;
        CancelInvoke("CountUp");
        uiMan.GameOver(points);
    }
}
