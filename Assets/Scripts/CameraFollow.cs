using System.Collections;
using UnityEngine;
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform targetVehicle;
    [SerializeField] private Vector3 cameraOffset = new Vector3(0f, 8f, -10f);
    [SerializeField] private float smoothSpeed = 5f;

    private Vector3 currentVelocity;
    private Vector3 shakeOffset;

    private void LateUpdate()
    {
        Vector3 targetPosition = targetVehicle.position + cameraOffset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothSpeed) + shakeOffset;
    }

    public void TriggerShake(float duration, float magnitude)
    {
        StartCoroutine(ShakeRoutine(duration, magnitude));
    }

    private IEnumerator ShakeRoutine(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            shakeOffset = new Vector3(x, y, 0f);
            elapsed += Time.deltaTime;

            yield return null;
        }

        shakeOffset = Vector3.zero;
    }
}