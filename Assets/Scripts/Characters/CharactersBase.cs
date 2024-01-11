using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace JWOAGameSystem
{
    [System.Serializable]
    public class Skill
    {
        public string skillName;
        public Transform spawnPos;
        public int mpCost;
        public float damage;
        public float cooldownTime; // 冷却时间
        private float cooldownTimer = 0f; // 计时器
        // ParticleSystem particleSystem;
        // float duration;
        // bool isUsing = false;
        // public Skill()
        // {
        // if (skillEffect.GetComponent<ParticleSystem>() != null)
        // particleSystem = skillEffect.GetComponent<ParticleSystem>();
        // }

        public bool IsOnCooldown()
        {
            return cooldownTimer > 0f;
        }

        public bool UseSkill(CharactersBase character)
        {
            if (!IsOnCooldown() && character.Mp >= mpCost)
            {
                character.Mp -= mpCost;

                // skillEffect; 
                // EffectManager.Instance.SpawnEffect(skillName, spawnPos);

                // particleSystem = skillEffect.GetComponent<ParticleSystem>();
                // // 获取到粒子系统的持续时间
                // duration = particleSystem.main.duration;

                // // 确保粒子系统存在并且未在播放
                // if (particleSystem != null && !particleSystem.isPlaying)
                // {
                //     // 播放特效
                //     // particleSystem.Play();
                //     isUsing = true;
                // }
                // TODO: 造成伤害值
                Debug.Log($"{character.name} <color=red>    使用了技能  {skillName}</color>");
                // character.TakeDamage(damage);
                cooldownTimer = cooldownTime; // 启动冷却计时器
                                              // 其他技能效果的逻辑
                return true;
            }
            else
            {
                if (IsOnCooldown())
                {
                    Debug.Log(skillName + "    Skill is on cooldown!");
                }
                else
                {
                    Debug.Log("Not enough MP to use this skill!");
                }
                // 其他处理
                return false;
            }
        }

        public void UpdateCooldown()
        {
            if (cooldownTimer > 0f)
            {
                // Debug.Log($"<color=green>{skillName} cool     {cooldownTimer}</color>");
                cooldownTimer -= Time.deltaTime;
                if (cooldownTimer < 0f)
                {
                    cooldownTimer = 0f;
                }
            }
        }
    }

    public class CharactersBase : MonoBehaviour, IAgent
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

        private float hp;
        public int maxHp = 100;

        /// <summary> 玩家能量条，释放技能用
        /// <see cref = "mp" />
        /// </summary>
        private float mp;  // 能量值

        /// <summary> 玩家最大能量条，释放技能用,默认100
        /// <see cref = "maxMp" />
        /// </summary>
        public int maxMp = 100;

        [SerializeField] private float attackDamage = 30;
        public float AttackDamage
        {
            get => attackDamage;
            set => attackDamage = value;
        }

         [SerializeField] private float skillDamage = 40;
        public float SkillDamage
        {
            get => skillDamage;
            set => skillDamage = value;
        }

        private float defense;
        public float Defense
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
                //     Debug.Log($"<color=yellow>{gameObject.name}   受伤了</color>");
                return isHurting;
            }
            set => isHurting = value;
        }

        private bool isDead = false;
        public bool IsDead
        {
            get
            {
                // if (hp <= 0)
                //     Debug.LogError(gameObject.name + "血量  见底： " + (Hp <= 0));
                return hp <= 0;
            }
            protected set => isDead = value;
        }

        /// <summary> 是否处于无敌帧时间 
        /// <see cref="InvincibleFrame"/>
        /// </summary>
        private bool invincibleFrame = false;
        public bool InvincibleFrame
        {
            get => invincibleFrame;
            set => invincibleFrame = value;
        }

        private bool isAnimationMoveing = false;
        [HideInInspector]
        public bool IsAnimationMoveing
        {
            get => isAnimationMoveing;
            set => isAnimationMoveing = value;
        }

        [SerializeField] private UnityEvent onHpUpdateEvent;
        [SerializeField] private UnityEvent onDeadEvent;
        #endregion


        // [SerializeField] private AudioSource audioSource;
        // [SerializeField] private List<AudioClip> audioClips;

        public List<Skill> skills;
        // public List<Equipment> equipments;
        // public List<StatusEffect> statusEffects;

        public float Hp
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

        public float Mp
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

        protected void Update()
        {
            UpdateSkillCooldowns();
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
            onHpUpdateEvent?.Invoke();
            // Debug.Log("<color=green>" + $"{gameObject.name}剩余血量  {Hp} </color>");
        }

        protected virtual void Dead()
        {
            onDeadEvent?.Invoke();
        }


        #region Main Methods
        public virtual void GetDamage(float damage, Vector3 pos)
        {
            Hp -= damage;
            isHurting = true;
        }

        private void UpdateSkillCooldowns()
        {
            foreach (Skill skill in skills)
            {
                skill.UpdateCooldown();
            }
        }

        public void UseSkill(int skillIndex)
        {
            if (skillIndex >= 0 && skillIndex < skills.Count)
            {
                skills[skillIndex].UseSkill(this);
            }
            else
            {
                Debug.Log(" Invalid skill index!");
            }
        }


        #endregion

    }
}
