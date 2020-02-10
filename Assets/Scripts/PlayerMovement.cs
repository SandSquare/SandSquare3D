using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        #region Unity properties

        [SerializeField, Range(0, 50)]
        private float walkSpeed = 4.0f;

        [SerializeField, Range(0, 50)]
        private float runSpeed = 8.0f;

        [SerializeField, Range(0, 50)]
        private float airControlSpeed = 6.0f;

        [SerializeField, Range(0, 50)]
        private float jumpSpeed = 8.0f;

        [SerializeField, Range(0, 20)]
        private float gravityMultiplier = 2f;

        [SerializeField, Range(0, 20)]
        private float keepOnGroundForce = 2f;

        [SerializeField, Range(0, 1f)]
        private float groundDistanceTolerance = 0.1f;

        #endregion

        #region Movement state variables

        private Vector3 velocity = Vector3.zero;

        public Vector3 Velocity
        {
            get => velocity;
            set => velocity = value;
        }

        public bool IsGrounded { get; private set; }

        private Vector2 input = Vector2.zero;

        private bool hitCeiling;
        private CollisionFlags collisionFlags;
        private bool isWalking;

        private PlayerInput inputs;

        private Vector3 rayPositionOffset = Vector3.zero;

        private Vector3 targetRotation;

        #endregion

        #region Components

        //private GameInput gameInput;
        private CharacterController characterController;

        #endregion

        #region Unity events

        private void Start()
        {
            //gameInput = FindObjectOfType<GameInput>();
            characterController = GetComponent<CharacterController>();

            inputs = GetComponent<PlayerInput>();
        }

        private void Update()
        {
            float speed = GetInput();

            Transform tr = transform;
            Vector3 targetMovement = Vector3.forward * input.y + Vector3.right * input.x;

            rayPositionOffset.y = characterController.height / 2f;

            // Check if there's something solid close below us
            bool didHit = Physics.SphereCast(tr.position + rayPositionOffset,
                characterController.radius * 0.8f, Vector3.down, out var hit,
                rayPositionOffset.y + groundDistanceTolerance, Physics.AllLayers, QueryTriggerInteraction.Ignore);

            float hitAngle = Vector3.Angle(Vector3.up, hit.normal);

            // Adjust direction based on ground slope, but only if the slope isn't too steep
            if (hitAngle < characterController.slopeLimit)
            {
                targetMovement = Vector3.ProjectOnPlane(targetMovement, hit.normal).normalized;
            }
            else
            {
                targetMovement.Normalize();
            }               

            // Apply movement only when on (or near) ground and player isn't moving upwards
            if ((characterController.isGrounded || didHit) && velocity.y <= 0f)
            {
                IsGrounded = true;
                velocity.x = targetMovement.x * speed;
                velocity.z = targetMovement.z * speed;

                // Apply a bit of force to help keep us grounded
                if (characterController.isGrounded) velocity.y = -keepOnGroundForce;

                if (inputs.JumpInput.triggered)
                {
                    velocity.y = jumpSpeed;
                }
            }
            else
            {
                IsGrounded = false;

                Vector3 airVelocity = new Vector3(inputs.MoveInput.x, transform.position.y, inputs.MoveInput.y) * airControlSpeed;

                velocity = Vector3.Lerp(targetMovement, airVelocity, 0.3f);

                ////if (velocity.sqrMagnitude < runSpeed * runSpeed)
                //velocity.x += (targetMovement.x * speed) * Time.deltaTime;
                //velocity.z += (targetMovement.z * speed) * Time.deltaTime;
            }

            // Apply constant gravity to also help keep us grounded
            velocity += gravityMultiplier * Time.deltaTime * Physics.gravity;

            collisionFlags = characterController.Move(velocity * Time.deltaTime);

            targetRotation.x = transform.position.x + velocity.x * 100;
            targetRotation.z = transform.position.z + velocity.z * 100;
            targetRotation.y = transform.position.y;

            if(input != Vector2.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(targetRotation - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 10f * Time.deltaTime);
            }
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if ((collisionFlags & CollisionFlags.Below) == CollisionFlags.Below)
            {
                hitCeiling = false;
                return;
            }

            // Stop upwards velocity if we hit something above, and reset when we return to ground
            if ((collisionFlags & CollisionFlags.Above) != CollisionFlags.Above || hitCeiling ||
                velocity.y <= 0) return;

            hitCeiling = true;
            velocity.y = 0;
        }

        #endregion

        #region Movement

        private float GetInput()
        {
            input.x = inputs.MoveInput.x;
            input.y = inputs.MoveInput.y;

            if (input.sqrMagnitude > 1)
                input.Normalize();

            isWalking = false;

            return isWalking ? walkSpeed : runSpeed;
        }

        public void ResetMovement()
        {
            velocity = Vector3.zero;
            input = Vector2.zero;
        }

        #endregion
    }
}
