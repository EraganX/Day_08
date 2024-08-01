using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracksMarksSpawner : MonoBehaviour
{
    private Vector2 lastPosition;
    public float trackDistance = 0.2f;
    [SerializeField] private string nameTag;

    private ObjectPoolManager poolManager;


    private void Awake()
    {
        poolManager = FindAnyObjectByType<ObjectPoolManager>();
    }

    private void Start()
    {
        lastPosition = transform.position;
        
    }

    private void Update()
    {
        var distanceDriven = Vector2.Distance(transform.position,lastPosition);

        if (distanceDriven>=trackDistance)
        {
            lastPosition = transform.position;
            var track = poolManager.GetPooledObject(nameTag);
            track.transform.position = transform.position;
            track.transform.rotation = transform.rotation;
        }
    }
}
