using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class AR_SpawnManager : MonoBehaviour
{
    [SerializeField] private AR_UIManager arUiManager;
    [SerializeField] private ARRaycastManager raycastManager;
    [SerializeField] private GameObject carPrefab;

    private GameObject spawnedCar;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Update()
    {
        // I'm spawning one car only
        if (spawnedCar != null) return;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                TryPlaceCar(touch.position);
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            TryPlaceCar(Input.mousePosition);

        }
    }
    
    private void TryPlaceCar(Vector3 pos)
    {
        if (raycastManager.Raycast(pos, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;
            spawnedCar = Instantiate(carPrefab, hitPose.position, hitPose.rotation);

            arUiManager.RegisterCar(spawnedCar);
        }
    }
  
}