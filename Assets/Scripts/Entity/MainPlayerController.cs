using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerController : MonoBehaviour
{
    public SPUM_Prefabs player;

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
        Vector3 moveVector = new Vector3(moveX, moveY, 0f);

        // ĳ���� �̵�
        transform.Translate(moveVector.normalized * Time.deltaTime * 5f);

        // �̵� ���� �� �ִϸ��̼� ����
        if (moveX != 0 || moveY != 0)
        {
            // �̵� ���·� �ִϸ��̼� ��ȯ (�̵� �ִϸ��̼� ���� ��ȣ�� ���÷� 0, �ε����� �߰�)
            player._anim.SetBool("1_Move", true);  // "isMove"��� bool �Ķ���Ͱ� Animator�� �����Ѵٰ� ����
            if (moveX > 0)  // ���������� �̵�
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);  // ���������� ���ϰ� ����
            }
            else if (moveX < 0)  // �������� �̵�
            {
                transform.localScale = new Vector3(1f, 1f, 1f);  // �������� ���ϰ� ����
            }
        }
        else
        {
            
            player._anim.SetBool("1_Move", false);  // "isMove"�� false�� �����Ͽ� �̵����� ���� ���� �ִϸ��̼� ����
        }
    }
}
