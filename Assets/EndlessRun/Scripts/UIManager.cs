using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private bool paused = false;

    public List<Sprite> timeImages;
    public List<Image> timePositions;
    public List<Image> pointsPositions;

    public Text finalPointsText;

    private Animator anim;

    Color colorTransparent = new Color(0f, 0f, 0f, 0f);
    Color colorWhite = new Color(1f, 1f, 1f, 1f);

    int uni, dec, cen, mil, demill;

    int points;
    int gamemode;

    public Toggle lifesMode, onehitMode;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        Time.timeScale = 0f;

    }

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 0f;
        points = 0;
        //pointsPositions[1].color = colorTransparent;
        //pointsPositions[2].color = colorTransparent;
        //pointsPositions[3].color = colorTransparent;
        //pointsPositions[4].color = colorTransparent;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        if (paused)
        {
            Cursor.lockState = CursorLockMode.Locked;
            anim.SetTrigger("P_OUT");
            Time.timeScale = 1f;
            Cursor.visible = false;
            paused = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            anim.SetTrigger("P_IN");
            Time.timeScale = 0f;
            Cursor.visible = true;
            paused = true;
        }
    }

    public void LoadSceneByName(string name)
    {
        SceneManager.LoadScene(name);
    }

    public int StartGame()
    {
        
        anim.SetTrigger("GameIDLE");
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        pointsPositions[1].color = colorTransparent;
        pointsPositions[2].color = colorTransparent;
        pointsPositions[3].color = colorTransparent;
        pointsPositions[4].color = colorTransparent;
        InvokeRepeating("StartPoints", 1f, 0.2f);
        if (lifesMode.isOn)
        {
            return 3;
        }
        else 
        {
            return 1;
        }
    }

    public void StartPoints()
    {
        points += 1;
        PointsManager(points);
    }

    public void GameOver() {
        CancelInvoke();
        finalPointsText.text = points + " points.";

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        anim.SetTrigger("GameOver");
    }

    #region INGAME 

    public void TimeManager(int number)
    {

        if (number == -1)
        {
            //TIMESUP

            timePositions[0].color = colorTransparent;
            timePositions[1].color = colorTransparent;
            timePositions[3].color = colorTransparent;
            timePositions[4].color = colorTransparent;

            timePositions[2].sprite = timeImages[10];
            timePositions[2].rectTransform.sizeDelta = new Vector2(500f, 500f);
        }
        else if (number >= 10)
        {
            int decenas = number / 10;
            int unidades = number - (decenas * 10);
            timePositions[3].sprite = timeImages[decenas];
            timePositions[4].sprite = timeImages[unidades];

        }
        else
        {
            timePositions[3].sprite = timeImages[0];
            timePositions[4].sprite = timeImages[number];
        }
    }

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
