using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 moveAmount; // 움직일 거리와 방향
    public float moveSpeed; // 움직임 속도

    private Vector3 startPosition;
    private Vector3 endPosition;
    private bool movingToEnd = true;

    void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition + moveAmount;
    }

    void Update()
    {
        if(movingToEnd)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, moveSpeed * Time.deltaTime);
            if(transform.position == endPosition)
            {
                movingToEnd = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);
            if(transform.position == startPosition)
            {
                movingToEnd = true;
            }
        }
    }
}
