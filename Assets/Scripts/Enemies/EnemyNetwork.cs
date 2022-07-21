using SO;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies
{
    public class EnemyNetwork : NetworkBehaviour
    {
        [SerializeField] private GameSettingsSO gameSettings;

        public void Initialize(bool isPlayer1, float xPosition)
        {
            if (isPlayer1)
            {
                transform.localRotation = Quaternion.Euler(90.0f,180.0f,0.0f);
                transform.position = new Vector3(xPosition, -gameSettings.screenHeight*0.5f, 0.0f);
            }
            else
            {
                transform.localRotation = Quaternion.Euler(-90.0f,0.0f,0.0f);
                transform.position = new Vector3(xPosition, gameSettings.screenHeight*0.5f, 0.0f);
            }
        }

    }
}