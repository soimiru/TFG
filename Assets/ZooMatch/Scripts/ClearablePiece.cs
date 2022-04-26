using System.Collections;
using UnityEngine;

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

    public void ClearPiece() {
        isBeingCleared = true;

        StartCoroutine(ClearCoroutine());
    }

    private IEnumerator ClearCoroutine() {
        Animator anim = GetComponent<Animator>();

        anim.Play(clearAnimation.name);
        yield return new WaitForSeconds(clearAnimation.length);
        Destroy(gameObject);
    }
}
