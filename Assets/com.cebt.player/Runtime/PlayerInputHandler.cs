using UnityEngine;
using UnityEngine.InputSystem;

// ReSharper disable once CheckNamespace
namespace com.cebt.player
{
    public class PlayerInputHandler : MonoBehaviour
    {
        [Header("Input Action Asset")] [SerializeField]
        private InputActionAsset playerControls;

        [Header("Action Map Name Reference")] [SerializeField]
        private string actionMapName = "Player";

        [Header("Action Name Reference")] [SerializeField]
        private string movement = "Move";

        [SerializeField] private string rotation = "Look";
        [SerializeField] private string jump = "Jump";
        [SerializeField] private string sprint = "Sprint";
        [SerializeField] private string interact = "Interact";
        [SerializeField] private string crouch = "Crouch";
        [SerializeField] private string attack = "Attack";

        private InputAction movementAction;
        private InputAction rotationAction;
        private InputAction jumpAction;
        private InputAction sprintAction;
        private InputAction interactAction;
        private InputAction crouchAction;
        private InputAction attackAction;

        public Vector2 MovementInput { get; private set; }
        public Vector2 RotationInput { get; private set; }
        public bool JumpTriggered { get; private set; }
        public bool SprintTriggered { get; private set; }
        public bool InteractTriggered { get; private set; }
        public bool CrouchTriggered { get; private set; }
        public bool AttackTriggered { get; private set; }
        

        private void Awake()
        {
            InputActionMap mapReference = playerControls.FindActionMap(actionMapName);

            movementAction = mapReference.FindAction(movement);
            rotationAction = mapReference.FindAction(rotation);
            jumpAction = mapReference.FindAction(jump);
            sprintAction = mapReference.FindAction(sprint);
            interactAction = mapReference.FindAction(interact);
            crouchAction = mapReference.FindAction(crouch);
            attackAction = mapReference.FindAction(attack);

            SubscribeActionValuesToInputEvents();
        }

        private void SubscribeActionValuesToInputEvents()
        {
            movementAction.performed += inputInfo => MovementInput = inputInfo.ReadValue<Vector2>();
            movementAction.canceled += inputInfo => MovementInput = Vector2.zero;

            rotationAction.performed += inputInfo => RotationInput = inputInfo.ReadValue<Vector2>();
            rotationAction.canceled += inputInfo => RotationInput = Vector2.zero;

            jumpAction.performed += inputInfo => JumpTriggered = true;
            jumpAction.canceled += inputInfo => JumpTriggered = false;

            sprintAction.performed += inputInfo => SprintTriggered = true;
            sprintAction.canceled += inputInfo => SprintTriggered = false;

            interactAction.performed += inputInfo => InteractTriggered = true;
            interactAction.canceled += inputInfo => InteractTriggered = false;

            crouchAction.performed += inputInfo => CrouchTriggered = true;
            crouchAction.canceled += inputInfo => CrouchTriggered = false;

            attackAction.performed += inputInfo => AttackTriggered = true;
            attackAction.canceled += inputInfo => AttackTriggered = false;
        }

        private void OnEnable()
        {
            playerControls.FindActionMap(actionMapName).Enable();
        }

        private void OnDisable()
        {
            playerControls.FindActionMap(actionMapName).Disable();
        }
    }
}