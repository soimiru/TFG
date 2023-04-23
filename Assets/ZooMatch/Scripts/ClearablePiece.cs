using System.Collections;
using UnityEngine;

/// <summary>
/// Componente "Clearable" de las piezas. Sirve para indicar si la pieza se está limpiando y para ejecutar la animación. 
/// </summary>
public class ClearablePiece : MonoBehaviour
{
    public AnimationClip clearAnimation;

    private bool isBeingCleared = false;

    public bool IsBeingCleared {
        get { return isBeingCleared; }
    }

    private GamePiece piece;
    public GamePiece Piece
    {
        get { return piece; }
    }

    private void Awake()
    {
        piece = GetComponent<GamePiece>();
    }

    public virtual void Clear()
    {
        isBeingCleared = true;
        StartCoroutine(ClearCoroutine());
    }

    /// <summary>
    /// Indica que la pieza se está limpiando y llama la corutina ClearCoroutine
    /// </summary>
    public void ClearPiece() {
        isBeingCleared = true;

        StartCoroutine(ClearCoroutine());
    }

    /// <summary>
    /// Ejecuta la animación de limpieza, espera los segundos que dura la animación y destruye el game object de la pieza
    /// </summary>
    /// <returns></returns>
    private IEnumerator ClearCoroutine() {
        Animator anim = GetComponent<Animator>();

        anim.Play(clearAnimation.name);
        yield return new WaitForSeconds(clearAnimation.length);
        Destroy(gameObject);
    }
}
