using UnityEngine;
using UnityEngine.EventSystems;

public class DeleteProfileDropzone : MonoBehaviour, IDropHandler
{
    ProfileMenu _profileMenu;

    void Start()
    {
        _profileMenu = GetComponentInParent<ProfileMenu>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        DraggableUI draggable = eventData.pointerDrag.GetComponent<DraggableUI>();
        if (draggable != null)
        {
            _profileMenu.ConfirmDeleteProfile(draggable.GetProfileToDelete());
        }
    }
}
