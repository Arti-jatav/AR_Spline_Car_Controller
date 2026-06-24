using UnityEngine;
using UnityEngine.UI;

public class AR_UIManager : MonoBehaviour
{
    [SerializeField] private Button tyreBtn;
    [SerializeField] private Button doorBtn;
    [SerializeField] private Button hoodBootBtn;

    private CarAnimController targetCarController;
    private bool isTyreRotating = false;
    private bool areDoorsOpen = false;
    private bool isHoopOpen = false;


    void Start()
    {
        tyreBtn.onClick.AddListener(OnToggleTyreClicked);
        doorBtn.onClick.AddListener(OnToggleDoorClicked);
        hoodBootBtn.onClick.AddListener(OnToggleHoopClicked);
    }

    public void RegisterCar(GameObject car)
    {
        targetCarController = car.GetComponent<CarAnimController>();
    }

    private void OnToggleTyreClicked()
    {
        if (targetCarController == null)
            return;

        if (isHoopOpen || areDoorsOpen)
            return;

        isTyreRotating = !isTyreRotating;
        targetCarController.ToggleTyreRotation(isTyreRotating);
    }

    private void OnToggleDoorClicked()
    {
        if (targetCarController == null)
            return;

        if (isHoopOpen || isTyreRotating)
            return;

        areDoorsOpen = !areDoorsOpen;
        targetCarController.ToggleDoor();
    }

    private void OnToggleHoopClicked()
    {
        if (targetCarController == null)
            return;

        if (isTyreRotating || areDoorsOpen)
            return;

        isHoopOpen = !isHoopOpen;
        targetCarController.ToggleHoodBoot();
    }
}