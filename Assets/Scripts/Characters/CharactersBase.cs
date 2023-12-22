using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public class CharactersBase : MonoBehaviour
    {
        #region 角色属性
        private string characterName = "JWOA";
        public string CharacterName
        {
            get => characterName;
            set => characterName = value;
        }

        [SerializeField] private int startingLevel = 1;
        private int level = 1;
        public int Level
        {
            get => level;
            set => level = value;
        }

        private int experience;
        public int Exp
        {
            get => experience;
            set => experience = value;
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

        [SerializeField] private int attackDamage = 10;
        public int AttackDamage
        {
            get => attackDamage;
            set => attackDamage = value;
        }

        private int defense;
        public int Defense
        {
            get => defense;
            set => defense = value;
        }

        private float movementSpeed;
        public float MovementSpeed
        {
            get => movementSpeed;
            set => movementSpeed = value;
        }

        private bool isHurting = false;
        public bool IsHurting
        {
            get
            {
                // if (isHurting)
                // Debug.Log(gameObject.name + "受伤了");
                return isHurting;
            }
            set => isHurting = value;
        }

        private bool isDead = false;
        public bool IsDead
        {
            get
            {
                if (hp <= 0)
                    Debug.LogError(gameObject.name + "血量  见底： " + (Hp <= 0));
                return hp <= 0;
            }
            protected set => isDead = value;
        }

        #endregion

        // [SerializeField] private AudioSource audioSource;
        // [SerializeField] private List<AudioClip> audioClips;

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

        }

        protected void Awake()
        {
            InitializeProperty();
        }

        public void InitializeProperty()
        {
            // 初始化属性...
            Hp = maxHp;
            Mp = maxMp;

            characterName = name;
            level = startingLevel;
            experience = 0;
        }

        // /// <summary> 播放音效
        // /// </summary>
        // /// <param name="index"></param>
        // public void PlayAudio(int index)
        // {
        //     audioSource.PlayOneShot(audioClips[index]);
        // }

        /// <summary> 当HP变化时自动调用
        /// </summary>
        protected virtual void OnHpUpdate()
        {
            Debug.Log("<color=green>" + $"{gameObject.name}剩余血量  {Hp} </color>");
        }

        protected virtual void Dead()
        {

        }

        public virtual void TakeDamage(int damage)
        {
            Hp -= damage;
            isHurting = true;
            Debug.Log(gameObject.name + " 受伤了，扣了" + damage);
        }

    }
}
