using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//GamePiece class, has all the Piece components
//Also contais Mouse events that comunicates with GridManager
public class GamePiece : MonoBehaviour
{
    [SerializeField]
    private int xX;
    [SerializeField]
    private int yY;

    public int X {
        get { return xX; }
        set {
            xX = value;
            if (IsMovable()) {
                xX = value;
            }
        }
    }
    public int Y
    {
        get { return yY; }
        set
        {
            yY = value;
            if (IsMovable())
            {
                yY = value;
            }
        }
    }

    private GridManager.PieceType type;
    public GridManager.PieceType Type
    {
        get { return type; }
    }

    private GridManager grid;
    public GridManager GridRef
    {
        get { return grid; }
    }

    private MovablePiece movableComponent;
    public MovablePiece MovableComponent {
        get { return movableComponent; }
    }

    private ColorPiece colorComponent;
    public ColorPiece ColorComponent
    {
        get { return colorComponent; }
    }

    private ClearablePiece clearComponent;
    public ClearablePiece ClearComponent
    {
        get { return clearComponent; }
    }

    private ClearLine clearLineComponent;
    public ClearLine ClearLineComponent
    {
        get { return clearLineComponent; }
    }

    private void Awake()
    {
        movableComponent = GetComponent<MovablePiece>();
        colorComponent = GetComponent<ColorPiece>();
        clearComponent = GetComponent<ClearablePiece>();
        clearLineComponent = GetComponent<ClearLine>();
    }

    public void Init(int _x, int _y, GridManager _grid, GridManager.PieceType _type) {
        xX = _x;
        yY = _y;
        grid = _grid;
        type = _type;
    }

    public bool IsMovable() {
        return movableComponent != null;
    }

    public bool IsColored()
    {
        return colorComponent != null;
    }
    public bool IsClearable() {
        return clearComponent != null;
    }

    #region MOUSE EVENTS
    private void OnMouseEnter()
    {
        grid.EnteredPiece(this);
        
    }
    private void OnMouseDown()
    {
        grid.PressPiece(this);
    }
    private void OnMouseUp()
    {
        grid.ReleasePiece();
    }
    #endregion
}
