using Failsafe.UI.MainMenu.Popup;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum ProfileClickState
{
    Select, Delete
}

public class ProfileMenu : MonoBehaviour
{
    [SerializeField] Popup _popup;
    
    [SerializeField] Transform _profileScrollContent;
    [SerializeField] Profile _profilePrefab;
    [SerializeField] Transform _buttonNewProfile;
    
    [SerializeField] List<Profile> _profiles = new List<Profile>();
    Profile _selectedProfile;
    ProfileClickState _profileClickState = ProfileClickState.Select;
    
    //переменная чтобы удалять конкретно подписанный лямбда-метод из подписчиков поп-апа
    private UnityAction lambdaFunc;
        
    public void OnCreateNewProfile()
    {
        Profile newProfile = Instantiate(_profilePrefab, _profileScrollContent);
        _profiles.Add(newProfile);
        RerenderProfiles();
        _buttonNewProfile.SetAsLastSibling();
        
    }

    public void RerenderProfiles()
    {
        for (int i = 0; i < _profiles.Count; i++)
        {
            _profiles[i].SetData(i+1);
            _profiles[i].DeselectProfile();
            if(_selectedProfile!= null && _selectedProfile == _profiles[i])
                _profiles[i].ShowSelectedProfile();
        }
    }
    public void OnCloseProfilesWindow()
    {
        gameObject.SetActive(false);
    }

    public bool IsThisProfileSelected(Profile profileToCheck)
    {
        return _selectedProfile == profileToCheck;
    }

    public void RemoveProfile(Profile clickedProfile)
    {
        _profiles.Remove(clickedProfile);
        if (_selectedProfile == clickedProfile)
            _selectedProfile = null;
        Destroy(clickedProfile.gameObject);
        
        RerenderProfiles();
    }
    public void ProfileClickAction(Profile clickedProfile)
    {
        switch (_profileClickState)
        {
            case ProfileClickState.Select:
                _selectedProfile = clickedProfile;
                break;
            case ProfileClickState.Delete:
                _popup.Show();
                //подписываюсь к методу поп-апа через код, а не инспектор, ибо мне нужно передавать профиль, который будет удален
                //а это еще и можно сделать только с помощью лямбда-метода
                lambdaFunc = () =>
                {
                    RemoveProfile(clickedProfile);
                    //приходится удалять подписку ибо при следующем удалении уже другого профиля все еще будет попытка удалить уже удаленный профиль и будет NullReference
                    _popup.onSubmit.RemoveListener(lambdaFunc);
                };
                _popup.onSubmit.AddListener(lambdaFunc);
                _popup.onCancel.AddListener(() => _popup.onSubmit.RemoveListener(lambdaFunc));
                break;
        }
        RerenderProfiles();
    }

    public void ChangeProfileClickState()
    {
        _profileClickState = _profileClickState == ProfileClickState.Select? ProfileClickState.Delete: ProfileClickState.Select;
    }
}
