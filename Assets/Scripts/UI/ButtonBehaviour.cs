using SO;
using Unity.Netcode;
using UnityEngine;

namespace UI
{
    public class ButtonBehaviour : NetworkBehaviour
    {
        public GameObject enemyPrefab;
        [SerializeField] private GameSettingsSO gameSettings;

        public void SpawnEnemy()
        {
            SpawnEnemy_ServerRpc(OwnerClientId);
        }

        [ServerRpc]
        private void SpawnEnemy_ServerRpc(ulong clientId)
        {
            var newEnemy = Instantiate(enemyPrefab);
            float randomX = Random.Range(-gameSettings.screenWidth*0.4f, gameSettings.screenWidth*0.4f);
            newEnemy.transform.position = new Vector3(randomX, -gameSettings.screenHeight*0.5f, 0.0f);
            newEnemy.GetComponent<NetworkObject>().SpawnWithOwnership(clientId);
        }

        [ClientRpc]
        private void SpawnEnemy_ClientRpc()
        {
            var newEnemy = Instantiate(enemyPrefab);
            float randomX = Random.Range(-gameSettings.screenWidth*0.4f, gameSettings.screenWidth*0.4f);
            newEnemy.transform.position = new Vector3(randomX, -gameSettings.screenHeight*0.5f, 0.0f);
        }
    }
}
