using UnityEngine;

namespace TownGameOI7
{
    public class PlayerMovement : MonoBehaviour
    {
        public Animator animator;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float walkSpeed;
        [SerializeField] private float runSpeed;
        [SerializeField] private float rotationSpeed;

        private Vector3 moveDirection;
        private Vector3 velocity;
        [SerializeField] private bool isGrounded;
        [SerializeField] private float groundCheckDistance;
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private float gravity;
        [SerializeField] private float jumpHeight;
        [SerializeField] private float acceleration;
        private CharacterController controller;

        private void Start()
        {
            controller = GetComponent<CharacterController>();
            animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            Move();
        }

        void Move()
        {
            isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            float moveZ = Input.GetAxis("Vertical");
            float moveH = Input.GetAxis("Horizontal");

            moveDirection = new Vector3(moveH, 0, moveZ).normalized;

            if (moveDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                transform.rotation = toRotation;
            }

            float targetSpeed = isGrounded ? (Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed) : 0f;
            float currentSpeed = Vector3.Dot(controller.velocity, moveDirection);

            float speed = isGrounded ? Mathf.Lerp(currentSpeed, targetSpeed, acceleration * Time.deltaTime) : targetSpeed;

            moveDirection *= speed;

            if (isGrounded && Input.GetButtonDown("Jump"))
            {
                Jump();
            }

            controller.Move(moveDirection * Time.deltaTime);
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);

            UpdateAnimator(speed);
        }

        private void UpdateAnimator(float speed)
        {
            float animationSpeed = Mathf.Abs(speed) / runSpeed;
            animator.SetFloat("Speed", animationSpeed);
            animator.SetBool("IsGrounded", isGrounded);
        }

        private void Jump()
        {
            if (isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            }
        }

        public void ToggleMovement(bool enableMovement)
        {
            enabled = enableMovement;
        }
    }
}
