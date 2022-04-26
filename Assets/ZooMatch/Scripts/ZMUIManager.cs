using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZMUIManager : MonoBehaviour
{
    private bool paused = false;

    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

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

    public void LoadSceneByName(string name)
    {
        SceneManager.LoadScene(name);
    }
}
