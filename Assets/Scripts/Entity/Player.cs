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
        // �̵� �Է��� �޴´�
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // �̵� ���� ���
        Vector2 moveVector = new Vector2(moveX, moveY);

        // ĳ���� �̵�
        transform.Translate(moveVector.normalized * Time.deltaTime * 5f);

        // �̵� ���� �� �ִϸ��̼� ����
        if (moveX != 0 || moveY != 0)
        {
            // �̵� ���·� �ִϸ��̼� ��ȯ (�̵� �ִϸ��̼� ���� ��ȣ�� ���÷� 0, �ε����� �߰�)
            playerRender._anim.SetBool("1_Move", true);  // "isMove"��� bool �Ķ���Ͱ� Animator�� �����Ѵٰ� ����
            if (moveX > 0)  // ���������� �̵�
            {
                transform.localScale = new Vector2(-1f, 1f);  // ���������� ���ϰ� ����
            }
            else if (moveX < 0)  // �������� �̵�
            {
                transform.localScale = new Vector2(1f, 1f);  // �������� ���ϰ� ����
            }
        }
        else
        {

            playerRender._anim.SetBool("1_Move", false);  // "isMove"�� false�� �����Ͽ� �̵����� ���� ���� �ִϸ��̼� ����
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
