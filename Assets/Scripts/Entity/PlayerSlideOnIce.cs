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

    // IceFloor에서 호출: 얼음바닥을 밟으면 슬라이딩 시작
    public void StartSliding(Vector2 direction)
    {
        // 플레이어의 기존 컨트롤을 비활성화 (예: PlayerController 스크립트)
        GameManager.Instance.player.enabled = false;
        Debug.Log("Player 조종불가!");
        isSliding = true;
        slideDirection = direction.normalized;

        // 필요에 따라, 플레이어의 물리 재질이나 리지드바디 드래그를 조정하여 미끄러짐 효과를 극대화하세요.
    }

    void Update()
    {
        if (isOnIceFloor && Input.GetKeyDown(KeyCode.X))
        {
            StopSliding();
            // 시작 위치로 이동 (z값은 현재 값 유지)
            transform.position = new Vector3(1f, 5.5f, transform.position.z);
            GameManager.Instance.player.enabled = true;
            isOnIceFloor = false;
            return;
        }
        // 슬라이딩 중이면 지속적으로 자동 이동
        if (isSliding)
        {
            rb.velocity = slideDirection * slideSpeed;
        }
        else if (isOnIceFloor)
        {
            // 얼음 위에서 입력이 들어오면 슬라이딩 재개
            Vector2 inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (inputDir.sqrMagnitude > 0)
            {
                StartSliding(inputDir);
            }
            else
            {
                // 입력이 없으면 미끄러지지 않고 멈춘 상태 유지
                rb.velocity = Vector2.zero;
            }
        }
    }

    // 플레이어가 얼음바닥이 아닌 다른 오브젝트와 충돌하면 슬라이딩 중지
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 예를 들어, 얼음바닥(또는 IceFloor)는 태그 "IceFloor"로 관리하고,
        // 그 외 충돌체와의 충돌이면 슬라이딩을 멈춥니다.
        if (isSliding && collision.gameObject.CompareTag("Block"))
        {
            Debug.Log("Player stopped sliding!");
            StopSliding();
        }

        else if (isSliding && collision.gameObject.CompareTag("EndSpot"))
        {
            Debug.Log("EndSpot이랑 부딫!");
            StopSliding();
            isOnIceFloor = false;
        }
    }

    // 슬라이딩 정지 및 플레이어 컨트롤 복구
    public void StopSliding()
    {
        if (isSliding)
        {
            isSliding = false;
            rb.velocity = Vector2.zero; 
            rb.WakeUp();
            // 플레이어 컨트롤 재활성화
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
            // 얼음바닥을 벗어나면 일반 컨트롤 복구
            GameManager.Instance.player.enabled = true;
        }
    }
}

