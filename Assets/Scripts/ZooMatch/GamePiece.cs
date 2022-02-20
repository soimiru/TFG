using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePiece : MonoBehaviour
{
    private int x;
    private int y;

    public int X {
        get { return x; }
        set {
            if (IsMovable()) {
                x = value;
            }
        }
    }
    public int Y
    {
        get { return y; }
        set
        {
            if (IsMovable())
            {
                y = value;
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

    private void Awake()
    {
        movableComponent = GetComponent<MovablePiece>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(int _x, int _y, GridManager _grid, GridManager.PieceType _type) {
        x = _x;
        y = _y;
        grid = _grid;
        type = _type;
    }

    public bool IsMovable() {
        return movableComponent != null;
    }
}
