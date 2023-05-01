using System;
using PorfirioPartida.Delibeery.Common;
using UnityEngine;
using UnityEngine.UI;

namespace PorfirioPartida.Delibeery.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bee : MonoBehaviour, IInteractable
    {
        private Rigidbody2D _rb;
        public Animator animator;

        [Header("Jump Params:")]
        public float speedFactor;
        public float jumpForce;
        public float additiveForceJump;
        public float jumpCooldown;
        [Header("Rates")] public float honeyDrainRate = 0.01f;
        public bool isDraining;
        public FloatValue totalHoney;
        
        [Header("Debug Params:")]
        [SerializeField]
        private float consecutiveJumps;
        [SerializeField]
        private float jumpCooldownCounter;
        [SerializeField]
        private Vector2 lastForce; 
        private bool _isMovingRight;
        [SerializeField]
        private bool isAlive;


        void Start()
        {
            isAlive = true;
            this._rb = GetComponent<Rigidbody2D>();
            _isMovingRight = true;
            ResetConstantXSpeed();
        }

        private void ResetConstantXSpeed()
        {
            var curVel = this._rb.velocity;
            curVel.x = (_isMovingRight ? 1:-1) * speedFactor * Time.deltaTime;
            this._rb.velocity = curVel;
        }

        public void ToggleDirection()
        {
            _isMovingRight = !_isMovingRight;
            ResetConstantXSpeed();
        }

        public void Interact()
        {
            Jump();
        }

        private void Update()
        {
            jumpCooldownCounter -= Time.deltaTime;
            if (isDraining)
            {
                DrainHoney();
            }
        }
        private void Jump()
        {
            if (!isAlive)
            {
                return;
            }

            if (jumpCooldownCounter < 0)
            {
                lastForce = Vector2.up * jumpForce;
                _rb.AddForce(lastForce, ForceMode2D.Impulse);
                consecutiveJumps = 1;
                jumpCooldownCounter = jumpCooldown;
            }
            else
            {
                consecutiveJumps++;
                var jumpFactor = jumpForce + jumpForce * Mathf.Pow(additiveForceJump, consecutiveJumps);
                lastForce = Vector2.up * jumpFactor;
                _rb.AddForce(lastForce, ForceMode2D.Impulse);
                jumpCooldownCounter = jumpCooldown;
            }

        }

        public void Die()
        {
            this.isAlive = false;
            animator.SetTrigger(AnimatorConstants.TriggerDie);
        }

        public void ResumeFlying()
        {
            animator.enabled = true;
            isDraining = false;
            this._rb.gravityScale = 1;
            ToggleDirection();
            Jump();
            // ResetConstantXSpeed();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Flower")
            {
                animator.enabled = false;
                isDraining = true;
                //0 speed while draining.
                this._rb.velocity = Vector2.zero;
                this._rb.gravityScale = 0;
                DrainHoney();
            }
        }


        private void DrainHoney()
        {
            totalHoney.value += this.honeyDrainRate * Time.deltaTime;
        }
    }
}
