using UnityEngine;

public class DoorScript: MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private bool _isPowered;
    [SerializeField] private bool _isOpen;
    [SerializeField] private Light[] _panelLights;// для отображения места для интерактива с дверью

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    public void OnPowered()
    {
        Debug.Log("Door power on");
        _isPowered = true;
        foreach (Light light in _panelLights)
            light.enabled = true;
    }
    public void OffPowered()
    {
        Debug.Log("Door power off");
        _isPowered = false;
        foreach (Light light in _panelLights)
            light.enabled = false;
    }
    private void OpenCloseDoor()
    {
        _isOpen = !_isOpen;
        _animator.SetBool("isOpen", _isOpen);
        Debug.Log("Active Door");
    }
    private void OnMouseDown() //для примера вызов открытия/закрытия дверей
    {
        if (_isPowered)
            OpenCloseDoor();
    }
}
