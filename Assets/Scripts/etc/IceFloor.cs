using UnityEngine;

public class IceFloor : MonoBehaviour
{
    // 얼음타일이 미끄러지는 방향 (대각선 방향)
    public Vector2 slideDirection = new Vector2(1, 1);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어 태그가 "Player"인 오브젝트가 얼음 타일을 밟으면
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player stepped on ice floor!");
            // 플레이어의 슬라이드 스크립트를 가져와 슬라이딩 시작을 요청합니다.
            PlayerSlideOnIce slideScript = collision.GetComponent<PlayerSlideOnIce>();
            if (slideScript != null)
            {
                slideScript.StartSliding(slideDirection);
            }
        }
    }
}