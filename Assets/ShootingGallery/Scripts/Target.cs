using UnityEngine;

public class Target : MonoBehaviour
{
    GameObject myParent;
    Animator parentAnimator;
    SGGameManager gameMan;

    float health = 10f;
    public bool alive = true;
    public int myDirection;
    public int type;

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
            myParent.transform.Translate(Vector3.left * (type * 2) * Time.deltaTime);
        }
        else {
            //Direction 1 IZQUIERDA
            myParent.transform.Translate(Vector3.right * (type * 2) * Time.deltaTime);
        }
    }

    /// <summary>
    /// Lógica para que los patitos reciban daño.
    /// </summary>
    /// <param name="dmg">Daño recibido.</param>
    public void TakeDamage(float dmg)
    {
        //La condición impide que el jugador dispare de nuevo a un patito que está muriendo
        if (alive) {
            SoundManager.Instance.PlaySFX("Duck");
            health -= dmg;
            if (health <= 0)
            {
                alive = false;
                Die();
            }
        }
        
    }

    /// <summary>
    /// LLama a la animación de morir del patito y añade los puntos.
    /// </summary>
    void Die()
    {
        parentAnimator.SetTrigger("Die");
        gameMan.AddPoints(100 * type); //CAMBIAR DEPENDIENDO DEL PATITO
    }

    /// <summary>
    /// Destruye el objeto padre.
    /// </summary>
    public void DestroyThis() {
        Destroy(myParent);
    }


}
