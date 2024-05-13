using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpForce = 10f; // 점프대의 힘
    private AudioSource audioSource;

    private void OnCollisionEnter(Collision collision)
    {
        // 플레이어가 점프대와 충돌했는지 확인
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                // 플레이어를 위로 튕겨내는 힘 적용
                playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        audioSource.Play();
    }
}
