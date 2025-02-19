using UnityEngine;

public class NPCHandler : MonoBehaviour
{
    public float interactionRadius = 1.5f; // NPC�� ��ȣ�ۿ��� �� �ִ� �ݰ�
    public string npcTag = "NPC"; // NPC�� �±� (��: "NPC"�� ����)
    private GameObject player;

    // ��� �ڽ� NPC�� �����ϴ� ����Ʈ
    private GameObject[] npcObjects;

    void Start()
    {
        // �÷��̾� ��ü�� ã��
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player ��ü�� ã�� �� �����ϴ�. Player �±׸� Ȯ�����ּ���.");
        }

        // �θ� ������Ʈ ���� ��� �ڽ� NPC�� ã�Ƽ� ����
        npcObjects = GameObject.FindGameObjectsWithTag(npcTag);
    }

    void Update()
    {
        // ��� NPC�� ���� ��ȣ�ۿ��� üũ
        foreach (GameObject npc in npcObjects)
        {
            float distance = Vector3.Distance(player.transform.position, npc.transform.position);
            if (distance <= interactionRadius && (Input.GetKeyDown(KeyCode.Space)))
            {
                Debug.Log(npc.name);  // �ش� NPC �̸� ���
                Debug.Log("����!");

                // NPC�� �´� ������ ����
                NPCManager.Instance.OnInteract(npc.name);
            }
        }
    }
}
