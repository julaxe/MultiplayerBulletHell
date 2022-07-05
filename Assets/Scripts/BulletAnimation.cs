using System.Collections.Generic;
using SO;
using UnityEngine;

namespace DefaultNamespace
{
    public class BulletAnimation : MonoBehaviour
    {
        [SerializeField] private List<ParticleSystem> trails;
        [SerializeField] private BulletSO bulletSo;

        private void DestroyParticles()
        {
            
        }
        
    }
}