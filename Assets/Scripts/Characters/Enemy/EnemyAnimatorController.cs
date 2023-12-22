using System.Collections.Generic;
using UnityEngine;

namespace JWOAGameSystem
{
    public enum EnemyState
    {
        Idle,
        Patrol,
        Search,
        Chase,
        Attack,
        JumpAttack,
        GetHit,
        Dead
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

        public AnimationState[] animationStates; // 存储状态和对应的动画名称

        public Animator _animator;
        private EnemyState _currentState = EnemyState.Idle;

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
                    UpdateAnimationState();
                }
            }
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            foreach (var state in animationStates)
            {
                int hash = Animator.StringToHash(state.animationName);
                _animationHashes[state.state] = hash;

                // 构建反向字典，将 EnemyState 映射到 AnimationState
                _stateToAnimationState[state.state] = state;
            }
        }

        // public void SetState(EnemyState newState)
        // {
        //     if (_currentState != newState)
        //     {
        //         _currentState = newState;
        //         UpdateAnimationState();
        //     }
        // }

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
