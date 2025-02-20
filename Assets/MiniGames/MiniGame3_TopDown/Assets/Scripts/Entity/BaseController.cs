using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TopDown
{
    public class BaseController : MonoBehaviour
    {
        protected Rigidbody2D _rigidbody;
        [SerializeField] private SpriteRenderer chrarcterRenderer;
        [SerializeField] private Transform weaponPivot;


        protected Vector2 movementDirection = Vector2.zero;
        public Vector2 MovemontDirection { get { return movementDirection; } }

        protected Vector2 lookDirection = Vector2.zero;
        public Vector2 LookDirection { get { return lookDirection; } }

        private Vector2 knockback = Vector2.zero;
        private float knockbackDuratiuon = 0.0f;

        protected AnimationHandler animationHandler;
        protected StatHandler statHandler;

        [SerializeField] public WeaponHandler weaponPrefab;
        protected WeaponHandler WeaponHandler;

        protected bool isAttacking;
        private float timeSinceLastAttack = float.MaxValue;
        protected virtual void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            animationHandler = GetComponent<AnimationHandler>();
            statHandler = GetComponent<StatHandler>();

            if (weaponPrefab != null)
                WeaponHandler = Instantiate(weaponPrefab, weaponPivot);
            else
                WeaponHandler = GetComponentInChildren<WeaponHandler>();
        }
        protected virtual void Start()
        {

        }
        protected virtual void Update()
        {
            HandleAction();
            Rotate(lookDirection);
            HandleAttackDelay();
        }

        protected virtual void FixedUpdate()
        {
            Movement(movementDirection);
            if (knockbackDuratiuon > 0.0f)
            {
                knockbackDuratiuon -= Time.fixedDeltaTime;
            }
        }

        protected virtual void HandleAction()
        {

        }

        private void Movement(Vector2 direction)
        {
            direction = direction * statHandler.Speed;
            if (knockbackDuratiuon > 0.0f)
            {
                direction *= 0.2f;
                direction += knockback;
            }
            _rigidbody.velocity = direction;
            animationHandler.Move(direction);

        }
        private void Rotate(Vector2 direction)
        {
            float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bool isLeft = Mathf.Abs(rotZ) > 90f;

            chrarcterRenderer.flipX = isLeft;
            if (weaponPivot != null)
            {
                weaponPivot.rotation = Quaternion.Euler(0f, 0f, rotZ);
            }
            WeaponHandler?.Rotate(isLeft);
        }

        public void ApplyKnockback(Transform other, float power, float duration)
        {
            knockbackDuratiuon = duration;
            knockback = -(other.position - transform.position).normalized * power;
        }

        private void HandleAttackDelay()
        {
            if (WeaponHandler == null)
                return;

            if (timeSinceLastAttack <= WeaponHandler.Delay)
            {
                timeSinceLastAttack += Time.deltaTime;
            }

            if (isAttacking && timeSinceLastAttack > WeaponHandler.Delay)
            {
                timeSinceLastAttack = 0;
                Attack();
            }
        }

        protected virtual void Attack()
        {
            if (lookDirection != Vector2.zero)
                WeaponHandler?.Attack();
        }

        public virtual void Death()
        {
            _rigidbody.velocity = Vector3.zero;

            foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>())
            {
                Color color = renderer.color;
                color.a = 0.3f;
                renderer.color = color;
            }

            foreach (Behaviour componet in transform.GetComponentsInChildren<Behaviour>())
            {
                componet.enabled = false;
            }

            Destroy(gameObject, 2f);

        }
    }
}