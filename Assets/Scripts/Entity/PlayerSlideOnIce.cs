using UnityEngine;

public class PlayerSlideOnIce : MonoBehaviour
{
    public float slideSpeed = 5f;
    private bool isSliding = false;
    private Vector2 slideDirection;
    private Rigidbody2D rb;
    private bool isOnIceFloor = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // IceFloor���� ȣ��: �����ٴ��� ������ �����̵� ����
    public void StartSliding(Vector2 direction)
    {
        // �÷��̾��� ���� ��Ʈ���� ��Ȱ��ȭ (��: PlayerController ��ũ��Ʈ)
        GameManager.Instance.player.enabled = false;
        Debug.Log("Player �����Ұ�!");
        isSliding = true;
        slideDirection = direction.normalized;

        // �ʿ信 ����, �÷��̾��� ���� �����̳� ������ٵ� �巡�׸� �����Ͽ� �̲����� ȿ���� �ش�ȭ�ϼ���.
    }

    void Update()
    {
        if (isOnIceFloor && Input.GetKeyDown(KeyCode.X))
        {
            StopSliding();
            // ���� ��ġ�� �̵� (z���� ���� �� ����)
            transform.position = new Vector3(1f, 5.5f, transform.position.z);
            GameManager.Instance.player.enabled = true;
            isOnIceFloor = false;
            return;
        }
        // �����̵� ���̸� ���������� �ڵ� �̵�
        if (isSliding)
        {
            rb.velocity = slideDirection * slideSpeed;
        }
        else if (isOnIceFloor)
        {
            // ���� ������ �Է��� ������ �����̵� �簳
            Vector2 inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (inputDir.sqrMagnitude > 0)
            {
                StartSliding(inputDir);
            }
            else
            {
                // �Է��� ������ �̲������� �ʰ� ���� ���� ����
                rb.velocity = Vector2.zero;
            }
        }
    }

    // �÷��̾ �����ٴ��� �ƴ� �ٸ� ������Ʈ�� �浹�ϸ� �����̵� ����
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ���� ���, �����ٴ�(�Ǵ� IceFloor)�� �±� "IceFloor"�� �����ϰ�,
        // �� �� �浹ü���� �浹�̸� �����̵��� ����ϴ�.
        if (isSliding && collision.gameObject.CompareTag("Block"))
        {
            Debug.Log("Player stopped sliding!");
            StopSliding();
        }

        else if (isSliding && collision.gameObject.CompareTag("EndSpot"))
        {
            Debug.Log("EndSpot�̶� �΋H!");
            StopSliding();
            isOnIceFloor = false;
        }
    }

    // �����̵� ���� �� �÷��̾� ��Ʈ�� ����
    public void StopSliding()
    {
        if (isSliding)
        {
            isSliding = false;
            rb.velocity = Vector2.zero; 
            rb.WakeUp();
            // �÷��̾� ��Ʈ�� ��Ȱ��ȭ
            GameManager.Instance.player.enabled = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("IceTile"))
        {
            Debug.Log("Player IN Ice!");
            isOnIceFloor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("IceTile"))
        {
            Debug.Log("Player out Ice!");
            StopSliding();
            isOnIceFloor = false;
            // �����ٴ��� ����� �Ϲ� ��Ʈ�� ����
            GameManager.Instance.player.enabled = true;
        }
    }
}

