using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ZMUIManager : MonoBehaviour
{
    [SerializeField] private Text potionsText;
    private int potionsNum;
    [SerializeField] private Text pointsText;
    private int points;
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
        potionsNum = 0;
        points = 0;
        AddPoints(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseGame();
        }
    }

    public void AddPoints(int numPieces) {
        if (numPieces != 0) {
            potionsNum++;
            points += 10 * numPieces;
            Debug.Log(points);
            pointsText.text = points.ToString();
            potionsText.text = "x"+potionsNum.ToString();
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
