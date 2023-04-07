using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SGUIManager : MonoBehaviour
{

    public List<Sprite> timeImages;
    public List<Image> timePositions;
    public List<Image> pointsPositions;

    public GameObject timeSliderGO;
    public Text finalPointsText;
    private Animator anim;

    Color colorTransparent = new Color(0f, 0f, 0f, 0f);
    Color colorWhite = new Color(1f, 1f, 1f, 1f);

    int uni, dec, cen, mil, demill;

    private bool paused = false;
    bool gameIsStarted = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
    }

    /// <summary>
    /// Comienza el juego.
    /// </summary>
    public void StartGame()
    {
        gameIsStarted = true;
        anim.SetTrigger("GameIDLE");
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        pointsPositions[1].color = colorTransparent;
        pointsPositions[2].color = colorTransparent;
        pointsPositions[3].color = colorTransparent;
        pointsPositions[4].color = colorTransparent;
    }

    void Update()
    {
        if (gameIsStarted && Input.GetKeyDown(KeyCode.P)) {
            PauseGame();
        }
    }

    /// <summary>
    /// Pausa/Despausa el juego y controla el cursor.
    /// </summary>
    public void PauseGame() {
        if (paused)
        {
            Cursor.lockState = CursorLockMode.Locked;
            anim.SetTrigger("P_OUT");
            Time.timeScale = 1f;
            Cursor.visible = false;
            paused = false;
        }
        else {
            Cursor.lockState = CursorLockMode.None;
            anim.SetTrigger("P_IN");
            Time.timeScale = 0f;
            Cursor.visible = true;
            paused = true;
        }
    }

    /// <summary>
    /// Carga la escena según el nombre.
    /// </summary>
    /// <param name="name">Nombre de la escena.</param>
    public void LoadSceneByName(string name) {
        SceneManager.LoadScene(name);
    }

    /// <summary>
    /// Oculta/Muestra el slider del modo de juego por tiempo.
    /// </summary>
    /// <param name="active">Ocultar/mostrar</param>
    public void HideTimeSlider(bool active)
    {
        timeSliderGO.SetActive(active);
    }

    /// <summary>
    /// Invoca la transición de game over de la intefaz y la lógica de la finalización del juego.
    /// </summary>
    private void GameOverTransition() {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        anim.SetTrigger("GameOver");
        CancelInvoke();
    }

    /// <summary>
    /// Lógica para finalizar el juego.
    /// </summary>
    /// <param name="points">Puntos finales.</param>
    public void GameOver(int points) {
        gameIsStarted = false;
        finalPointsText.text = points + " points.";
        Invoke("GameOverTransition", 3f);

    }

    /// <summary>
    /// Gestión del tiempo.
    /// </summary>
    /// <param name="number">Segundos de juego.</param>
    public void TimeManager(int number) {

        if (number == -1)
        {
            //TIMESUP

            timePositions[0].color = colorTransparent;
            timePositions[1].color = colorTransparent;
            timePositions[3].color = colorTransparent;
            timePositions[4].color = colorTransparent;

            timePositions[2].sprite = timeImages[10];
            timePositions[2].rectTransform.sizeDelta = new Vector2(400f, 400f);
        }
        else if (number == -2) {
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
    /// <summary>
    /// Gestión de los puntos.
    /// </summary>
    /// <param name="number">Puntos.</param>
    public void PointsManager(int number) {
        //1-9
        if (number < 10) {
            pointsPositions[0].sprite = timeImages[number];
        }

        //10-99
        else if (number <100) {
            pointsPositions[1].color = colorWhite;

            dec = number / 10;
            uni = number - (dec * 10);

            pointsPositions[0].sprite = timeImages[dec];
            pointsPositions[1].sprite = timeImages[uni];

        }

        //100-999
        else if (number < 1000) {
            pointsPositions[1].color = colorWhite;
            pointsPositions[2].color = colorWhite;

            cen = number / 100;
            dec = (number - cen*100) / 10;
            uni = number - (cen * 100) - (dec * 10);

            pointsPositions[0].sprite = timeImages[cen];
            pointsPositions[1].sprite = timeImages[dec];
            pointsPositions[2].sprite = timeImages[uni];
        }

        //1000-9999
        else if (number < 10000) {
            pointsPositions[1].color = colorWhite;
            pointsPositions[2].color = colorWhite;
            pointsPositions[3].color = colorWhite;

            mil = number / 1000;
            cen = (number - mil*1000) / 100;
            dec = (number - (mil * 1000) - (cen * 100)) / 10;
            uni = number - (mil * 1000) - (cen * 100) - (dec * 10);

            pointsPositions[0].sprite = timeImages[mil];
            pointsPositions[1].sprite = timeImages[cen];
            pointsPositions[2].sprite = timeImages[dec];
            pointsPositions[3].sprite = timeImages[uni];
        }

        //10000-99999
        else {
            pointsPositions[1].color = colorWhite;
            pointsPositions[2].color = colorWhite;
            pointsPositions[3].color = colorWhite;
            pointsPositions[4].color = colorWhite;


            demill = number / 10000;
            mil = (number - (demill * 10000))/ 1000;
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
}
