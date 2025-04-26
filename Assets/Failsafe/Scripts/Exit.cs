using UnityEngine;

namespace Failsafe.Scripts
{
    public class Exit : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
}
