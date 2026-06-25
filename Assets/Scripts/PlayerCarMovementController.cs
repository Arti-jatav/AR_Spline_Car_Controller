using UnityEngine;

public class PlayerCarMovementController : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float acceleration = 15f;
    [SerializeField] private float deceleration = 25f;
    [SerializeField] private float rotationSpeed = 15f;
    [SerializeField] private float baseScoreFactor = 1f;
    [SerializeField] private float scoreMultiplierSpeed = 0.5f;

    private PathWaypoints playerWaypoints;

    private int currentWaypointIndex = 0;
    private float currentSpeed = 0f;
    private float addedScore = 0f;
    private float currentHoldTime = 0f;

    private bool stopMovement = false;

    private void Update()
    {
        if (stopMovement || currentWaypointIndex >= playerWaypoints.Waypoints.Length)
        {
            return;
        }

        if (Input.GetMouseButton(0))
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, acceleration * Time.deltaTime);
            currentHoldTime += Time.deltaTime;
        }
        else
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.deltaTime);
            currentHoldTime = 0;
        }

        if (currentSpeed <= 0f)
        {
            return;
        }

        float activeScoreFactor = baseScoreFactor + (currentHoldTime * scoreMultiplierSpeed);
        addedScore += currentSpeed * activeScoreFactor * Time.deltaTime;

        if (addedScore >= 1f)
        {
            int pointsToAdd = (int)addedScore;
            GameManager.Instance.AddScore(pointsToAdd);
            addedScore -= pointsToAdd;
        }

        Vector3 targetPos = playerWaypoints.Waypoints[currentWaypointIndex].position;

        transform.position = Vector3.MoveTowards(transform.position, targetPos, currentSpeed * Time.deltaTime);

        Vector3 dir = targetPos - transform.position;
        if (dir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        if (Vector3.Distance(transform.position, targetPos) < Constants.MIN_DISTANCE_THRESHOLD)
        {
            if (currentWaypointIndex < playerWaypoints.Waypoints.Length - 1)
            {
                currentWaypointIndex++;
            }
            else
            {
                currentSpeed = 0f;
                stopMovement = true;
            }
        }
    }

    public void ResetPlayer(PathWaypoints waypoints)
    {
        playerWaypoints = waypoints;
        transform.position = playerWaypoints.Waypoints[0].position;
        stopMovement = false;
        currentWaypointIndex = 0;
    }

    public void StopPlayerMovement()
    {
        stopMovement = true;
    }
}