using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent agent;
    private Vector2 previousPosition;
    public float stopDistance = 2f;

    public Transform cannon;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1f;
    private float nextFireTime;

    public float avoidanceRadius = 1f;
    public float separationStrength = 2f;

    private Vector3 randomOffset;

    private ObjectPoolManager poolManager;

    void Start()
    {
        poolManager = FindAnyObjectByType<ObjectPoolManager>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player not found");
        }

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        previousPosition = transform.position;

        SetRandomOffset();
    }

    void Update()
    {
        if (player != null)
        {
            FollowPlayer();
            RotateTowardsMovementDirection();
            RotateCannonTowardsPlayer();
            FireAtPlayer();
        }
    }

    void SetRandomOffset()
    {
        randomOffset = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5), 0);
    }

    void FollowPlayer()
    {
        Vector3 targetPosition = player.position + randomOffset;
        NavMeshHit hit;

        if (NavMesh.SamplePosition(targetPosition, out hit, 5f, NavMesh.AllAreas))
        {
            targetPosition = hit.position;
        }
        else
        {
            SetRandomOffset();
        }

        float distanceToPlayer = Vector2.Distance(transform.position, targetPosition);

        if (distanceToPlayer > stopDistance)
        {
            Vector3 avoidance = CalculateAvoidance();
            agent.SetDestination(targetPosition + avoidance);
        }
        else
        {
            agent.ResetPath();
        }
    }

    Vector3 CalculateAvoidance()
    {
        Vector3 avoidance = Vector3.zero;
        Collider[] colliders = Physics.OverlapSphere(transform.position, avoidanceRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject != gameObject && collider.CompareTag("Enemy"))
            {
                Vector3 direction = transform.position - collider.transform.position;
                avoidance += direction.normalized / direction.magnitude;
            }
        }

        return avoidance * separationStrength;
    }

    void RotateTowardsMovementDirection()
    {
        Vector2 currentPosition = transform.position;
        Vector2 movementDirection = (currentPosition - previousPosition).normalized;

        if (movementDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        }

        previousPosition = currentPosition;
    }

    void RotateCannonTowardsPlayer()
    {
        Vector2 direction = player.position - cannon.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        cannon.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90)) ;
    }

    void FireAtPlayer()
    {
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            FireBullet();
        }
    }



    void FireBullet()
    {
        //Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        var bullet = poolManager.GetPooledObject("EnemyBullet");
        bullet.transform.position = cannon.transform.position;
        bullet.transform.rotation = cannon.transform.rotation;
    }

    public void BulletResoreToPool(GameObject obj)
    {
        poolManager.ReturnPoolObject(obj);
    }
}


