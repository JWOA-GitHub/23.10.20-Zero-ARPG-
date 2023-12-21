using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public class CharactersBase : MonoBehaviour
    {
        private string characterName = "JWOA";
        public string CharacterName
        {
            get => characterName;
            set => value = characterName;
        }

        private int level = 0;
        public int Level
        {
            get => level;
            set => value = level;
        }

        private int experience;
        public int Exp
        {
            get => experience;
            set => value = experience;
        }

        private int hp;
        public int maxHp = 100;

        /// <summary> 玩家能量条，释放技能用
        /// <see cref = "mp" />
        /// </summary>
        private int mp;  // 能量值

        /// <summary> 玩家最大能量条，释放技能用,默认100
        /// <see cref = "maxMp" />
        /// </summary>
        public int maxMp = 100;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private List<AudioClip> audioClips;

        private int attackDamage;
        public int AttackDamage
        {
            get => attackDamage;
            set => value = attackDamage;
        }

        private int defense;
        public int Defense
        {
            get => defense;
            set => value = defense;
        }

        private float movementSpeed;
        public float MovementSpeed
        {
            get => movementSpeed;
            set => value = movementSpeed;
        }

        // public List<Skill> skills;
        // public List<Equipment> equipments;
        // public List<StatusEffect> statusEffects;

        public int Hp
        {
            get => hp;
            set
            {
                hp = value;
                if (hp <= 0)
                {
                    hp = 0;
                    Invoke(nameof(Dead), 0.3f);
                }
                OnHpUpdate();
            }
        }

        public int Mp
        {
            get { return mp; }
            set { mp = value; }
        }

        public CharactersBase(string name, int startingLevel)
        {
            // 初始化属性...
            Hp = maxHp;
            Mp = maxMp;

            characterName = name;
            level = startingLevel;
            experience = 0;
        }

        /// <summary> 播放音效
        /// </summary>
        /// <param name="index"></param>
        public void PlayAudio(int index)
        {
            audioSource.PlayOneShot(audioClips[index]);
        }

        /// <summary> 当HP变化时自动调用
        /// </summary>
        protected virtual void OnHpUpdate()
        {

        }

        protected virtual void Dead()
        {

        }

        public virtual void Hurt(int damage)
        {
            Hp -= damage;
            Debug.Log(gameObject.name + " 受伤了");
        }

    }
}
