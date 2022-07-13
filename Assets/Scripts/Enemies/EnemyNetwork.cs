using SO;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies
{
    public class EnemyNetwork : NetworkBehaviour
    {
        private readonly NetworkVariable<EnemyNetworkData> _netState = 
            new(writePerm: NetworkVariableWritePermission.Owner);

        public ulong ownerId;
        public GameObject enemyPrefab;
        [SerializeField] private GameSettingsSO gameSettings;
        public bool isOwnerOfEnemy = false;

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

        private void Update()
        {
            if (IsOwner)
            {
                SendDataToServer();
            }
            else
            {
                ReadDataFromServer();
            }
        }

        [ServerRpc(RequireOwnership = false)]
        void ChangeData_ServerRpc()
        {
            SendDataToServer();
        }
        void SendDataToServer()
        {
            _netState.Value = new EnemyNetworkData()
            {
                Position = transform.position,
                Rotation = transform.localRotation.eulerAngles
            };
        }

        void ReadDataFromServer()
        {
            transform.position = _netState.Value.Position;
            transform.localRotation = Quaternion.Euler(_netState.Value.Rotation);
        }

        struct EnemyNetworkData : INetworkSerializable
        {
            private float _x, _y;
            private float _xRot, _yRot, _zRot;

            internal Vector3 Position
            {
                get => new Vector3(_x, _y, 0);
                set
                {
                    _x = value.x;
                    _y = value.y;
                }
            }

            internal Vector3 Rotation
            {
                get => new Vector3(_xRot, _yRot, _zRot);
                set
                {
                    _xRot = value.x;
                    _yRot = value.y;
                    _zRot = value.z;
                }
            }
            public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
            {
                serializer.SerializeValue(ref _x);
                serializer.SerializeValue(ref _y);
                serializer.SerializeValue(ref _xRot);
                serializer.SerializeValue(ref _yRot);
                serializer.SerializeValue(ref _zRot);
            }
        }
    }
}