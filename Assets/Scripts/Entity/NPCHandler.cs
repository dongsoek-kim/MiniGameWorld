using UnityEngine;

public class NPCHandler : MonoBehaviour
{
    public float interactionRadius = 1.5f; // NPC�� ��ȣ�ۿ��� �� �ִ� �ݰ�
    public string npcTag = "NPC"; // NPC�� �±� (��: "NPC"�� ����)
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player ��ü�� ã�� �� �����ϴ�. Player �±׸� Ȯ�����ּ���.");
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= interactionRadius && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(this.name);
            Debug.Log("����!");
            NPCManager.Instance.OnIntercate();
        }
    }
}
