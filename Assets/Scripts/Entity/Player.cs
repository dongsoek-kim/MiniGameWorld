using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public SPUM_Prefabs playerRender;
    private Animator animator;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
       
    }
 
    // Update is called once per frame
    void Update()
    {
        // 이동 입력을 받는다
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // 이동 벡터 계산
        Vector2 moveVector = new Vector2(moveX, moveY);

        // 캐릭터 이동
        transform.Translate(moveVector.normalized * Time.deltaTime * 5f);

        // 이동 중일 때 애니메이션 실행
        if (moveX != 0 || moveY != 0)
        {
            // 이동 상태로 애니메이션 전환 (이동 애니메이션 상태 번호를 예시로 0, 인덱스를 추가)
            playerRender._anim.SetBool("1_Move", true);  // "isMove"라는 bool 파라미터가 Animator에 존재한다고 가정
            if (moveX > 0)  // 오른쪽으로 이동
            {
                transform.localScale = new Vector2(-1f, 1f);  // 오른쪽으로 향하게 설정
            }
            else if (moveX < 0)  // 왼쪽으로 이동
            {
                transform.localScale = new Vector2(1f, 1f);  // 왼쪽으로 향하게 설정
            }
        }
        else
        {

            playerRender._anim.SetBool("1_Move", false);  // "isMove"를 false로 설정하여 이동하지 않을 때의 애니메이션 실행
        }
    }
    public void SetPlayerTransform()
    {
        transform.position = new Vector3(GameManager.Instance.storeTransform.position.x,GameManager.Instance.storeTransform.position.y);
    }
    public void StorePlayerTransform()
    {
        GameManager.Instance.storeTransform.position= transform.position;
    }
}
