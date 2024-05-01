using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    public Camera fpsCam;

    public GameObject bulletPrefab;
    public Transform shootingPoint;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    /// <summary>
    /// Lógica detrás de los disparos.
    /// </summary>
    public void Shoot() 
    {
        SoundManager.Instance.PlaySFX("Shot");

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();

            GameObject bullet = (GameObject)Instantiate(bulletPrefab, new Vector3(0, 0, 0), Quaternion.identity);

            LineRenderer bulletLineRenderer = bullet.GetComponent<LineRenderer>();
            bulletLineRenderer.SetPosition(0, shootingPoint.position);
            bulletLineRenderer.SetPosition(1, hit.point);

            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }

    }
}
