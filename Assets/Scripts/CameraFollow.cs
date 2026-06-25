using UnityEngine;
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform targetVehicle;
    [SerializeField] private Vector3 cameraOffset = new Vector3(0f, 8f, -10f);
    [SerializeField] private float smoothSpeed = 5f;

    private void LateUpdate()
    {
        if (targetVehicle == null)
        {
            return;
        }

        Vector3 targetPosition = targetVehicle.position + cameraOffset;
        Vector3 interpolatedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

        transform.position = interpolatedPosition;
    }
}