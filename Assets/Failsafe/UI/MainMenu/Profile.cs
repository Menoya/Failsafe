using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Profile : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    ProfileMenu _profileMenu;
    
    [SerializeField] TMP_Text _indexText;
    [SerializeField] TMP_Text _profileNameText;
    [SerializeField] GameObject _selectedFrame;
    
    [SerializeField] Material _targetMaterial;
    [SerializeField] Sprite _targetSprite;
    [SerializeField] Color _targetColor;
    [SerializeField] Image _profileBackground;
    
    
    Material _originMaterial;
    Color _originalColor;
    Sprite _originalSprite;
    
    
    private void Awake()
    {
        _profileMenu = GetComponentInParent<ProfileMenu>();
        //_profileBackground = GetComponent<Image>();
        _originalColor = _indexText.color;
        _originalSprite = _profileBackground.sprite;
        _originMaterial = _indexText.fontSharedMaterial;
    }
    
    public void SetData(int index)
    {
        _indexText.text = "0" + index;
    }
    public void OnProfileClicked()
    {
        _profileMenu.ProfileClickAction(this);
    }

    public void ShowSelectedProfile()
    {
        _selectedFrame.SetActive(true);
    }

    public void DeselectProfile()
    {
        _selectedFrame.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _profileBackground.sprite = _targetSprite;
        
        _indexText.fontSharedMaterial = _targetMaterial;
        _indexText.fontWeight = FontWeight.SemiBold;
        _profileNameText.fontSharedMaterial = _targetMaterial;
        _profileNameText.fontWeight = FontWeight.SemiBold;
        
        _indexText.color = _targetColor;
        _profileNameText.color = _targetColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _profileBackground.sprite = _originalSprite;
        
        _indexText.fontSharedMaterial = _originMaterial;
        _indexText.fontWeight = FontWeight.Regular;
        _profileNameText.fontSharedMaterial = _originMaterial;
        _profileNameText.fontWeight = FontWeight.Regular;
        
        _indexText.color = _originalColor;
        _profileNameText.color = _originalColor;
      
        
    }
}
