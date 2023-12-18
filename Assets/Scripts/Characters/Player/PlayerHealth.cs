using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public class PlayerHealth : MonoBehaviour
    {
        private static int _healthPoints;
        public int HealthPoints
        {
            get { return _healthPoints; }
            set { _healthPoints = value; }
        }
        public int _maxHealthPoints = 100;
        /// <summary> 玩家能量条，释放技能用
        /// <see cref = "_energyBar" />
        /// </summary>
        private static int _energyBar;  // 能量值
        public int EnergyBar
        {
            get { return _energyBar; }
            set { _energyBar = value; }
        }
        /// <summary> 玩家最大能量条，释放技能用,默认100
        /// <see cref = "_maxEnergyBar" />
        /// </summary>
        public int _maxEnergyBar = 100;

        private void Start()
        {
            SetUp();
        }

        private void SetUp()
        {
            HealthPoints = _maxHealthPoints;
            EnergyBar = _maxEnergyBar;
        }

        public bool TakeDamage(int damage)
        {
            HealthPoints -= damage;
            // TODO:  受伤状态
            Debug.Log(gameObject.name + "  受到伤害" + damage);

            bool isDead = HealthPoints <= 0;
            if (isDead)
                Die();

            return isDead;
        }

        private void Die()
        {
            Destroy(gameObject);
        }

    }
}
