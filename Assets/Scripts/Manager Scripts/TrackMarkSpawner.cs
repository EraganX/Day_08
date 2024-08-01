using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackMarkSpawner : MonoBehaviour
{
    private Vector2 lastPosition;
    public float tracDistance = 0.2f;
    public GameObject trackPrefab;
    public int objectPoolSize = 50;
}
