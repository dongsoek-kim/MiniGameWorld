using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FlappyPlane
{
    public class Player : MonoBehaviour
    {
        Animator animator;
        Rigidbody2D _rigidbody;

        public float flapFroce = 6f;
        public float forwardSpeed = 3f;
        public bool isDead = false;
        float deathCooldown = 1f;
        float clearCooldown = 1f;
        bool isFlap = false;
        bool isClear = false;
        public bool godMode = false;


        GameManager gameManager;
        // Start is called before the first frame update
        void Start()
        {
            gameManager = GameManager.Instance;
            animator = GetComponentInChildren<Animator>();
            _rigidbody = GetComponent<Rigidbody2D>();
            if (animator == null)
                Debug.LogError("Not Founded Animator");

            if (_rigidbody == null)
                Debug.LogError("Not Founded Rigidbody");

            gameManager.GameClear += OnGameClear;
        }

        // Update is called once per frame
        void Update()
        {
            if (isDead)
            {
                if (deathCooldown <= 0)
                {
                    //재시작
                    if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                    {
                        SceneManager.LoadScene("MainScene");
                    }
                }
                else
                {
                    deathCooldown -= Time.deltaTime;
                }

            }
            else if (isClear)
            {
                if (clearCooldown <= 0)
                {
                    if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                    {
                        SceneManager.LoadScene("MainScene");
                    }
                }
                else
                {
                    clearCooldown -= Time.deltaTime;
                } 
            }        
            else
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    isFlap = true;
                }
            }
        }
        private void FixedUpdate()
        {
            if (isDead)
                return;

            Vector3 velocity = _rigidbody.velocity;
            velocity.x = forwardSpeed+GameManager.difficulty;//난이도에따른 속도증가
            if (isClear)
            {
                Collider2D collider = GetComponent<Collider2D>();
                Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
                if (collider != null)
                {
                    collider.enabled = false;  // Collider2D 비활성화
                }
                if (rigidbody != null)
                {
                    rigidbody.isKinematic = true;  // Rigidbody2D를 물리 엔진의 영향을 받지 않게 설정
                }

                return;
            }
            if (isFlap)
            {
                velocity.y += flapFroce;
                isFlap = false;
            }

            _rigidbody.velocity = velocity;
            float angle = Mathf.Clamp((_rigidbody.velocity.y * 10f), -90, 90);
            float lerpAngle = Mathf.Lerp(_rigidbody.velocity.y, angle, Time.deltaTime * 5f);
            transform.rotation = Quaternion.Euler(0, 0, lerpAngle);
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (godMode)
                return;

            if (isDead)
                return;
            isDead = true;
            animator.SetInteger("IsDie", 1);
            gameManager.GameOver();
        }

        public void OnGameClear()
        {
            isClear = true;
            godMode = true;
        }
    }
}
