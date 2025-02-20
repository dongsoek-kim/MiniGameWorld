using UnityEngine;

public class IceFloor : MonoBehaviour
{
    // ����Ÿ���� �̲������� ���� (�밢�� ����)
    public Vector2 slideDirection = new Vector2(1, 1);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �÷��̾� �±װ� "Player"�� ������Ʈ�� ���� Ÿ���� ������
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player stepped on ice floor!");
            // �÷��̾��� �����̵� ��ũ��Ʈ�� ������ �����̵� ������ ��û�մϴ�.
            PlayerSlideOnIce slideScript = collision.GetComponent<PlayerSlideOnIce>();
            if (slideScript != null)
            {
                slideScript.StartSliding(slideDirection);
            }
        }
    }
}