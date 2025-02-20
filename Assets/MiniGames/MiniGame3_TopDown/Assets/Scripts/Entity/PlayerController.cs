using UnityEngine;
using UnityEngine.InputSystem;

namespace TopDown
{
    public class PlayerController : BaseController
    {
        private GameManager gameManager;
        private Camera camera;

        public void Init(GameManager gameManager)
        {
            this.gameManager = gameManager;
            camera = Camera.main;
        }

        public override void Death()
        {
            base.Death();
            {
                base.Death();
                gameManager.GameOver();
            }
        }
        void OnMove(InputValue inputValue)
        {

            movementDirection = inputValue.Get<Vector2>();
            movementDirection = movementDirection.normalized;
        }
        void OnLook(InputValue inputValue)
        {
            Vector2 mousePosition = inputValue.Get<Vector2>();
            Vector2 worldPos = camera.ScreenToWorldPoint(mousePosition);
            lookDirection = (worldPos - (Vector2)transform.position);

            if (lookDirection.magnitude < .9f)
            {
                lookDirection = Vector2.zero;
            }
            else
            {
                lookDirection = lookDirection.normalized;
            }
        }

        void OnFire(InputValue inputValue)
        {
            isAttacking = inputValue.isPressed;
        }
    }
}