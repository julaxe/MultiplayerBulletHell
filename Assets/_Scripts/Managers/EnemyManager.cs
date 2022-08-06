using FishNet.Object;
using UnityEngine;

namespace _Scripts.Managers
{
    public class EnemyManager : NetworkBehaviour
    {
        private NetworkObject _networkObject;
        
        private void Awake()
        {
            _networkObject = GetComponent<NetworkObject>();
        }

        // public override void OnNetworkSpawn()
        // {
        //     enabled = IsOwner;
        // }

        private void Update()
        {
            //if (!IsServer) return;
            if (Mathf.Abs(transform.position.y) > GameManager.Instance.gameSettings.screenHeight)
            {
                //transform.position = new Vector3(0.0f, gameSettingsSo.screenHeight * 0.5f, 0.0f);
                ReturnToBulletPool();
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (!IsOwner) return;
            //manage collision (score)
            if (other.CompareTag("Player1")) //bullet from player 1
            {
                Score_ServerRpc(10, true);
            }
            else if (other.CompareTag("Player2"))
            {
                Score_ServerRpc(10, false);
            }
            ReturnToBulletPool();
        }

        [ServerRpc]
        private void Score_ServerRpc(int value, bool isPlayer)
        {
            //Score_ClientRpc(value, isPlayer);
        }
        // [ClientRpc]
        // private void Score_ClientRpc(int value, bool isPlayer1)
        // {
        //     if (isPlayer1)
        //     {
        //         PlayersManager.Instance.player1Info.score += value;
        //     }
        //     else
        //     {
        //         PlayersManager.Instance.player2Info.score += value;
        //     }
        // }
        
        private void ReturnToBulletPool()
        {
            transform.position = new Vector3(-5.0f, 0.0f, 0.0f);
            Despawn_ServerRpc();
        }

        [ServerRpc]
        private void Despawn_ServerRpc()
        {
            _networkObject.RemoveOwnership();
            _networkObject.Despawn();
        }
    }
}