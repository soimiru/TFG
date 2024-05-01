using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ZMUIManager : MonoBehaviour
{
    [SerializeField] private Text potionsText;
    private int potionsNum;
    [SerializeField] private Text pointsText;
    public List<Sprite> timeImages;
    public List<Image> timePositions;
    public List<Image> pointsPositions;

    private Animator anim;
    private GridManager gridManager;

    public Text finalPointsText;

    public Toggle infiniteMode, timeMode;
    public Slider timeSlider, sizeSlider;
    public GameObject timeSliderGO, sizeSliderGO, finishBTN;

    public int mode;
    private int points;
    private bool paused = false;
    private int gameSeconds;
    int uni, dec, cen, mil, demill;

    Color colorTransparent = new Color(0f, 0f, 0f, 0f);
    Color colorWhite = new Color(1f, 1f, 1f, 1f);

    private void Awake()
    {
        anim = GetComponent<Animator>();
        gridManager = FindObjectOfType<GridManager>();
    }

    private void Start()
    {
        SoundManager.Instance.PlayMusic("Potions");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }
    }
    public void ToggleCheck(bool active)
    {
        if (infiniteMode.isOn)
        {
            //timeMode.isOn = !timeMode.isOn;
            timeMode.transform.Find("Checkmark").gameObject.SetActive(false);
        }
        else if (timeMode.isOn)
        {
            //infiniteMode.isOn = !infiniteMode.isOn;
            infiniteMode.transform.Find("Checkmark").gameObject.SetActive(false);

        }
    }
    public void HideSliders(bool active)
    {
        timeSliderGO.SetActive(active);
        sizeSliderGO.SetActive(!active);
    }

    /// <summary>
    /// Customiza el juego en función de las opciones que introduzca el jugador.
    /// </summary>
    public void CustomizeGame() {
        //GAME CUSTOMIZATION
        //MODO INFINITO
        if (infiniteMode.isOn)
        {
            finishBTN.SetActive(true);
            mode = 0;
            gameSeconds = 0;
            switch (sizeSlider.value) { 
                case 1:
                    gridManager.xDim = 5;
                    gridManager.yDim = 5;
                    break;
                case 2:
                    gridManager.xDim = 7;
                    gridManager.yDim = 5;
                    break;
                case 3:
                    gridManager.xDim = 9;
                    gridManager.yDim = 6;
                    break;
            }
        }

        //MODO TIEMPO
        else if (timeMode.isOn)
        {
            finishBTN.SetActive(false);
            mode = 1;
            //Define el tiempo que durará la partida
            gameSeconds = (int)(timeSlider.value * 30);

        }
        StartGame();
    }

    /// <summary>
    /// Comienza el juego.
    /// </summary>
    void StartGame() {

        //MODO INFINITO
        if (mode == 0)
        {
            InvokeRepeating("CountUp", 0f, 1f);
        }
        //MODO TIEMPO
        else if (mode == 1)
        {
            InvokeRepeating("CountDown", 0f, 1f);
        }

        anim.SetTrigger("GameIDLE");
        gridManager.StartGame();

        Time.timeScale = 1f;
        potionsNum = 0;
        points = 0;
        AddPoints(0);
    }

    /// <summary>
    /// Pausa/Despausa el juego.
    /// </summary>
    public void PauseGame()
    {
        if (paused)
        {
            Time.timeScale = 1f;
            anim.SetTrigger("P_OUT");
            paused = false;
        }
        else
        {
            Time.timeScale = 0f;
            anim.SetTrigger("P_IN");
            paused = true;
        }
    }

    /// <summary>
    /// Método que gestiona la finalización del juego.
    /// </summary>
    public void GameOver()
    {
        //gameIsStarted = false;
        //INFINITE
        if (mode == 0) {
            TimeManager(-2);
        }
        //TIME
        else
        {
            TimeManager(-1);
        }
        finalPointsText.text = points + " points.";
        Invoke("GameOverTransition", 1f);

    }

    /// <summary>
    /// Se invoca después de un segundo y lanza la animación de la interfaz para los puntos.
    /// </summary>
    private void GameOverTransition()
    {
        Time.timeScale = 0f;
        anim.SetTrigger("GameOver");
        CancelInvoke();
    }

    /// <summary>
    /// Método para gestionar las escenas.
    /// </summary>
    /// <param name="name"></param>
    public void LoadSceneByName(string name)
    {
        SceneManager.LoadScene(name);
    }


    #region TIME MANAGER

    /// <summary>
    /// Método para gestionar la cuenta atrás.
    /// </summary>
    public void CountDown()
    {
        if (gameSeconds == 0)
        {
            //TimeManager(-1);
            //gameOver = true;
            GameOver();
            CancelInvoke("CountDown");
        }
        else
        {
            gameSeconds -= 1;
            TimeManager(gameSeconds);
        }

    }

    /// <summary>
    /// Método para gestionar la cuenta hacia adelante.
    /// </summary>
    public void CountUp()
    {
        gameSeconds += 1;
        TimeManager(gameSeconds);
    }

    /// <summary>
    /// Método para gestionar el tiempo.
    /// </summary>
    /// <param name="number">Número de segundos</param>
    public void TimeManager(int number)
    {

        if (number == -1)
        {
            //TIMEUP

            timePositions[0].color = colorTransparent;
            timePositions[1].color = colorTransparent;
            timePositions[3].color = colorTransparent;
            timePositions[4].color = colorTransparent;

            timePositions[2].sprite = timeImages[10];
            timePositions[2].rectTransform.sizeDelta = new Vector2(400f, 400f);
        }
        else if (number == -2)
        {
            timePositions[0].color = colorTransparent;
            timePositions[1].color = colorTransparent;
            timePositions[3].color = colorTransparent;
            timePositions[4].color = colorTransparent;

            timePositions[2].sprite = timeImages[11];
            timePositions[2].rectTransform.sizeDelta = new Vector2(400f, 400f);
        }
        else if (number < 10)
        {
            timePositions[1].sprite = timeImages[0];
            timePositions[3].sprite = timeImages[0];
            timePositions[4].sprite = timeImages[number];
        }
        else if (number >= 10 && number < 60)
        {
            int decenas = number / 10;
            int unidades = number - (decenas * 10);

            timePositions[1].sprite = timeImages[0];
            timePositions[3].sprite = timeImages[decenas];
            timePositions[4].sprite = timeImages[unidades];

        }
        //60-119
        else if (number >= 60 && number < 120)
        {
            number -= 60;
            int decenas = number / 10;
            int unidades = number - (decenas * 10);
            timePositions[1].sprite = timeImages[1];

            timePositions[3].sprite = timeImages[decenas];
            timePositions[4].sprite = timeImages[unidades];
        }
        //>119
        else if (number >= 120)
        {
            timePositions[1].sprite = timeImages[2];

        }
    }

    #endregion

    #region POINTS MANAGER
    /// <summary>
    /// Añade puntos cuando ocurre algo.
    /// </summary>
    /// <param name="numPieces"></param>
    public void AddPoints(int numPieces)
    {
        if (numPieces != 0)
        {
            SoundManager.Instance.PlaySFX("Pop");
            potionsNum++;
            points += 10 * numPieces;
            pointsText.text = points.ToString();
            potionsText.text = "x" + potionsNum.ToString();
        }
    }

    /// <summary>
    /// Método para gestionar la UI de los puntos.
    /// </summary>
    /// <param name="number"></param>
    public void PointsManager(int number)
    {
        //1-9
        if (number < 10)
        {
            pointsPositions[0].sprite = timeImages[number];
        }

        //10-99
        else if (number < 100)
        {
            pointsPositions[1].color = colorWhite;

            dec = number / 10;
            uni = number - (dec * 10);

            pointsPositions[0].sprite = timeImages[dec];
            pointsPositions[1].sprite = timeImages[uni];

        }

        //100-999
        else if (number < 1000)
        {
            pointsPositions[1].color = colorWhite;
            pointsPositions[2].color = colorWhite;

            cen = number / 100;
            dec = (number - cen * 100) / 10;
            uni = number - (cen * 100) - (dec * 10);

            pointsPositions[0].sprite = timeImages[cen];
            pointsPositions[1].sprite = timeImages[dec];
            pointsPositions[2].sprite = timeImages[uni];
        }

        //1000-9999
        else if (number < 10000)
        {
            pointsPositions[1].color = colorWhite;
            pointsPositions[2].color = colorWhite;
            pointsPositions[3].color = colorWhite;

            mil = number / 1000;
            cen = (number - mil * 1000) / 100;
            dec = (number - (mil * 1000) - (cen * 100)) / 10;
            uni = number - (mil * 1000) - (cen * 100) - (dec * 10);

            pointsPositions[0].sprite = timeImages[mil];
            pointsPositions[1].sprite = timeImages[cen];
            pointsPositions[2].sprite = timeImages[dec];
            pointsPositions[3].sprite = timeImages[uni];
        }

        //10000-99999
        else
        {
            pointsPositions[1].color = colorWhite;
            pointsPositions[2].color = colorWhite;
            pointsPositions[3].color = colorWhite;
            pointsPositions[4].color = colorWhite;


            demill = number / 10000;
            mil = (number - (demill * 10000)) / 1000;
            cen = (number - (demill * 10000) - (mil * 1000)) / 100;
            dec = (number - (demill * 10000) - (mil * 1000) - (cen * 100)) / 10;
            uni = number - (demill * 10000) - (mil * 1000) - (cen * 100) - (dec * 10);

            pointsPositions[0].sprite = timeImages[demill];
            pointsPositions[1].sprite = timeImages[mil];
            pointsPositions[2].sprite = timeImages[cen];
            pointsPositions[3].sprite = timeImages[dec];
            pointsPositions[4].sprite = timeImages[uni];
        }
    }

    #endregion

}
