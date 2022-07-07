using System;
using Unity.Netcode;
using UnityEngine;

namespace Enemies
{
    public class EnemyNetwork : NetworkBehaviour
    {
        private readonly NetworkVariable<EnemyNetworkData> _netState = 
            new(writePerm: NetworkVariableWritePermission.Owner);

        public override void OnNetworkSpawn()
        {
            if (IsOwner) return;
            FlipZScale();
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

        void SendDataToServer()
        {
            _netState.Value = new EnemyNetworkData()
            {
                Position = transform.position
            };
        }

        void ReadDataFromServer()
        {
            transform.position = _netState.Value.Position * -1.0f;
        }
        void FlipZScale()
        {
            var scale = transform.localScale;
            scale.z *= -1;
            transform.localScale = scale;
        }

        struct EnemyNetworkData : INetworkSerializable
        {
            private float _x, _y;

            internal Vector3 Position
            {
                get => new Vector3(_x, _y, 0);
                set
                {
                    _x = value.x;
                    _y = value.y;
                }
            }
            public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
            {
                serializer.SerializeValue(ref _x);
                serializer.SerializeValue(ref _y);
            }
        }
    }
}