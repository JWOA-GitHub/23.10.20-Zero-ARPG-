using UnityEngine;

namespace JWOAGameSystem
{
    [RequireComponent(typeof(PlayerInput))]
    public class Player : CharactersBase
    {
        [field: Header("References")]
        [field: SerializeField] public PlayerSO Data { get; set; }

        [field: Header("Collisions")]
        // [field: SerializeField] public CapsuleColliderUtility ColliderUtility { get; private set; }
        [field: SerializeField] public PlayerCapsuleColliderUtility ColliderUtility { get; private set; }
        [field: SerializeField] public PlayerLayerData LayerData { get; private set; }

        [field: Header("Cameras")]
        [field: SerializeField] public PlayerCameraUtility CameraUtility { get; private set; }

        [field: Header("Animations")]
        [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }

        // TODO：character
        public Rigidbody Rigidbody { get; private set; }
        public Animator Animator { get; private set; }
        [Tooltip("玩家输入动作表管理")] public PlayerInput Input { get; private set; }

        public Transform MainCameraTransform { get; private set; }

        [Tooltip("玩家移动状态机")] private PlayerMovementStateMachine movementStateMachine;

        public Player(string name, int startingLevel) : base(name, startingLevel)
        {
            Debug.Log($"player:  {name}     {startingLevel}");
        }

        private new void Awake()
        {
            // Data.weapon = GameObject.FindGameObjectWithTag("MoonSword");
            // GameObject wea = Instantiate(Data.weapon, transform.position, Quaternion.identity);
            // Debug.LogError("   " + Data.weapon.activeInHierarchy + "    " + wea.name);
            base.Awake();

            HideCursor();

            // TODO：character?
            Rigidbody = GetComponent<Rigidbody>();
            Animator = GetComponentInChildren<Animator>();
            Input = GetComponent<PlayerInput>();

            ColliderUtility.Initialize(gameObject);
            ColliderUtility.CalculateCapsuleColliderDimensions();
            CameraUtility.Initialize();
            AnimationData.Initialize();

            MainCameraTransform = Camera.main.transform;

            movementStateMachine = new PlayerMovementStateMachine(this);
        }

        private void OnValidate()
        {
            ColliderUtility.Initialize(gameObject);
            ColliderUtility.CalculateCapsuleColliderDimensions();
        }

        private void Start()
        {
            movementStateMachine.InitState(movementStateMachine.IdingState);
        }

        private void OnTriggerEnter(Collider collider)
        {
            movementStateMachine.OnTriggerEnter(collider);
        }

        private void OnTriggerExit(Collider collider)
        {
            movementStateMachine.OnTriggerExit(collider);
        }

        private new void Update()
        {
            base.Update();
            movementStateMachine.HandleInput();

            movementStateMachine.LogicUpdate();
        }

        private void FixedUpdate()
        {
            movementStateMachine.PhysicsUpdate();
        }

        public void OnMovementStateAnimationEnterEvent()
        {
            movementStateMachine.OnAnimationEnterEvent();
        }

        public void OnMovementStateAnimationExitEvent()
        {
            movementStateMachine.OnAnimationExitEvent();
        }

        public void OnMovementStateAnimationTransitionEvent()
        {
            movementStateMachine.OnAnimationTransitionEvent();
        }

        /// <summary> 在需要隐藏鼠标光标的地方调用此函数
        /// </summary>
        public void HideCursor()
        {
            Cursor.visible = false; // 隐藏鼠标光标
            Cursor.lockState = CursorLockMode.Locked; // 锁定鼠标在屏幕中心
        }

        /// <summary> 在需要显示鼠标光标的地方调用此函数
        /// </summary>
        public void ShowCursor()
        {
            Cursor.visible = true; // 显示鼠标光标
            Cursor.lockState = CursorLockMode.None; // 解锁鼠标
        }
    }
}
