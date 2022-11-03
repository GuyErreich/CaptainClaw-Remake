using UnityEngine; 

namespace CaptainClaw.Scripts.Player
{
    public class PlayerInputManager : MonoBehaviour {
        [SerializeField] private float smoothMovementTime = 0.2f;
        [SerializeField, Range(0.001f, 0.01f)] private float smoothMovementThreshHold = 0.005f;
        // [SerializeField] AbilityStorage abilityStorage;

        private PlayerControls controls;
        private PlayerControls.CharacterActions characterInput;
        // private PlayerControls.SlimeRepoActions slimeRepoInput;
        private InputReceiver receiver;

        private Vector2 smoothMovement, movement;
        private bool isMoving ,isJumping, isRunnig;

        private Vector2 currentMovementInput, smoothMovementVelocity;

        private void Awake() {
            //to lock in the centre of window
            Cursor.lockState = CursorLockMode.Locked;
            //to hide the curser
            Cursor.visible = false;

            this.controls = new PlayerControls();

            this.CharacterInput();
            // this.SlimeRepoInput();
        }

        private void Update() {
            var smoothedMovement = Vector2.SmoothDamp(this.smoothMovement, this.currentMovementInput, ref this.smoothMovementVelocity, this.smoothMovementTime);
            var x = (smoothedMovement.x < smoothMovementThreshHold) && (smoothedMovement.x > -smoothMovementThreshHold) ? 0 : smoothedMovement.x;
            var y = (smoothedMovement.y < smoothMovementThreshHold) && (smoothedMovement.y > -smoothMovementThreshHold) ? 0 : smoothedMovement.y;
            this.smoothMovement = new Vector2(x, y);

            InputReceiver.Receive(this.movement ,this.smoothMovement, this.isRunnig, this.isJumping);
        }

        private void CharacterInput() {
            this.characterInput = this.controls.Character;

            this.characterInput.Movement.performed += ctx => this.currentMovementInput  = ctx.ReadValue<Vector2>();
            this.characterInput.Movement.performed += ctx => this.movement  = ctx.ReadValue<Vector2>();
            this.characterInput.Run.performed += ctx => this.isRunnig = ctx.ReadValueAsButton();
            this.characterInput.Jump.started += ctx => this.isJumping = ctx.ReadValueAsButton();
            this.characterInput.Jump.canceled += ctx => this.isJumping = ctx.ReadValueAsButton();
        }

        // private void SlimeRepoInput() {
        //     this.slimeRepoInput = this.controls.SlimeRepo;

        //     this.slimeRepoInput.Purple.started += _ => {
        //         this.jellyShoot.SlimeColor = Colors.Types.Purple;
                
        //         SlimeChangeEvent?.Invoke(Colors.Types.Purple);
        //     };
        //     this.slimeRepoInput.Green.started += _ => {
        //         var index = this.abilityStorage.Keys.IndexOf(Colors.Types.Green);
        //         if (this.abilityStorage.Values[index])
        //             this.jellyShoot.SlimeColor = Colors.Types.Green;
                
        //         SlimeChangeEvent?.Invoke(Colors.Types.Green);
        //     };
        // }

        private void OnEnable() {
            controls.Enable();
        }

        private void OnDestroy() {
            controls.Disable();
        }
    }
}
