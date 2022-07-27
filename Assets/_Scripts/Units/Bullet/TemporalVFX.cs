using System;
using UnityEngine;

namespace Bullet
{
    public class TemporalVFX : MonoBehaviour
    {
        private void Start()
        {
            var ps = GetComponentInChildren<ParticleSystem>();
            Destroy(gameObject,ps.main.duration);
        }
    }
}