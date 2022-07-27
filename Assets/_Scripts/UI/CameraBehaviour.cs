using UnityEngine;

namespace UI
{
    public class CameraBehaviour : MonoBehaviour
    {
        public void RotateCamera()
        {
            transform.Rotate(new Vector3(0.0f,0.0f,180.0f));
        }
    }
}
