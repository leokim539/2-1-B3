using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour
{
    Rigidbody rigidbody;
    public float jumpStrength = 2;
    public event System.Action Jumped;
    public int jumpPossible; 
    int jumpCount;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        jumpCount = jumpPossible;
    }

    void Update()
    {
        Jump1();
    }
    void Jump1()
    {
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            rigidbody.AddForce(Vector3.up * 100 * jumpStrength);
            Jumped?.Invoke();
            jumpCount--;
            

        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "item")
        {
            jumpCount++;
            Destroy(other.gameObject);
        }
        if (jumpCount == 0)
        {
            jumpCount++;
        }
    }
}

