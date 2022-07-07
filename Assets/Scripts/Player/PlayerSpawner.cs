using System;
using SO;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Player
{
    public class PlayerSpawner : NetworkBehaviour
    {
        public GameObject enemyPrefab;
        [SerializeField] private GameSettingsSO gameSettings;

        private void OnGUI()
        {
            if (!IsOwner) return;
            GUILayout.BeginArea(new Rect(10,10,50,50));
            if (GUILayout.Button("Enemy")) SpawnEnemy();
            GUILayout.EndArea();
        }

        private void SpawnEnemy()
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