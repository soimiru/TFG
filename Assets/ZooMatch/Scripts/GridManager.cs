using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//All the grid logic. Clearing Pieces
public class GridManager : MonoBehaviour
{
    public enum PieceType { 
        EMPTY,
        NORMAL, 
        OBSTACLE,
        ROW_CLEAR,
        COLUMN_CLEAR,
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

                //TOP
                if (y == 0)
                {
                    //TOP LEFT
                    if (x == 0)
                    {
                        background.GetComponent<TileBackground>().SetTile(TileBackground.TileType.TOPLEFT);
                    }
                    //TOP RIGHT
                    else if (x == xDim - 1)
                    {
                        background.GetComponent<TileBackground>().SetTile(TileBackground.TileType.TOPRIGHT);
                        
                    }
                    //TOP
                    else
                    {
                        background.GetComponent<TileBackground>().SetTile(TileBackground.TileType.TOP);
                    }
                }
                //BOTTOM
                else if (y == yDim - 1)
                {
                    //BOTTOM LEFT
                    if (x == 0)
                    {
                        background.GetComponent<TileBackground>().SetTile(TileBackground.TileType.BOTTOMLEFT);

                    }
                    //BOTTOM RIGHT
                    else if (x == xDim - 1)
                    {
                        background.GetComponent<TileBackground>().SetTile(TileBackground.TileType.BOTTOMRIGHT);
                    }
                    //BOTTOM
                    else
                    {
                        background.GetComponent<TileBackground>().SetTile(TileBackground.TileType.BOTTOM);
                    }
                }
                //CENTER
                else {
                    if (x == 0) {
                        background.GetComponent<TileBackground>().SetTile(TileBackground.TileType.LEFT);
                    }
                    else if (x == xDim-1) {
                        background.GetComponent<TileBackground>().SetTile(TileBackground.TileType.RIGHT);
                    }
                    else{
                        int rand = Random.Range(0, 2);
                        if (rand == 0)
                        {
                            background.GetComponent<TileBackground>().SetTile(TileBackground.TileType.CENTER1);
                        }
                        else if (rand == 1)
                        {
                            background.GetComponent<TileBackground>().SetTile(TileBackground.TileType.CENTER2);
                        }
                        else {
                            background.GetComponent<TileBackground>().SetTile(TileBackground.TileType.CENTER3);
                        }
                    }
                }
                background.name = "BG["+x+", "+y+"]";
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
        //Destroy(pieces[5, 4].gameObject);
        //SpawnNewPiece(5, 4, PieceType.OBSTACLE);

        //Destroy(pieces[6, 4].gameObject);
        //SpawnNewPiece(6, 4, PieceType.OBSTACLE);

        StartCoroutine(Fill());

    }

