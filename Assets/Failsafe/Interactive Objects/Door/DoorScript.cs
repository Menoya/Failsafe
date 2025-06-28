using UnityEngine;

public class DoorScript: MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private bool _isPowered;
    [SerializeField] private bool _isOpen;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    public void OnPowered()
    {
        Debug.Log("Door power on");
        _isPowered = true;
    }
    public void OffPowered()
    {
        Debug.Log("Door power off");
        _isPowered = false;
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
