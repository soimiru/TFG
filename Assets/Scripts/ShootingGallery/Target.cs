using UnityEngine;

public class Target : MonoBehaviour
{
    GameObject myParent;
    Animator parentAnimator;
    public float health = 10f;
    public int myDirection;

    void Awake()
    {
        myParent = this.transform.parent.gameObject;
        parentAnimator = this.GetComponentInParent<Animator>();
    }

    void Update()
    {
        if (myDirection == 0)
        {
            //Direction 0 DERECHA
            myParent.transform.Translate(Vector3.left * 2 * Time.deltaTime);
        }
        else {
            //Direction 1 IZQUIERDA
            myParent.transform.Translate(Vector3.right * 2 * Time.deltaTime);
        }
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        if (health <= 0) {
            Die();
        }
    }

    void Die()
    {
        parentAnimator.SetTrigger("Die");
    }

    public void DestroyThis() {
        Destroy(myParent);
    }


}