    public IEnumerator Fill() {

        bool needsRefill = true;
        while (needsRefill) {

            yield return new WaitForSeconds(fillTime);

            while (FillStep()) {
                inverse = !inverse;
                yield return new WaitForSeconds(fillTime);
            }
            needsRefill = ClearAllValidMatches();
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


                //if (ClearAllValidMatches()) { 
                //    StartCoroutine(Fill());
                //}

                ClearAllValidMatches();

                if (piece1.Type == PieceType.ROW_CLEAR || piece1.Type == PieceType.COLUMN_CLEAR) {
                    ClearGridPiece(piece1.X, piece1.Y);
                }
                else if (piece2.Type == PieceType.ROW_CLEAR || piece2.Type == PieceType.COLUMN_CLEAR)
                {
                    ClearGridPiece(piece2.X, piece2.Y);
                }

                pressedPiece = null;
                enteredPiece = null;

                StartCoroutine(Fill());

            }
            else {
                pieces[piece1.X, piece1.Y] = piece1;
                pieces[piece2.X, piece2.Y] = piece2;
            }
        }
    }

    public bool ClearAllValidMatches()
    {
        bool needsRefill = false;
        for (int x = 0; x < xDim; x++)
        {
            for (int y = 0; y < yDim; y++)
            {
                if (pieces[x, y].IsClearable()) {
                    List<GamePiece> match = GetMatch(pieces[x, y], x, y);

                    if (match != null) {
                        //SpecialPieceType
                        PieceType specialPieceType = PieceType.COUNT;
                        GamePiece randomPiece = match[Random.Range(0, match.Count)];
                        int specialPieceX = randomPiece.X;
                        int specialPieceY = randomPiece.Y;
                        Debug.Log(match.Count);

                        if (match.Count == 4) {
                            if (pressedPiece == null || enteredPiece == null)   //No se han hecho movimientos pero se ha generado una combinacion
                            {
                                specialPieceType = (PieceType)Random.Range((int)PieceType.ROW_CLEAR, (int)PieceType.COLUMN_CLEAR);
                            }
                            else if (pressedPiece.Y == enteredPiece.Y)  //Coinciden en el eje Y
                            {
                                specialPieceType = PieceType.ROW_CLEAR;
                            }
                            else if (pressedPiece.X == enteredPiece.X)
                            {  //Coinciden en el eje X
                                specialPieceType = PieceType.COLUMN_CLEAR;
                            }
                        }

                        for (int i = 0; i < match.Count; i++) {
                            if (ClearGridPiece(match[i].X, match[i].Y)) {
                                needsRefill = true;

                                if (match[i] == pressedPiece || match[i] == enteredPiece) {
                                    specialPieceX = match[i].X;
                                    specialPieceY = match[i].Y;
                                }
                            }
                        }
                        //Si specialPieceType no es el tipo base, se crea una nueva pieza especial donde corresponda.
                        if (specialPieceType != PieceType.COUNT) {
                            Destroy(pieces[specialPieceX, specialPieceY]);
                            GamePiece newPiece = SpawnNewPiece(specialPieceX, specialPieceY, specialPieceType);

                            //CAMBIAR
                            if ((specialPieceType == PieceType.ROW_CLEAR || specialPieceType == PieceType.COLUMN_CLEAR) &&
                                newPiece.IsColored() && match[0].IsColored()) {
                                newPiece.ColorComponent.SetColor(match[0].ColorComponent.Color);
                            }

                        }

                    }
                }
            }
        }

        return needsRefill;
    }

    public bool ClearGridPiece(int x, int y) {
        if (pieces[x, y].IsClearable() && !pieces[x, y].ClearComponent.IsBeingCleared)
        {
            pieces[x, y].ClearComponent.ClearPiece();

            SpawnNewPiece(x, y, PieceType.EMPTY);
            return true;
        }
        else {
            return false;
        }
    }

    public void ClearRow(int row) {
        for (int x = 0; x < xDim; x++) {
            if (pieces[x, row].Type != PieceType.OBSTACLE)
            {
                ClearGridPiece(x, row);
            }
        }
    }

    public void ClearColumn(int col) {
        for (int y = 0; y < yDim; y++) {
            if (pieces[col, y].Type != PieceType.OBSTACLE) { 
                ClearGridPiece(col, y);
            }
            
        }
    }

    #region PRESS-ENTERED-RELEASE LOGIC

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
    #endregion

    #region MATCH LOGIC
    public List<GamePiece> GetMatch(GamePiece piece, int newX, int newY)
    {
        if (piece.IsColored())
        {
            ColorPiece.ColorType color = piece.ColorComponent.Color;
            List<GamePiece> horizontalPieces = new List<GamePiece>();
            List<GamePiece> verticalPieces = new List<GamePiece>();
            List<GamePiece> matchingPieces = new List<GamePiece>();

            //HORIZONTAL
            horizontalPieces.Add(piece);    //Añade la primera pieza al array
            for (int direction = 0; direction <= 1; direction++)
            {
                for (int xOff = 1; xOff < xDim; xOff++)
                {
                    int x;
                    if (direction == 0) //IZQUIERDA
                    {
                        x = newX - xOff;
                    }
                    else
                    {  //DERECHA
                        x = newX + xOff;
                    }
                    if (x < 0 || x >= xDim)
                    {   //LIMITES
                        break;
                    }

                    if (pieces[x, newY].IsColored() && pieces[x, newY].ColorComponent.Color == color)
                    {
                        horizontalPieces.Add(pieces[x, newY]);
                    }
                    else
                    {
                        break;
                    }

                }
            }

            //Añadimos al array de matching si hay más de 3 piezas iguales. 
            //if (horizontalPieces.Count >= 3)
            //{
            //    for (int i = 0; i < horizontalPieces.Count; i++)
            //    {
            //        matchingPieces.Add(horizontalPieces[i]);
            //    }
            //}


            //Comprobamos combinaciones en forma de L
            if (horizontalPieces.Count >= 3)
            {
                for (int i = 0; i < horizontalPieces.Count; i++)
                {
                    matchingPieces.Add(horizontalPieces[i]);
                }

                for (int i = 0; i < horizontalPieces.Count; i++)
                {
                    for (int direction = 0; direction <= 1; direction++)
                    {
                        for (int yOff = 1; yOff < yDim; yOff++)
                        {
                            int y;
                            if (direction == 0)
                            {   //ARRIBA
                                y = newY - yOff;
                            }
                            else
                            {  //ABAJO
                                y = newY + yOff;
                            }
                            if (y < 0 || y >= yDim)
                            {   //LIMITES
                                break;
                            }

                            //Comprobar que los colores coinciden
                            if (pieces[horizontalPieces[i].X, y].IsColored() && pieces[horizontalPieces[i].X, y].ColorComponent.Color == color)
                            {
                                verticalPieces.Add(pieces[horizontalPieces[i].X, y]);
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }

                    //Si no hay suficientes piezas, limpiamos el array
                    if (verticalPieces.Count < 2)
                    {
                        verticalPieces.Clear();
                    }
                    //Si hay suficientes, las añadimos al array de "matching"
                    else
                    {
                        for (int j = 0; j < verticalPieces.Count; j++)
                        {
                            matchingPieces.Add(verticalPieces[j]);
                        }
                        break;
                    }
                }
            }

            //Devuelve el array de matching
            if (matchingPieces.Count >= 3)
            {
                return matchingPieces;
            }

            //RESET
            verticalPieces.Clear();
            horizontalPieces.Clear();


            //VERTICAL
            verticalPieces.Add(piece);
            for (int direction = 0; direction <= 1; direction++)
            {
                for (int yOff = 1; yOff < yDim; yOff++)
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
                    if (y < 0 || y >= yDim)
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

            //Añadimos al array de matching si hay más de 3 piezas iguales. 
            //if (verticalPieces.Count >= 3)
            //{
            //    for (int i = 0; i < verticalPieces.Count; i++)
            //    {
            //        matchingPieces.Add(verticalPieces[i]);
            //    }
            //}

            //Comprobamos las combinaciones en forma de L
            if (verticalPieces.Count >= 3)
            {
                for (int i = 0; i < verticalPieces.Count; i++)
                {
                    matchingPieces.Add(verticalPieces[i]);
                }
                for (int i = 0; i < verticalPieces.Count; i++)
                {
                    for (int direction = 0; direction <= 1; direction++)
                    {
                        for (int xOff = 1; xOff < xDim; xOff++)
                        {
                            int x;
                            if (direction == 0)
                            {   //IZQUIERDA
                                x = newX - xOff;
                            }
                            else
                            {  //DERECHA
                                x = newX + xOff;
                            }
                            if (x < 0 || x >= xDim)
                            {   //LIMITES
                                break;
                            }

                            //Comprobar que los colores coinciden
                            if (pieces[x, verticalPieces[i].Y].IsColored() && pieces[x, verticalPieces[i].Y].ColorComponent.Color == color)
                            {
                                horizontalPieces.Add(pieces[x, verticalPieces[i].Y]);
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }

                    //Si no hay suficientes piezas, limpiamos el array
                    if (horizontalPieces.Count < 2)
                    {
                        horizontalPieces.Clear();
                    }
                    //Si hay suficientes, las añadimos al array de "matching"
                    else
                    {
                        for (int j = 0; j < horizontalPieces.Count; j++)
                        {
                            matchingPieces.Add(horizontalPieces[j]);
                        }
                        break;
                    }
                }
            }

            //Devolvemos el array
            if (matchingPieces.Count >= 3)
            {
                return matchingPieces;
            }

        }
        return null;
    }
    #endregion
}
