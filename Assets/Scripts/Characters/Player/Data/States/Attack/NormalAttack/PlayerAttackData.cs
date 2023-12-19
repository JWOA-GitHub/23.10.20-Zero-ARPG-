using System;
using UnityEngine;

namespace JWOAGameSystem
{
    [Serializable]
    public class PlayerAttackData
    {
        [field: SerializeField] public GameObject PlayerWeapon;
        [field: SerializeField] public string PlayerWeaponTag = "MoonSword";

        public void SetUp()
        {
            PlayerWeapon = GameObject.FindGameObjectWithTag(PlayerWeaponTag);
            Debug.Log("               " + PlayerWeapon.transform.name);
        }
    }
}
