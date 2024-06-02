using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanWindEffect : MonoBehaviour
{
    public float windForce = 12f; // 바람 힘
    public float minHeight = 9f; // 최소 높이
    public float maxHeight = 11f; // 최대 높이

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // 현재 캐릭터 높이
            float currentY = other.transform.position.y - transform.position.y;
            
            // 캐릭터가 최소 높이보다 낮을 때
            if (currentY < minHeight)
            {
                // 캐릭터를 위로 밀어 올리는 힘을 가함
                rb.AddForce(Vector3.up * windForce, ForceMode.Acceleration);
            }
            // 캐릭터가 최대 높이를 넘어섰을 때
            else if (currentY > maxHeight)
            {
                // 캐릭터의 상승 속도를 쭐임
                Vector3 velocity = rb.velocity;
                if (velocity.y > 0.2)
                {
                    velocity.y = 0;
                    rb.velocity = velocity;
                }
            }
            // 캐릭터가 최소 높이와 최대 높이 사이에 있을 때
            else
            {
                // 일정한 바람의 힘을 유지
                rb.AddForce(Vector3.up * windForce, ForceMode.Acceleration);
            }
        }
    }
}
