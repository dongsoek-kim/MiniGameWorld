using UnityEngine;

public class NPCHandler : MonoBehaviour
{
    public float interactionRadius = 1.5f; // NPC와 상호작용할 수 있는 반경
    public string npcTag = "NPC"; // NPC의 태그 (예: "NPC"로 설정)
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player 객체를 찾을 수 없습니다. Player 태그를 확인해주세요.");
        }
    }

    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= interactionRadius && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(this.name);
            Debug.Log("반응!");
            NPCManager.Instance.OnIntercate();
        }
    }
}
