using UnityEngine;
using System.Collections;

public class TeleportPad : MonoBehaviour
{
    public Transform teleportTarget; // 플레이어를 이동시킬 위치
    private AudioSource audioSource; // AudioSource 컴포넌트에 대한 참조

    private void Start()
    {
        // AudioSource 컴포넌트를 가져옵니다.
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 소리 재생
            audioSource.Play();

            StartCoroutine(FreezePlayerPosition(other));
        }
    }

    IEnumerator FreezePlayerPosition(Collider player)
    {
        // 플레이어의 Rigidbody 컴포넌트를 가져옵니다.
        Rigidbody playerRigidbody = player.GetComponent<Rigidbody>();

        // 플레이어의 현재 위치를 저장합니다.
        Vector3 currentPosition = teleportTarget.position;

        // 플레이어의 위치를 teleportTarget의 위치로 이동시킵니다.
        player.transform.position = currentPosition;

        // 만약 Rigidbody 컴포넌트가 있다면, 움직임을 임시로 정지시킵니다.
        if (playerRigidbody != null)
        {
            playerRigidbody.isKinematic = true;
        }

        // 1초간 대기합니다.
        yield return new WaitForSeconds(1f);

        // 움직임을 다시 활성화합니다.
        if (playerRigidbody != null)
        {
            playerRigidbody.isKinematic = false;
        }
    }
}
