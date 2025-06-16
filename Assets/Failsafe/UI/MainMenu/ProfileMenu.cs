using System.Collections.Generic;
using UnityEngine;

public enum ProfileClickState
{
    Select, Delete
}

public class ProfileMenu : MonoBehaviour
{
    [SerializeField] Transform _profileScrollContent;
    [SerializeField] Profile _profilePrefab;
    [SerializeField] Transform _buttonNewProfile;
    
    List<Profile> _profiles = new List<Profile>();
    Profile _selectedProfile;
    ProfileClickState _profileClickState = ProfileClickState.Select;
     
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
    
    public void ProfileClickAction(Profile clickedProfile)
    {
        switch (_profileClickState)
        {
            case ProfileClickState.Select:
                _selectedProfile = clickedProfile;
                break;
            case ProfileClickState.Delete:
                _profiles.Remove(clickedProfile);
                if (_selectedProfile == clickedProfile)
                    _selectedProfile = null;
                Destroy(clickedProfile.gameObject);
                break;
        }
        RerenderProfiles();
    }

    public void ChangeProfileClickState()
    {
        _profileClickState = _profileClickState == ProfileClickState.Select? ProfileClickState.Delete: ProfileClickState.Select;
    }
}
