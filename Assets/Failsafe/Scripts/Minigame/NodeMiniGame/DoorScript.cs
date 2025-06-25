using UnityEngine;

public class DoorScript: MonoBehaviour
{
    private Animator _animator;
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    public void CloseOpenDoor(bool IsPowered)
    {
        if (IsPowered)
        {
            Debug.Log("Open Door");
            _animator.SetBool("isOpen", IsPowered);
        }
        else
        {
            Debug.Log("Close Door");
            _animator.SetBool("isOpen", IsPowered);
        }
    }
    private void Update()
    {
        //Debug.Log();
    }
}
