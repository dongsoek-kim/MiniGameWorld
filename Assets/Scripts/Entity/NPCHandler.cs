using UnityEngine;

public class NPCHandler : MonoBehaviour
{
    public float interactionRadius = 1.5f; // NPC�� ��ȣ�ۿ��� �� �ִ� �ݰ�
    public string npcTag = "NPC"; // NPC�� �±� (��: "NPC"�� ����)
    private GameObject player;

    void Start()
    {
        // �÷��̾� ��ü�� ã�Ƽ� ����
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player ��ü�� ã�� �� �����ϴ�. Player �±׸� Ȯ�����ּ���.");
        }
    }

    void Update()
    {
        // �÷��̾�� NPC ������ �Ÿ��� ���
        float distance = Vector3.Distance(player.transform.position, transform.position);

        // �ݰ� ���� �÷��̾ �ְ� �����̽��ٸ� ������ ��ȣ�ۿ�
        if (distance <= interactionRadius && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(this.name);
            Debug.Log("����!");
        }
    }
}
