using System;
using PorfirioPartida.Delibeery.Common;
using PorfirioPartida.Delibeery.Manager;
using UnityEngine;

namespace PorfirioPartida.Delibeery.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bee : MonoBehaviour, IInteractable
    {
        [NonSerialized]
        public Rigidbody2D _rb;
        public Animator mainAnimator;
        public Transform bodyTransform;
        public Animator barAnimator;
        public Transform beeBarWrapper;

        [Header("Jump Params:")]
        public float speedFactor;
        public float fullHoneySpeedFactor;
        public float gravityScale;
        public float fullGravityScale;
        public float jumpForce;
        public float additiveForceJump;
        public float jumpCooldown;
        
        private float _consecutiveJumps;
        private float _jumpCooldownCounter;
        private Vector2 _lastForce; 
        
        [Header("Rates")] public float honeyDrainRate = 0.01f;
        private bool _isDraining;
        private bool _isDropping;
        
        public float localHoney;
        public float maxLocalHoney;
        public float distanceToDrainThreshold = 0.1f;
        public float resizeBodyOnFullFactor = .75f;
        
        [Header("DieParams:")]
        [SerializeField] public bool isAlive;
        private bool _isFull;
        private float _timeToDie;
        public float timeToDie = 4f;

        private float delayToDestroyAfterDie = 3f;
        private Transform _flowerTarget;

        public float Y_DOOM = -50;
        public float X_HONEYCOMB = 4;
        
        void Start()
        {
            isAlive = true;
            this._rb = GetComponent<Rigidbody2D>();
            // _isMovingRight = true;
            localHoney = 0;
            _flowerTarget = LebeelManager.Instance.flowerTransform;

            this._rb.gravityScale = gravityScale;
            ToggleDirection();
            FixHoneyBarSize();
            X_HONEYCOMB = LebeelManager.Instance.GetXHoneyCombLimit();
        }

        private void ResetConstantXSpeed()
        {
            var factor = _isFull ? fullHoneySpeedFactor : speedFactor;
            var curVel = this._rb.velocity;
            curVel.x = (IsMovingRight() ? 1:-1) * factor;
            this._rb.velocity = curVel;
        }

        private bool IsMovingRight()
        {
            return !_isFull;
        }


        public void ToggleDirection()
        {
            var localBeeTransform = this.transform;
            var scale = localBeeTransform.localScale;
            scale.x = IsMovingRight() ? 1:-1;
            localBeeTransform.localScale = scale;
            ResetConstantXSpeed();
        }

        public void Interact()
        {
            if (!isAlive)
            {
                return;
            }

            if (_isDraining)
            {
                if (IsFull())
                {
                    localHoney = maxLocalHoney;
                    ResumeFlying();
                }
                else
                {
                    mainAnimator.SetTrigger(AnimatorConstants.TriggerAnnoy);
                }

            } else if (_isDropping)
            {
                mainAnimator.SetTrigger(AnimatorConstants.TriggerAnnoy);
            }
            else
            {
                Jump();
            }
        }
        private void Update()
        {
            if (!isAlive)
            {
                return;
            }

            if (this.transform.position.y < Y_DOOM)
            {
                Die();
            }

            _jumpCooldownCounter -= Time.deltaTime;

            if (_isDraining)
            {
                if (CanDrain())
                {
                    DrainHoney();
                }
                else
                {
                    MoveTowardsFlower();
                }
                FixHoneyBarSize();
            }
            //If Unloading, also fix bar.

            if (this.transform.position.x < X_HONEYCOMB && _isFull)
            {
                if (!_isDropping)
                {
                    _isDropping = true;
                    AnimDraining(true);
                    this._rb.velocity = Vector2.zero;
                    this._rb.gravityScale = 0;
                }
                else
                {
                    DropLoad();
                }
            }
        }

        private void DropLoad()
        {
            var rate = this.honeyDrainRate * Time.deltaTime;
            this.localHoney -= rate;

            //Substract the last amount, even if smaller than rate.
            if (this.localHoney - rate < 0)
            {
                rate = this.localHoney; //ensure 0s.
                this.localHoney = 0;
                
                LebeelManager.Instance.HoneyComb.AddHoney(maxLocalHoney);
            }

            
            ResizeBody();
            FixHoneyBarSize();
            
            if (this.localHoney <= 0)
            {
                _isDraining = false;
                _isDropping = false;
                _isFull = false;
                
                AnimIsFull(false);
                AnimDraining(false);

                this._rb.gravityScale = gravityScale;
                this.localHoney = 0;
                
                FixHoneyBarSize();
                ToggleDirection();
            }
        }

        private void FixHoneyBarSize()
        {
            var newScale = beeBarWrapper.localScale;
            newScale.x = GetFullPct();
            beeBarWrapper.localScale = newScale;
        }

        public float GetFullPct()
        {
            if (!isAlive) return 0;
            
            if (maxLocalHoney <= 0) return 1;
            
            return localHoney / maxLocalHoney;
        }

        private void MoveTowardsFlower()
        {
            //AnimDraining(false);
            //TODO: replace with slow motion
            this.transform.position = _flowerTarget.position;
        }

        private void AnimDraining(bool val)
        {
            mainAnimator.SetBool(AnimatorConstants.IsDraining, val);
            barAnimator.SetBool(AnimatorConstants.IsDraining, val);
        }

        private void ResizeBody()
        {
            var ls = bodyTransform.localScale;
            var factor =1 + GetFullPct() * resizeBodyOnFullFactor;
            ls.y = factor;
            ls.x = factor;
            //Debug.Log($"scale must be {ls.y}");
            bodyTransform.localScale = ls;
        }

        private void AnimIsFull(bool val)
        {
            barAnimator.SetBool(AnimatorConstants.IsFull, val);
        }

        private bool CanDrain()
        {
            var diff = this.transform.position - _flowerTarget.position;
            return diff.magnitude < distanceToDrainThreshold;
        }

        private bool IsFull()
        {
            return Math.Abs(localHoney - maxLocalHoney) < honeyDrainRate/2f;
        }

        private void Jump()
        {
            if (!isAlive)
            {
                return;
            }
            if (_rb.velocity.y < 0)
            {
                //reset gravity down only
                _rb.velocity = Vector2.zero;
            }

            
            if (_jumpCooldownCounter < 0)
            {
                _lastForce = Vector2.up * jumpForce;
                _rb.AddForce(_lastForce, ForceMode2D.Impulse);
                _consecutiveJumps = 1;
                _jumpCooldownCounter = jumpCooldown;
            }
            else
            {
                _consecutiveJumps++;
                var jumpFactor = jumpForce + jumpForce * Mathf.Pow(additiveForceJump, _consecutiveJumps);
                _lastForce = Vector2.up * jumpFactor;
                _rb.AddForce(_lastForce, ForceMode2D.Impulse);
                _jumpCooldownCounter = jumpCooldown;
            }

            ResetConstantXSpeed();
        }
        public void Die()
        {
            this.isAlive = false;
            AnimIsFull(false);
            AnimDraining(false);
            mainAnimator.ResetTrigger(AnimatorConstants.TriggerAnnoy);
            mainAnimator.SetTrigger(AnimatorConstants.TriggerDie);
            beeBarWrapper.localScale = Vector3.zero;
            this._rb.gravityScale = .8f;
            
            Dispose();
        }

        private void Dispose()
        {
            //Remove from all and adjust counter
            GetComponent<BeeGps>().Dispose();
            
            //Fix UI and Level Manager Updates.
            LebeelManager.Instance.DieCounter();
            
            Destroy(this.gameObject, delayToDestroyAfterDie);
        }

        public void ResumeFlying()
        {
            AnimIsFull(false);
            AnimDraining(false);
            _isDraining = false;
            this._rb.gravityScale = _isFull ? fullGravityScale:gravityScale;
            ToggleDirection();
            Jump();
        }

        //private void OnCollisionEnter2D(Collision2D other)
        //{
            //Debug.Log($"OnCollisionEnter2D {other.gameObject.name}");
       // }

        private void OnTriggerEnter2D(Collider2D other)
        {
            //Debug.Log($"OnTriggerEnter2D {other.name}");
            if (_isDraining) return;
            
            if (other.CompareTag(TagConstants.Flower))
            {
                if (localHoney == 0)
                {
                    AnimDraining(true);
                    _isDraining = true;
                    this._rb.velocity = Vector2.zero;
                    this._rb.gravityScale = 0;
                }
            }
        }

        private void DrainHoney()
        {
            if (_isFull)
            {
                if (_timeToDie < 0)
                {
                    Die();
                }

                _timeToDie -= Time.deltaTime;
                return;
            }

            if (!_isDraining) return;

            localHoney += this.honeyDrainRate * Time.deltaTime;
            ResizeBody();
            if (localHoney > maxLocalHoney)
            {
                localHoney = maxLocalHoney;
            }
            
            // AnimDraining(true);
            if (IsFull())
            {
                _timeToDie = timeToDie;
                _isFull = true;
                AnimIsFull(true);
            }
        }
    }
}
