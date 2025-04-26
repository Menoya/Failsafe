using UnityEngine;

namespace Failsafe.Scripts.FPSCounter
{
    public class FPSCounter : MonoBehaviour
    {

        [SerializeField] private GameObject fpsGameObject;

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                fpsGameObject.SetActive(!fpsGameObject.activeSelf);
            }
        }
    }
}
