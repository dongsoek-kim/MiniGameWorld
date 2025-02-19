using UnityEngine;

public class NPCHandler : MonoBehaviour
{
    public float interactionRadius = 1.5f; // NPC와 상호작용할 수 있는 반경
    public string npcTag = "NPC"; // NPC의 태그 (예: "NPC"로 설정)
    private GameObject player;

    // 모든 자식 NPC를 관리하는 리스트
    private GameObject[] npcObjects;

    void Start()
    {
        // 플레이어 객체를 찾음
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player 객체를 찾을 수 없습니다. Player 태그를 확인해주세요.");
        }

        // 부모 오브젝트 안의 모든 자식 NPC를 찾아서 관리
        npcObjects = GameObject.FindGameObjectsWithTag(npcTag);
    }

    void Update()
    {
        // 모든 NPC에 대해 상호작용을 체크
        foreach (GameObject npc in npcObjects)
        {
            float distance = Vector3.Distance(player.transform.position, npc.transform.position);
            if (distance <= interactionRadius && (Input.GetKeyDown(KeyCode.Space)))
            {
                Debug.Log(npc.name);  // 해당 NPC 이름 출력
                Debug.Log("반응!");

                // NPC에 맞는 동작을 수행
                NPCManager.Instance.OnInteract(npc.name);
            }
        }
    }
}
