using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBoom : MonoBehaviour
{
    public ParticleSystem rockExplosionPrefab; // RockBoom 파티클 프리팹
    public GameObject rock; // 파괴할 Block 오브젝트
    private ParticleSystem rockExplosionInstance; // 파티클 시스템 인스턴스

    // 폭발을 시작하는 메서드
    public void BoomStart()
    {
        StartCoroutine(RockBoomSequence());

    }
    private IEnumerator RockBoomSequence()
    {
        FollowCamera follow = Camera.main.GetComponent<FollowCamera>();
        if (follow != null)
        {
            follow.enabled = false;
            GameManager.Instance.player.enabled = false;
        }
        Vector3 originalPosition = Camera.main.transform.position;
        Vector3 targetPosition = new Vector3(1.6f, 6.5f, originalPosition.z);

        float moveDuration = 0.5f;
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            Camera.main.transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Camera.main.transform.position = targetPosition;

        yield return new WaitForSeconds(1f);

        rockExplosionInstance = Instantiate(rockExplosionPrefab, transform.position, Quaternion.identity);
        rockExplosionInstance.Play();
        ParticleSystem.MainModule mainModule = rockExplosionInstance.main;
        float particleDuration = mainModule.duration + mainModule.startLifetime.constantMax;

        Destroy(rockExplosionInstance.gameObject, particleDuration);
        rock.SetActive(false);
        yield return new WaitForSeconds(particleDuration);
        if (follow != null)
        {
            follow.enabled = true;
            Camera.main.transform.position = originalPosition;
            GameManager.Instance.player.enabled = true;
        }

        Destroy(gameObject, 2f);
    }

}
