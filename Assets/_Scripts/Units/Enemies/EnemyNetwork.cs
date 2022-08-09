using _Scripts.Managers;
using FishNet.Object;
using SO;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.Units.Enemies
{
    public class EnemyNetwork : NetworkBehaviour
    {
        [SerializeField] private GameSettingsSO gameSettings;
        private bool _initialized;

        public override void OnStartNetwork()
        {
            base.OnStartNetwork();
            _initialized = false;
            enabled = IsOwner;
        }

        private void Update()
        {
            if (!_initialized)
            {
                float randomX = Random.Range(-gameSettings.screenWidth*0.4f, gameSettings.screenWidth*0.4f);
                Initialize(randomX, Player.Instance.isPlayer1);
            }
        }

        public void Initialize(float xPosition, bool isPlayer1)
        {
            if (isPlayer1)
            {
                transform.localRotation = Quaternion.Euler(90.0f,180.0f,0.0f);
                transform.position = new Vector3(xPosition, -gameSettings.screenHeight*0.4f, 0.0f);
            }
            else
            {
                transform.localRotation = Quaternion.Euler(-90.0f,0.0f,0.0f);
                transform.position = new Vector3(xPosition, gameSettings.screenHeight*0.4f, 0.0f);
            }

            _initialized = true;
        }

    }
}