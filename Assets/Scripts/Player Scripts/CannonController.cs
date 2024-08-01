using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    [SerializeField] private GameObject _cannon;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1f;
    private float nextFireTime = 0f;

    private ObjectPoolManager poolManager;

    private void Start()
    {
        poolManager = FindAnyObjectByType<ObjectPoolManager>();
    }

    private void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 direction = mousePosition -_cannon.transform.position ;

        float angle = Mathf.Atan2(direction.y,direction.x)*Mathf.Rad2Deg;

        _cannon.transform.rotation = Quaternion.Euler(0,0,angle-90f);

        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void Shoot()
    {
        var bullet = poolManager.GetPooledObject("PlayerBullet");
        bullet.transform.position = _cannon.transform.position;
        bullet.transform.rotation = _cannon.transform.rotation;

    }
}
