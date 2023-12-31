using System;
using UnityEngine;

namespace JWOAGameSystem
{
    [Serializable]
    public class PlayerAttackData
    {
        [field: SerializeField] public WeaponManager PlayerWeapon;
        [field: SerializeField] public string PlayerWeaponTag = "MoonSword";

        public void SetUp()
        {
            Debug.Log("<color=red>               " + PlayerWeapon.transform.name + "</color>");
        }
    }
}
