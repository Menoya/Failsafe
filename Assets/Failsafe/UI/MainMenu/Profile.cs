using TMPro;
using UnityEngine;

public class Profile : MonoBehaviour
{
    [SerializeField] TMP_Text _indexText;

    public void SetData(int index)
    {
        _indexText.text = "0" + index;
    }
}
