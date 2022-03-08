using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearLine : ClearablePiece
{
    public bool isRow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Clear()
    {
        base.Clear();
        if (isRow)
        {
            Piece.GridRef.ClearRow(Piece.Y);
        }
        else {
            Piece.GridRef.ClearColumn(Piece.X);
        }
    }
}
