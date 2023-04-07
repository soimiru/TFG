using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PIECE = 50f;

    [SerializeField] private Transform starterPiece;
    [SerializeField] private List<Transform> levelPieces;
    [SerializeField] private List<Transform> specialPieces;
    [SerializeField] private GameObject player;
    public UIManager uiManager;

    private Vector3 lastEndPosition;
    private int pieceCount;

    public enum GameMode
    {
        LIFESMODE,
        ONEHITMODE
    };

    public GameMode gameMode;

    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
        lastEndPosition = starterPiece.Find("EndPosition").position;
        pieceCount = 0;
        
        for (int i = 0; i < 5; i++)
        {
            SpawnLevelPiece();
        }
    }

    void Update()
    {
        if (Vector3.Distance(player.transform.position, lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL_PIECE) {
            if (gameMode == GameMode.LIFESMODE && pieceCount >= 2)
            {
                SpawnSpecialPiece();
                pieceCount = 0;
            }
            else { 
                SpawnLevelPiece();
                pieceCount++;

            }
        }
    }

    /// <summary>
    /// Aplica distintos parámetros dependiendo del modo de juego.
    /// </summary>
    public void CustomizeGame() {
        int mode = uiManager.StartGame();
        if (mode == 1)
        {
            gameMode = GameMode.ONEHITMODE;
        }
        else if (mode == 3) {
            gameMode = GameMode.LIFESMODE;
        }
        player.GetComponent<PlayerMovement>().SetInitialLifes(mode);
    }

    /// <summary>
    /// Spawnea un nuevo prefab de plataforma.
    /// </summary>
    private void SpawnLevelPiece() {
        Transform chosenLevelPiece = levelPieces[Random.Range(0, levelPieces.Count)];
        Transform lastLevelPieceTransform = SpawnLevelPiece(chosenLevelPiece, lastEndPosition);
        lastEndPosition = lastLevelPieceTransform.Find("EndPosition").position;
    }

    /// <summary>
    /// Spawnea un nuevo prefab de plataforma especial.
    /// </summary>
    private void SpawnSpecialPiece() {
        Transform chosenLevelPiece = specialPieces[Random.Range(0, specialPieces.Count)];
        Transform lastLevelPieceTransform = SpawnLevelPiece(chosenLevelPiece, lastEndPosition);
        lastEndPosition = lastLevelPieceTransform.Find("EndPosition").position;
    }

    /// <summary>
    /// Instancia un nuevo prefab en la posición indicada.
    /// </summary>
    /// <param name="levelPart">El tipo de plataforma a instanciar.</param>
    /// <param name="position">El Vector3 de la posicion en la que se instanciará la plataforma.</param>
    /// <returns></returns>
    private Transform SpawnLevelPiece(Transform levelPart, Vector3 position) {
        Transform pieceTransform = Instantiate(levelPart, position, Quaternion.identity);
        return pieceTransform;
    }

}
