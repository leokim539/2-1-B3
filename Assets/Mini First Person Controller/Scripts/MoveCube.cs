using UnityEngine;

public class MoveCube : MonoBehaviour
{
    public Vector3 distance = new Vector3(5.0f, 0, 0); // 이동할 거리 (x, y, z 축)
    public float speed = 2.0f; // 이동 속도

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool movingToTarget = true;

    void Start()
    {
        startPosition = transform.position; // 시작 위치 저장
        targetPosition = startPosition + distance; // 목표 위치 계산
    }

    void Update()
    {
        // 현재 위치와 목표 위치 사이를 왔다갔다 하도록 설정
        if (movingToTarget)
        {
            // 목표 위치로 이동
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            
            // 목표 위치에 도달하면 방향 전환
            if (transform.position == targetPosition)
                movingToTarget = false;
        }
        else
        {
            // 시작 위치로 이동
            transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);
            
            // 시작 위치에 도달하면 방향 전환
            if (transform.position == startPosition)
                movingToTarget = true;
        }
    }
}
