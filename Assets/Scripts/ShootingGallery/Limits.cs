using UnityEngine;

public class Limits : MonoBehaviour
{
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Target"))
        {
            //Llama a la funci�n del script Target y destruye el gameobject padre
            other.GetComponent<Target>().DestroyThis();
        }
    }
}
