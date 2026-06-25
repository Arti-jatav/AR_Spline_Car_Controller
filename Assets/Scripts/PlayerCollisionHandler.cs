using System.Collections;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] private CameraFollow cameraFollow;
    [SerializeField] private PlayerCarMovementController playerCarMovementController;
    [SerializeField] private float shakeDuration = 0.3f;
    [SerializeField] private float shakeMagnitude = 0.2f;
    [SerializeField] private float gameOverDelay = 1.5f;

    private bool hasCrashed = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (hasCrashed) return;

        if (collision.gameObject.CompareTag("AI Car"))
        {
            hasCrashed = true;
            cameraFollow.TriggerShake(shakeDuration, shakeMagnitude);
            playerCarMovementController.StopPlayerMovement();
            StartCoroutine(DelayedGameOverRoutine());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            GameManager.Instance.TriggerLevelWin();
        }
    }

    private IEnumerator DelayedGameOverRoutine()
    {
        yield return new WaitForSeconds(gameOverDelay);
        GameManager.Instance.TriggerGameOver();
    }
}