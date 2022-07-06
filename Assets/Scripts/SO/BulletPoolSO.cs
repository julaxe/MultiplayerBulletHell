using System;
using System.Collections.Generic;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "BulletPoolSO", menuName = "Weapon/BulletPool", order = 0)]
    public class BulletPoolSO : ScriptableObject
    {
        public Queue<GameObject> bulletPool;
        public BulletSO bulletSo;
        public int initialAmountOfBullets;

        public void InitializePool()
        {
            bulletPool = new Queue<GameObject>();
            for (int i = 0; i < initialAmountOfBullets; i++)
            {
                var newBullet = Instantiate(bulletSo.bulletPrefab);
                AddBullet(newBullet);
            }
        }

        public void AddBullet(GameObject bullet)
        {
            bullet.SetActive(false);
            bulletPool.Enqueue(bullet);
        }

        public GameObject GetBulletFromPool()
        {
            var bullet = bulletPool.Count == 0 ? Instantiate(bulletSo.bulletPrefab) : bulletPool.Dequeue();
            bullet.SetActive(true);
            return bullet;
        }
    }
}