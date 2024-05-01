using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{

    public float duration;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyBullet", duration);
    }

    private void DestroyBullet() {

        Destroy(gameObject);
    }
}
