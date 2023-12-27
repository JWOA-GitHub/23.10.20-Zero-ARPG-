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
            Debug.Log("<color=red>               " + PlayerWeapon.transform.name + "</color>");
        }
    }
}
