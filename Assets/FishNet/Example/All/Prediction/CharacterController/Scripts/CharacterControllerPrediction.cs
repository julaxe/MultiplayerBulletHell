﻿using FishNet.Object;
using FishNet.Object.Prediction;
using UnityEngine;
using UnityEngine.InputSystem;
/*
* 
* See TransformPrediction.cs for more detailed notes.
* 
*/

namespace FishNet.Example.Prediction.CharacterControllers
{

    public class CharacterControllerPrediction : NetworkBehaviour
    {
        #region Types.
        public struct MoveData
        {
            public float Horizontal;
            public float Vertical;
        }
        public struct ReconcileData
        {
            public Vector3 Position;
            public Quaternion Rotation;
            public ReconcileData(Vector3 position, Quaternion rotation)
            {
                Position = position;
                Rotation = rotation;
            }
        }
        #endregion

        #region Serialized.
        [SerializeField]
        private float _moveRate = 5f;
        #endregion

        #region Private.
        private CharacterController _characterController;
        private MoveData _md;
        #endregion

        private void Awake()
        {
            InstanceFinder.TimeManager.OnTick += TimeManager_OnTick;
            _characterController = GetComponent<CharacterController>();
        }

        public override void OnStartClient()
        {
            base.OnStartClient();            
            _characterController.enabled = (base.IsServer || base.IsOwner);
        }

        private void OnDestroy()
        {
            if (InstanceFinder.TimeManager != null)
            {
                InstanceFinder.TimeManager.OnTick -= TimeManager_OnTick;
            }
        }

        private void TimeManager_OnTick()
        {
            if (base.IsOwner)
            {
                Reconciliation(default, false);
                Move(_md, false);
            }
            if (base.IsServer)
            {
                Move(default, true);
                ReconcileData rd = new ReconcileData(transform.position, transform.rotation);
                Reconciliation(rd, true);
            }
        }

        private void OnMove(InputValue value)
        {
            var direction = value.Get<Vector2>();
            
            float horizontal =direction.x;
            float vertical = direction.y;

            if (horizontal == 0f && vertical == 0f)
                return;

            _md = new MoveData()
            {
                Horizontal = horizontal,
                Vertical = vertical
            };
            
        }
        private void CheckInput(out MoveData md)
        {
            md = default;

           
        }

        [Replicate]
        private void Move(MoveData md, bool asServer, bool replaying = false)
        {
            Vector3 move = new Vector3(md.Horizontal, 0f, md.Vertical).normalized + new Vector3(0f, Physics.gravity.y, 0f);
            _characterController.Move(move * _moveRate * (float)base.TimeManager.TickDelta);
        }

        [Reconcile]
        private void Reconciliation(ReconcileData rd, bool asServer)
        {
            transform.position = rd.Position;
            transform.rotation = rd.Rotation;
        }


    }


}