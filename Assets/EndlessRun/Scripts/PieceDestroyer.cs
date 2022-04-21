using UnityEngine;

public class PieceDestroyer : MonoBehaviour
{

    public GameObject player;

    private void Awake()
    {
        player = GameObject.Find("Player");
    }
    private void Update()
    {
        if (Vector3.Distance(player.transform.position, this.transform.position) > 100f) {
            Destroy(this.gameObject);
        }
    }
}
