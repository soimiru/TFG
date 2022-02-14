using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SGGameManager : MonoBehaviour
{

    public List<Transform> spawnPoints;
    public List<GameObject> patitos;
    public Canvas mainCanvas;
    public Text pointsText;

    private int points = 0;
    private int randomIndex;

    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn() {
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
        StartCoroutine(WaitForSpawn());
    }


    private IEnumerator WaitForSpawn() {
        yield return new WaitForSeconds(1);

        Spawn();
    }

    public void AddPoints(int amount) 
    {
        points += amount;
        pointsText.text = "Points: " + points;
    }
}
