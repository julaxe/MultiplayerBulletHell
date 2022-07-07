using System;
using DefaultNamespace;
using SO;
using Unity.Netcode;
using UnityEngine;

namespace Player
{
    public class PlayerNetwork : NetworkBehaviour
    {
        private readonly NetworkVariable<PlayerNetworkData> _netState =
            new(writePerm: NetworkVariableWritePermission.Owner);

        private PlayerWeapon _playerWeapon;
        [SerializeField] private WeaponSO enemyWeapon;
        [SerializeField] private LayerMask enemyLayer;

        private void Awake()
        {
            _playerWeapon = GetComponent<PlayerWeapon>();
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            if (IsOwner) return;
            FlipZScale();
            ChangeToEnemy();
        }

        private void Update()
        {
            if (IsOwner)
            {
                UpdateDataOnServer();
            }
            else
            {
                ReadFromServer();
            }
        }

        void UpdateDataOnServer()
        {
            _netState.Value = new PlayerNetworkData()
            {
                Position = transform.position,
                Rotation = transform.localRotation.eulerAngles
            };
        }

        void ReadFromServer()
        {
            transform.position = _netState.Value.Position * -1.0f;
            transform.localRotation = Quaternion.Euler(_netState.Value.Rotation);
        }

        void FlipZScale()
        {
            var scale = transform.localScale;
            scale.z *= -1;
            transform.localScale = scale;
        }

        void ChangeToEnemy()
        {
            _playerWeapon.weaponSo = enemyWeapon;
            gameObject.layer = LayerMask.NameToLayer("Enemy");
        }
        
        struct PlayerNetworkData : INetworkSerializable
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