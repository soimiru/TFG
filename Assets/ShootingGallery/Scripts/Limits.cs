using UnityEngine;

public class Limits : MonoBehaviour
{
    private SGGameManager sgManager;

    void Awake() {
        sgManager = FindObjectOfType<SGGameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Target"))
        {
            //Llama a la función del script Target y destruye el gameobject padre
            other.GetComponent<Target>().DestroyThis();
            if (sgManager.mode == 0 && other.GetComponent<Target>().alive) {
                sgManager.GameOver();
            }
        }
    }
}
