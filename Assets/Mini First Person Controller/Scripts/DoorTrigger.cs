using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Door connectedDoor; // 연결된 문 오브젝트
    public float speed = 2f; // 문이 움직이는 속도
    
    private bool isColliding = false; 
    private Vector3 initialPosition;

    void Start()
{
    initialPosition = transform.position; // 시작 시 초기 위치를 현재 위치로 설정
}
    private void OnTriggerEnter(Collider other)
    {
        isColliding = true; // 충돌 상태를 true로 설정
      
        connectedDoor.OpenDoor(speed);
    }

    private void OnTriggerExit(Collider other)
    {
        isColliding = false; // 충돌 상태를 false로 설정
      
        connectedDoor.CloseDoor(speed);
    }
     
}

