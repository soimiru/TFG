using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearablePiece : MonoBehaviour
{
    public AnimationClip clearAnimation;

    private bool isBeingCleared = false;

    public bool IsBeingCleared {
        get { return isBeingCleared; }
    }

    private GamePiece piece;

    private void Awake()
    {
        piece = GetComponent<GamePiece>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
