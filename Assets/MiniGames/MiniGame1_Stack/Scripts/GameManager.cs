using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using FlappyPlane;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

namespace Stack
{
    public class GameManager : MonoBehaviour
    {
        public static int difficulty;
        private bool isClear;
        public Action GameClear;
        float deathCooldown = 1f;
        float clearCooldown = 1f;

        UIManager uiManager;
        public UIManager UIManager { get { return uiManager; } }
        public static void SetGameDifficulty(int newDifficulty)
        {
            difficulty = newDifficulty;
        }
        private const float BoundSize = 7f;
        private const float MovingBoundsSize = 3f;
        private const float StackMovingSpeed = 5.0f;
        private const float BlockMovingSpeed = 3.5f;
        private const float ErroMargin = 0.3f;
        public GameObject originBlock = null;

        private Vector2 prevBlockPosition;
        private Vector2 desiredPosition;
        private Vector2 stackBounds = new Vector2(BoundSize, BoundSize);

        Transform lastBlock = null;
        float blockTransition = 0f;

        int stackCount = -1;
        public Color prevColor;
        public Color nextColor;
        private bool isGameOver;


        void Awake()
        {
            GameClear += ONGameClear;
            uiManager = FindObjectOfType<UIManager>();
        }
        void Start()
        {
            if (originBlock == null)
            {
                Debug.Log("OriginBlock is NULL");
                return;
            }

            prevColor = GetRandomColor();
            nextColor = GetRandomColor();

            prevBlockPosition = Vector2.down;
            Spawn_Block();
            Spawn_Block();
        }

