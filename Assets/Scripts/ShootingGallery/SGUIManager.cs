using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SGUIManager : MonoBehaviour
{

    private bool paused = false;
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
}
