using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "WeaponSO", menuName = "Weapon/Weapon", order = 0)]
    public class WeaponSO : ScriptableObject
    {
        public  string weaponName;
        public BulletPoolSO bulletPoolSo;
        public int ammo;
    }
}