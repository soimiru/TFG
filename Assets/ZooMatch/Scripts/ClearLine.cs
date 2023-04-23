public class ClearLine : ClearablePiece
{
    public bool isRow;

    /// <summary>
    /// Borra todas las piezas de una columna o fila
    /// </summary>
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
