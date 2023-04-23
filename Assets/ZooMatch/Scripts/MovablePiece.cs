using System.Collections;
using UnityEngine;

/// <summary>
/// Componente para gestionar el movimiento de las piezas.
/// </summary>
public class MovablePiece : MonoBehaviour
{
    private GamePiece piece;
    private IEnumerator moveCoroutine;

    private void Awake()
    {
        piece = GetComponent<GamePiece>();
    }

    /// <summary>
    /// M�todo que gestiona el movimiento de las piezas.
    /// </summary>
    /// <param name="newX">Nueva coordenada X</param>
    /// <param name="newY">Nueva coordenada Y</param>
    /// <param name="time">Tiempo que tarda en transcurrir la animaci�n de movimiento</param>
    public void Move(int newX, int newY, float time) {
        /*
        piece.X = newY;
        piece.Y = newY;

        piece.transform.localPosition = piece.GridRef.GetWorldPosition(newX, newY);*/

        if (moveCoroutine != null) {
            StopCoroutine(moveCoroutine);
        }
        moveCoroutine = MoveCoroutine(newX, newY, time);
        StartCoroutine(moveCoroutine);
    }

    /// <summary>
    /// M�todo que mejora y anima el movimiento de las piezas.
    /// </summary>
    /// <param name="newX">Nueva coordenada X</param>
    /// <param name="newY">Nueva coordenada Y</param>
    /// <param name="time">Tiempo que tarda en transcurrir la animaci�n de movimiento</param>
    /// <returns></returns>
    private IEnumerator MoveCoroutine(int newX, int newY, float time) {

        this.name = "Piece [" + newX + ", " + newY + "]";
        piece.X = newX;
        piece.Y = newY;

        Vector3 startPos = transform.position;
        Vector3 endPos = piece.GridRef.GetWorldPosition(newX, newY);

        for (float t = 0; t <= 1 * time; t += Time.deltaTime) {
            piece.transform.position = Vector3.Lerp(startPos, endPos, t / time);
            yield return 0;
        }
        piece.transform.position = endPos;
    }
}
