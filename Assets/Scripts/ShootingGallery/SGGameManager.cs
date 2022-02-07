using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SGGameManager : MonoBehaviour
{

    public List<Transform> spawnPoints;
    public GameObject patito;
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
        patitoNew = (GameObject)Instantiate(patito, spawnPoints[randomIndex].position, Quaternion.identity);
        if (randomIndex < 3)
        {  //SPAWNS DERECHA
            patitoNew.GetComponentInChildren<Target>().myDirection = 0;
        }
        else 
        {  //SPAWNS IZQUIERDA
            patitoNew.GetComponentInChildren<Target>().myDirection = 1;
        }
        StartCoroutine(WaitForSpawn());
    }


    private IEnumerator WaitForSpawn() {
        yield return new WaitForSeconds(5);

        Spawn();
    }
}
