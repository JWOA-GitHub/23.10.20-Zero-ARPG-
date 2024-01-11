using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public enum EnemyState
    {
        Idle,
        Patrol,
        Chase,
        GetHit,
        Dead,

        Search,

        JumpForward,
        JumpBackwards,

        Attack,
        JumpAttack,


    }

    public class EnemyAnimatorController : MonoBehaviour
    {
        [System.Serializable]
        public struct AnimationState
        {
            public EnemyState state;
            public string animationName;
            public float _transitionDuration;
        }

        [SerializeField] private CharacterController _character;
        public AnimationState[] animationStates; // 存储状态和对应的动画名称

        public Animator _animator;
        private EnemyState _currentState = EnemyState.Idle;

        [HideInInspector] public int animationMoveID;
        [HideInInspector] public Vector3 animationMoveDir = Vector3.zero;
        public float animationMoveSpeedModifier = 1f;
        [SerializeField, Header("动画移动障碍层")] private LayerMask GroundLayer;

        // private int[] _animationHashes;
        private Dictionary<EnemyState, int> _animationHashes = new Dictionary<EnemyState, int>();
        public Dictionary<EnemyState, AnimationState> _stateToAnimationState = new Dictionary<EnemyState, AnimationState>();


        public EnemyState EnemyState
        {
            get => _currentState;
            set
            {
                if (_currentState != value)
                {
                    _currentState = value;

                    switch (_currentState)
                    {
                        case EnemyState.JumpBackwards:
                            SetAnimationMoveBase(transform.forward, 7);
                            // Debug.Log("forward : " + -transform.forward + "root:" + transform.root.forward);
                            break;
                        case EnemyState.GetHit:
                            // Debug.Log("forward : " + transform.forward + "root:" + transform.root.forward);
                            SetAnimationMoveBase(transform.forward, 5);
                            SoundManger.Instance.PlayAudio(Globals.S_Hurting);
                            break;
                        default:
                            SetAnimationMoveBase(Vector3.zero, 1);
                            break;
                    }
                    // Debug.Log(gameObject.name + "   敌人状态：   " + _currentState);
                    UpdateAnimationState();
                }
            }
        }

        private void Awake()
        {
            _character = GetComponent<CharacterController>();
            _animator = GetComponent<Animator>();
            foreach (var state in animationStates)
            {
                int hash = Animator.StringToHash(state.animationName);
                _animationHashes[state.state] = hash;

                // 构建反向字典，将 EnemyState 映射到 AnimationState
                _stateToAnimationState[state.state] = state;
            }

            if (Animator.StringToHash("AnimationMove") != 0)
            {
                // SetAnimationMoveBase(transform.forward, 2);
                animationMoveID = Animator.StringToHash("AnimationMove");
            }
        }

        #region AnimationMove部分
        private void Update()
        {
            if (animationMoveID != 0 && animationMoveDir != Vector3.zero)
            {
                // 动画中的Curve曲线 决定动画的移动位置！
                CharacterMoveInterface(animationMoveDir,
                _animator.GetFloat(animationMoveID) * animationMoveSpeedModifier);
            }
        }

        public void SetAnimationMoveBase(Vector3 moveDir, float moveSpeedModeifier)
        {
            animationMoveDir = moveDir;
            animationMoveSpeedModifier = moveSpeedModeifier;
        }

        /// <summary>
        /// 检测移动方向是否有碰撞
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        protected bool CanAnimationMotion(Vector3 dir)
        {
            return Physics.Raycast(transform.position + transform.up * .5f, dir.normalized * _animator.GetFloat(animationMoveID), out var hit, 1f, GroundLayer);
        }

        /// <summary>
        /// Animation动作位移
        /// </summary>
        /// <param name="moveDirection"></param>
        /// <param name="moveSpeed"></param>
        public void CharacterMoveInterface(Vector3 moveDirection, float moveSpeed)
        {
            if (!CanAnimationMotion(moveDirection))
            {
                Vector3 movementDirection = moveDirection.normalized;

                _character.Move(moveSpeed * Time.deltaTime * movementDirection);

                // if (moveSpeed * Time.deltaTime * movementDirection != Vector3.zero)
                // Debug.Log(gameObject.name + "         动画位移力度  " + moveSpeed + "  " + (moveSpeed * Time.deltaTime * movementDirection));
            }
        }

        #endregion

        private void UpdateAnimationState()
        {
            float transitionDuration = GetTransitionDurationForState(_currentState);
            int animationHash = GetAnimationHashForState(_currentState);

            _animator.Play(animationHash, (int)transitionDuration, 0f); // 使用动态的动画哈希值和过渡时间
            // _animator.CrossFade(animationHash, transitionDuration); // 使用动态的动画哈希值和过渡时间


            // // 在animationStates数组中找到对应状态的动画名称并播放动画
            // foreach (var state in animationStates)
            // {
            //     if (state.state == _currentState)
            //     {
            //         transitionDuration = state._transitionDuration > 0 ? state._transitionDuration : transitionDuration;
            //         _animator.CrossFade(state.animationName, transitionDuration); // 使用动态的动画名称
            //         break;
            //     }
            // }
        }

        private float GetTransitionDurationForState(EnemyState state)
        {
            float defaultDuration = 0.5f; // 默认的过渡时间

            foreach (var animState in animationStates)
            {
                if (animState.state == state && animState._transitionDuration > 0)
                {
                    return animState._transitionDuration;
                }
            }

            return defaultDuration;
        }

        private int GetAnimationHashForState(EnemyState state)
        {
            if (_animationHashes.TryGetValue(state, out int hash))
            {
                return hash;
            }
            return 0;
        }


    }
}
