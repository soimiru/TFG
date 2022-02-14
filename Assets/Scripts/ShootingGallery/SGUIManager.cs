using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SGUIManager : MonoBehaviour
{

    private bool paused = false;

    public List<Sprite> timeImages;
    public List<Image> timePositions;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            PauseGame();
        }
    }

    public void PauseGame() {
        if (paused)
        {
            Time.timeScale = 1f;
            paused = false;
        }
        else {
            Time.timeScale = 0f;
            paused = true;
        }
    }

    public void TimeManager(int number) {

        if (number == -1)
        {
            //TIMESUP
            Color colorTransparent = new Color(0f, 0f, 0f, 0f);
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
        else {
            timePositions[3].sprite = timeImages[0];
            timePositions[4].sprite = timeImages[number];
        }
    }
}
