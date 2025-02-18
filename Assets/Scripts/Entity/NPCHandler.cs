using UnityEngine;

public class NPCHandler : MonoBehaviour
{
    public float interactionRadius = 1.5f; // NPC와 상호작용할 수 있는 반경
    public string npcTag = "NPC"; // NPC의 태그 (예: "NPC"로 설정)
    private GameObject player;

    void Start()
    {
        // 플레이어 객체를 찾아서 저장
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player 객체를 찾을 수 없습니다. Player 태그를 확인해주세요.");
        }
    }

    void Update()
    {
        // 플레이어와 NPC 사이의 거리를 계산
        float distance = Vector3.Distance(player.transform.position, transform.position);

        // 반경 내에 플레이어가 있고 스페이스바를 누르면 상호작용
        if (distance <= interactionRadius && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(this.name);
            Debug.Log("반응!");
        }
    }
}
