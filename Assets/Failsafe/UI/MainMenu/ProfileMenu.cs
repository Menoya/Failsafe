using System.Collections.Generic;
using UnityEngine;

public class ProfileMenu : MonoBehaviour
{
    [SerializeField] Transform _profileScrollContent;
    [SerializeField] Profile _profilePrefab;
    [SerializeField] Transform _buttonNewProfile;
    
     List<Profile> _profiles = new List<Profile>();
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
        }
    }
    public void OnCloseProfilesWindow()
    {
        gameObject.SetActive(false);
    }
}
