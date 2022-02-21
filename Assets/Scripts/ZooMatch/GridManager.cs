using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public enum PieceType { 
        EMPTY,
        NORMAL, 
        OBSTACLE,
        COUNT,
    };

    [System.Serializable]
    public struct PiecePrefab {
        public PieceType type;
        public GameObject prefab;
    };

    public int xDim;
    public int yDim;
    public float fillTime;

    public PiecePrefab[] piecePrefabs;
    public GameObject backgroundPrefab;

    private Dictionary<PieceType, GameObject> piecePrefabDict;

    private GamePiece[,] pieces; //2D array con la coma

    private bool inverse = false;

    private GamePiece pressedPiece;
    private GamePiece enteredPiece;

    void Start()
    {
        piecePrefabDict = new Dictionary<PieceType, GameObject>();
        for (int i = 0; i < piecePrefabs.Length; i++)
        {
            if (!piecePrefabDict.ContainsKey(piecePrefabs[i].type)) {
                piecePrefabDict.Add(piecePrefabs[i].type, piecePrefabs[i].prefab);
            }
        }

        for (int x = 0; x < xDim; x++)
        {
            for (int y = 0; y < yDim; y++)
            {
                GameObject background = (GameObject)Instantiate(backgroundPrefab, GetWorldPosition(x, y), Quaternion.identity);
                //background.transform.parent = transform;
                background.transform.SetParent(transform, false);
            }
        }

        pieces = new GamePiece[xDim, yDim];
        for (int x = 0; x < xDim; x++)
        {
            for (int y = 0; y < yDim; y++)
            {
                /*GameObject newPiece = (GameObject)Instantiate(piecePrefabDict[PieceType.NORMAL], new Vector3(x, y, 0), Quaternion.identity);
                //GameObject newPiece = (GameObject)Instantiate(piecePrefabDict[PieceType.NORMAL], Vector3.zero, Quaternion.identity);

                newPiece.name = "Piece(" + x + "," + y + ")";
                newPiece.transform.SetParent(transform, false);

                pieces[x, y] = newPiece.GetComponent<GamePiece>();
                pieces[x, y].Init(x, y, this, PieceType.NORMAL);

                //if (pieces[x, y].IsMovable()) {
                //    pieces[x, y].MovableComponent.Move(x, y);
                //}

                if (pieces[x, y].IsColored()) {
                    pieces[x, y].ColorComponent.SetColor((ColorPiece.ColorType)Random.Range(0, pieces[x, y].ColorComponent.NumColors));
                }*/
                SpawnNewPiece(x, y, PieceType.EMPTY);
            }
        }

        Destroy(pieces[4, 4].gameObject);
        SpawnNewPiece(4,4,PieceType.OBSTACLE);
        Destroy(pieces[5, 4].gameObject);
        SpawnNewPiece(5, 4, PieceType.OBSTACLE);

        Destroy(pieces[6, 4].gameObject);
        SpawnNewPiece(6, 4, PieceType.OBSTACLE);

        StartCoroutine(Fill());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Fill() {

        while (FillStep()) {
            inverse = !inverse;
            yield return new WaitForSeconds(fillTime);
        }
    }
    public bool FillStep() { 
        bool movedPiece = false;
        for (int y = yDim-2; y >= 0; y--)
        {
            //for (int x = 0; x < xDim; x++) {
            for (int loopX = 0; loopX < xDim; loopX++)
            {
                int x = loopX;
                if (inverse) {
                    x = xDim - 1 - loopX;
                }
                GamePiece piece = pieces[x, y];
                if (piece.IsMovable()) {
                    GamePiece pieceBelow = pieces[x, y + 1];
                    if (pieceBelow.Type == PieceType.EMPTY)
                    {
                        Destroy(pieceBelow.gameObject);
                        piece.MovableComponent.Move(x, y + 1, fillTime);
                        pieces[x, y + 1] = piece;
                        SpawnNewPiece(x, y, PieceType.EMPTY);
                        movedPiece = true;
                    }
                    else {
                        for (int diag = -1; diag <= 1; diag++) {
                            if (diag != 0) {
                                int diagX = x + diag;
                                if (inverse) {
                                    diagX = x - diag;
                                }

                                if (diagX >= 0 && diagX < xDim) {
                                    GamePiece diagonalPiece = pieces[diagX, y + 1];
                                    if (diagonalPiece.Type == PieceType.EMPTY) {
                                        bool hasPieceAbove = true;
                                        for (int aboveY = y; aboveY >= 0; aboveY--) //Loop de todas las piezas en diagonal por abajo
                                        {
                                            GamePiece pieceAbove = pieces[diagX, aboveY];
                                            if (pieceAbove.IsMovable()) {
                                                break;
                                            }
                                            //No movible, no empty
                                            else if (!pieceAbove.IsMovable() && pieceAbove.Type != PieceType.EMPTY) {
                                                hasPieceAbove = false;
                                                break;
                                            }
                                        }
                                        if (!hasPieceAbove) {
                                            Destroy(diagonalPiece.gameObject);
                                            piece.MovableComponent.Move(diagX, y + 1, fillTime);
                                            pieces[diagX, y + 1] = piece;
                                            SpawnNewPiece(x, y, PieceType.EMPTY);
                                            movedPiece = true;
                                            break;
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            }
        }

        for (int x = 0; x < xDim; x++) {
            GamePiece pieceBelow = pieces[x, 0];
            if (pieceBelow.Type == PieceType.EMPTY) {
                Destroy(pieceBelow.gameObject); //*********************
                GameObject newPiece = (GameObject)Instantiate(piecePrefabDict[PieceType.NORMAL], GetWorldPosition(x, -1), Quaternion.identity);
                newPiece.transform.SetParent(transform, false);

                pieces[x, 0] = newPiece.GetComponent<GamePiece>();
                pieces[x, 0].Init(x, -1, this, PieceType.NORMAL);
                pieces[x, 0].MovableComponent.Move(x, 0, fillTime);
                pieces[x, 0].ColorComponent.SetColor((ColorPiece.ColorType)Random.Range(0, pieces[x, 0].ColorComponent.NumColors));
                pieces[x, 0].name = "Piece[" + x + ", " + 0 + "]";
                movedPiece = true;
            }
        }
        return movedPiece;
    }

    public Vector2 GetWorldPosition(int x, int y) {
        return new Vector2(transform.position.x - xDim / 2.0f + x,
            transform.position.y + yDim /2.0f - y);
    }

    public GamePiece SpawnNewPiece(int x, int y, PieceType type) {
        GameObject newPiece = (GameObject)Instantiate(piecePrefabDict[type], GetWorldPosition(x, y), Quaternion.identity);
        newPiece.transform.SetParent(transform, false);

        pieces[x, y] = newPiece.GetComponent<GamePiece>();
        pieces[x, y].Init(x, y, this, type);
        pieces[x, y].name = "Piece["+x+", "+y+"]";
        return pieces[x, y];
    }

    public bool IsAdjacent(GamePiece piece1, GamePiece piece2) {
        if (piece1.X == piece2.X && ((int)Mathf.Abs(piece1.Y - piece2.Y) == 1))
        {
            return true;
        }
        else if (piece1.Y == piece2.Y && ((int)Mathf.Abs(piece1.X - piece2.X) == 1))
        {
            return true;
        }
        else {
            return false;       
        }
    }

    public void SwapPieces(GamePiece piece1, GamePiece piece2) {
        if (piece1.IsMovable() && piece2.IsMovable()) {
            pieces[piece1.X, piece1.Y] = piece2;
            pieces[piece2.X, piece2.Y] = piece1;

            if (GetMatch(piece1, piece2.X, piece2.Y) != null || GetMatch(piece2, piece1.X, piece1.Y) != null)
            {
                int p1X = piece1.X;
                int p1Y = piece1.Y;
                piece1.MovableComponent.Move(piece2.X, piece2.Y, fillTime);
                piece2.MovableComponent.Move(p1X, p1Y, fillTime);
            }
            else {
                pieces[piece1.X, piece1.Y] = piece1;
                pieces[piece2.X, piece2.Y] = piece2;
            }

            
        }
    }

    public void PressPiece(GamePiece piece) {
        pressedPiece = piece;
        
    }
    public void EnteredPiece(GamePiece piece)
    {
        enteredPiece = piece;
        
    }
    public void ReleasePiece()
    {
        if (IsAdjacent(pressedPiece, enteredPiece)) {
            
            SwapPieces(pressedPiece, enteredPiece);
        }
    }

    public List<GamePiece> GetMatch(GamePiece piece, int newX, int newY) {
        if (piece.IsColored()) {
            ColorPiece.ColorType color = piece.ColorComponent.Color;
            List<GamePiece> horizontalPieces = new List<GamePiece>();
            List<GamePiece> verticalPieces = new List<GamePiece>();
            List<GamePiece> matchingPieces = new List<GamePiece>();

            //HORIZONTAL
            horizontalPieces.Add(piece);
            for (int direction = 0; direction <= 1; direction++)
            {
                for (int xOff = 1; xOff < xDim; xOff++)
                {
                    int x;
                    if (direction == 0) //IZQUIERDA
                    {
                        x = newX - xOff;
                    }
                    else {  //DERECHA
                        x = newX + xOff;
                    }
                    if (x < 0 || x >= xDim) {   //LIMITES
                        break;
                    }

                    if (pieces[x, newY].IsColored() && pieces[x, newY].ColorComponent.Color == color)
                    {
                        horizontalPieces.Add(pieces[x, newY]);
                    }
                    else {
                        break;
                    }

                }
            }


            if (horizontalPieces.Count >= 3){
                for (int i = 0; i < horizontalPieces.Count; i++)
                {
                    matchingPieces.Add(horizontalPieces[i]);
                }
            }

            if (matchingPieces.Count >= 3) {
                return matchingPieces;
            }

            //VERTICAL
            verticalPieces.Add(piece);
            for (int direction = 0; direction <= 1; direction++)
            {
                for (int yOff = 1; yOff < xDim; yOff++)
                {
                    int y;
                    if (direction == 0) //ARRIBA
                    {
                        y = newY - yOff;
                    }
                    else
                    {  //ABAJO
                        y = newY + yOff;
                    }
                    if (y < 0 || y >= xDim)
                    {   //LIMITES
                        break;
                    }

                    if (pieces[newX, y].IsColored() && pieces[newX, y].ColorComponent.Color == color)
                    {
                        verticalPieces.Add(pieces[newX, y]);
                    }
                    else
                    {
                        break;
                    }

                }
            }


            if (verticalPieces.Count >= 3)
            {
                for (int i = 0; i < verticalPieces.Count; i++)
                {
                    matchingPieces.Add(verticalPieces[i]);
                }
            }

            if (matchingPieces.Count >= 3)
            {
                return matchingPieces;
            }


        }
        return null;
    } 
}
