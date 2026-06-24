using UnityEngine;

public class CarAnimController : MonoBehaviour
{
    [SerializeField] private Animator carAnimator;
    [SerializeField] private Transform[] wheels;
    [SerializeField] private float rotationSpeed = 500f;

    private bool isTyreRotating = false;
    private bool isDoorOpen = false;
    private bool isHoodBootOpen = false;

    private const string TRIGGER_DOOR_OPEN = "ToggleDoorOpen";
    private const string TRIGGER_DOOR_CLOSE = "ToggleDoorClose";
    private const string TRIGGER_HOOD_BOOT_OPEN = "ToggleHoodBootOpen";
    private const string TRIGGER_HOOD_BOOT_CLOSE = "ToggleHoodBootClose";

    void Update()
    {
        if (isTyreRotating)
        {
            foreach (Transform wheel in wheels)
            {
                if (wheel != null)
                {
                    wheel.Rotate(Vector3.right * rotationSpeed * Time.deltaTime, Space.Self);
                }
            }
        }
    }

    public void ToggleTyreRotation(bool isRotating)
    {
        isTyreRotating = isRotating;
    }

    public void ToggleDoor()
    {
        if (carAnimator != null)
        {
            isDoorOpen = !isDoorOpen;

            if (isDoorOpen)
            {
                carAnimator.SetTrigger(TRIGGER_DOOR_OPEN);
            }
            else
            {
                carAnimator.SetTrigger(TRIGGER_DOOR_CLOSE);
            }
        }
    }

    public void ToggleHoodBoot()
    {
        if (carAnimator != null)
        {
            isHoodBootOpen = !isHoodBootOpen;

            if (isHoodBootOpen)
            {
                carAnimator.SetTrigger(TRIGGER_HOOD_BOOT_OPEN);
            }
            else
            {
                carAnimator.SetTrigger(TRIGGER_HOOD_BOOT_CLOSE);
            }
        }
    }
}