        private void Update()
        {
            if (isGameOver)
                return;

            if (Input.GetMouseButtonDown(0))
            {

                if (PlaceBlock())
                {
                    Spawn_Block();
                    uiManager.UpdateScore(stackCount - 1);
                    IsClear();
                    if (isClear)
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
                }
                else
                {
                    // 게임 오버
                    uiManager.SetGameOver();
                    GameOverEffect();
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
            }

            MoveBlock();
            transform.position = Vector2.Lerp(transform.position, desiredPosition, StackMovingSpeed * Time.deltaTime);
        }
        bool Spawn_Block()
        {
            // 이전블럭 저장
            if (lastBlock != null)
                prevBlockPosition = lastBlock.localPosition;

            GameObject newBlock = null;
            Transform newTrans = null;

            newBlock = Instantiate(originBlock);

            if (newBlock == null)
            {
                Debug.Log("NewBlock Instantiate Failed!");
                return false;
            }

            ColorChange(newBlock);

            newTrans = newBlock.transform;
            newTrans.parent = this.transform;
            newTrans.localPosition = prevBlockPosition + Vector2.up;
            newTrans.localRotation = Quaternion.identity;
            newTrans.localScale = new Vector2(stackBounds.x,1);

            stackCount++;

            desiredPosition = Vector2.down * stackCount;
            blockTransition = 0f;

            lastBlock = newTrans;
            return true;
        }
        Color GetRandomColor()
        {
            float r = UnityEngine.Random.Range(100f, 250f) / 255f;
            float g = UnityEngine.Random.Range(100f, 250f) / 255f;
            float b = UnityEngine.Random.Range(100f, 250f) / 255f;

            return new Color(r, g, b);
        }
        void ColorChange(GameObject go)
        {
            Color applyColor = Color.Lerp(prevColor, nextColor, (stackCount % 11) / 10f);

            Renderer rn = go.GetComponent<Renderer>();

            if (rn == null)
            {
                Debug.Log("Renderer is NULL!");
                return;
            }

            rn.material.color = applyColor;
            Camera.main.backgroundColor = applyColor - new Color(0.3f, 0.3f, 0.3f);

            if (applyColor.Equals(nextColor) == true)
            {
                prevColor = nextColor;
                nextColor = GetRandomColor();
            }
        }

        void MoveBlock()
        {
            blockTransition += (Time.deltaTime * BlockMovingSpeed)+difficulty;

            float movePosition = Mathf.PingPong(blockTransition, BoundSize) - BoundSize/2;

            lastBlock.localPosition = new Vector2(movePosition * MovingBoundsSize, stackCount);
      
        }

        bool PlaceBlock()
        {
            Vector2 lastPosition = lastBlock.transform.localPosition;
            float deltaX = prevBlockPosition.x - lastPosition.x;
            deltaX = Mathf.Abs(deltaX);
            if (deltaX > ErroMargin)
            {
                bool isNegativeNum = (prevBlockPosition.x < lastPosition.x);
                stackBounds.x -= deltaX;
                if (stackBounds.x <= 0)
                {
                    return false;
                }

                float middle = (prevBlockPosition.x + lastPosition.x) / 2;
                lastBlock.localScale = new Vector2(stackBounds.x, 1);

                Vector2 tempPosition = lastBlock.localPosition;
                tempPosition.x = middle;
                lastBlock.localPosition = lastPosition = tempPosition;

                float rubbleHalfScale = deltaX / 2;
                CreateRubble(
                    new Vector2(isNegativeNum
                            ? lastPosition.x + stackBounds.x / 2 + rubbleHalfScale
                            : lastPosition.x - stackBounds.x / 2 - rubbleHalfScale
                        , lastPosition.y),
                    new Vector2(deltaX, 1));                
            }
            else 
            {
                lastBlock.localPosition = prevBlockPosition + Vector2.up;               
            }
            return true;

        }
        void CreateRubble(Vector2 pos, Vector2 scale)
        {
            GameObject go = Instantiate(lastBlock.gameObject);
            go.transform.parent = this.transform;

            go.transform.localPosition = pos;
            go.transform.localScale = scale;
            go.transform.localRotation = Quaternion.identity;

            go.AddComponent<Rigidbody2D>();
            go.name = "Rubble";
        }
        void GameOverEffect()
        {
            int childCount = this.transform.childCount;

            for (int i = 1; i < 20; i++)
            {
                if (childCount < i)
                    break;

                GameObject go =
                    this.transform.GetChild(childCount - i).gameObject;

                if (go.name.Equals("Rubble"))
                    continue;

                Rigidbody rigid = go.AddComponent<Rigidbody>();

                rigid.AddForce(
                    (Vector2.up * UnityEngine.Random.Range(0, 10f)
                     + Vector2.right * (UnityEngine.Random.Range(0, 10f) - 5f))
                    * 100f
                );
            }
        }
        public void ONGameClear()
        {
            uiManager.SetGameClear();
        }
        public void IsClear()
        {

            if (!isClear)
            {
                switch (difficulty)

                {
                    case 0://easy
                        if (stackCount-1 >= 3)
                        {
                            Debug.Log("클리어");
                            isClear = true;
                            GameClear?.Invoke();
                            MiniGameManager.Instance.StageClear?.Invoke(2, 1);//결합도 문제
                        }
                        break;
                    case 1://normal
                        if (stackCount-1 >= 3)
                        {
                            Debug.Log("클리어");
                            isClear = true;
                            GameClear?.Invoke();
                            MiniGameManager.Instance.StageClear?.Invoke(2, 2);//결합도 문제
                        }
                        break;
                    case 2://hard

                        if (stackCount-1 >= 3)
                        {
                            Debug.Log("클리어");
                            isClear = true;
                            GameClear?.Invoke();
                            MiniGameManager.Instance.StageClear?.Invoke(2, 3);//결합도 문제
                        }
                        break;
                    case 3://challenge

                        if (stackCount-1 >= 3)
                        {
                            Debug.Log("클리어");
                            isClear = true;
                            GameClear?.Invoke();
                            MiniGameManager.Instance.StageClear?.Invoke(2, 4);//결합도 문제
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}