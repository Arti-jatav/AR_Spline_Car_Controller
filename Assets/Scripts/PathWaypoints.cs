using UnityEngine;

public class PathWaypoints : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;

    public Transform[] Waypoints => waypoints;
}
