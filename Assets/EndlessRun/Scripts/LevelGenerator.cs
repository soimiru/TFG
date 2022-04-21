using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PIECE = 50f;

    [SerializeField] private Transform starterPiece;
    [SerializeField] private List<Transform> levelPieces;
    [SerializeField] private GameObject player;

    private Vector3 lastEndPosition;

    private void Awake()
    {
        lastEndPosition = starterPiece.Find("EndPosition").position;
        
        for (int i = 0; i < 5; i++)
        {
            SpawnLevelPiece();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL_PIECE) {
            SpawnLevelPiece();
        }
        
    }

    private void SpawnLevelPiece() {
        Transform chosenLevelPiece = levelPieces[Random.Range(0, levelPieces.Count)];
        Transform lastLevelPieceTransform = SpawnLevelPiece(chosenLevelPiece, lastEndPosition);
        lastEndPosition = lastLevelPieceTransform.Find("EndPosition").position;
    }

    private Transform SpawnLevelPiece(Transform levelPart, Vector3 position) {
        Transform pieceTransform = Instantiate(levelPart, position, Quaternion.identity);
        return pieceTransform;
    }
}
