using UnityEngine;

public class Target : MonoBehaviour
{
    GameObject myParent;
    Animator parentAnimator;
    SGGameManager gameMan;
    public float health = 10f;
    private bool alive = true;
    public int myDirection;

    void Awake()
    {
        myParent = this.transform.parent.gameObject;
        parentAnimator = this.GetComponentInParent<Animator>();

        gameMan = GameObject.Find("_GameManager").GetComponent<SGGameManager>();
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
        //La condición impide que el jugador dispare de nuevo a un patito que está muriendo
        if (alive) {
            health -= dmg;
            if (health <= 0)
            {
                alive = false;
                Die();
            }
        }
        
    }

    void Die()
    {
        parentAnimator.SetTrigger("Die");
        gameMan.AddPoints(100); //CAMBIAR DEPENDIENDO DEL PATITO
    }

    public void DestroyThis() {
        Destroy(myParent);
    }


}
