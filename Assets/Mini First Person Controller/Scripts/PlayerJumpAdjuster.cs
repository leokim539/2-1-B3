using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerJumpAdjuster : MonoBehaviour
{
    private Rigidbody rb;
    public float jumpForce = 5f;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("MovingPlatform"))
        {
            isGrounded = true;
            transform.parent = other.transform; // 플레이어를 발판의 자식으로 만들어 발판과 함께 움직이게 함
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("MovingPlatform"))
        {
            isGrounded = false;
            transform.parent = null; // 플레이어와 발판의 부모-자식 관계를 해제
        }
    }
}
