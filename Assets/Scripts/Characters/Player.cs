using UnityEngine;

namespace JWOAGameSystem
{
    [RequireComponent(typeof(PlayerInput))]
    public class Player : MonoBehaviour
    {
        [field: Header("References")]
        [field: SerializeField] public PlayerSO Data { get; set; }

        [field: Header("Collisions")]
        [field: SerializeField] public CapsuleColliderUtility ColliderUtility { get; private set; }
        [field: SerializeField] public PlayerLayerData LayerData { get; private set; }

        // TODO：character
        public Rigidbody Rigidbody { get; private set; }
        [Tooltip("玩家输入动作表管理")] public PlayerInput Input { get; private set; }

        public Transform MainCameraTransform { get; private set; }

        [Tooltip("玩家移动状态机")] private PlayerMovementStateMachine movementStateMachine;

        private void Awake()
        {
            // TODO：character
            Rigidbody = GetComponent<Rigidbody>();
            Input = GetComponent<PlayerInput>();

            ColliderUtility.Initialize(gameObject);
            ColliderUtility.CalculateCapsuleColliderDimensions();

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

        private void Update()
        {
            movementStateMachine.HandleInput();

            movementStateMachine.LogicUpdate();
        }

        private void FixedUpdate()
        {
            movementStateMachine.PhysicsUpdate();
        }
    }
}
