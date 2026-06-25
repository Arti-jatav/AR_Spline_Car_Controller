using UnityEngine;

public class AICarMovementController : MonoBehaviour
{
    private PathWaypoints currentPath;
    private TrafficPoolManager poolManager;
    private int currentWaypointIndex;

    public void SetPoolManager(TrafficPoolManager manager)
    {
        poolManager = manager;
    }

    public void ResetToStartPath(PathWaypoints newPath)
    {
        currentPath = newPath;
        currentWaypointIndex = 0;

        if (currentPath != null && currentPath.Waypoints.Length > 0)
        {
            transform.position = currentPath.Waypoints[0].position;
            transform.LookAt(currentPath.Waypoints[0].position);
        }
    }

    private void Update()
    {
        if (currentPath == null || currentPath.Waypoints.Length == 0 || poolManager == null)
        {
            return;
        }

        Transform targetWaypoint = currentPath.Waypoints[currentWaypointIndex];
        Vector3 targetPos = targetWaypoint.position;

        transform.position = Vector3.MoveTowards(transform.position, targetPos, poolManager.TrafficSpeed * Time.deltaTime);

        Vector3 dir = targetPos - transform.position;
        if (dir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(dir);
        }

        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= currentPath.Waypoints.Length)
            {
                poolManager.ReturnCarToPool(gameObject);
            }
        }
    }
}