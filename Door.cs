using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public AudioSource OpenDoorSource;
    public AudioSource CloseDoorSource;

    public Vector3 initialPosition;
    public Vector3 targetPosition;
    private bool isColliding = false; 

    void Start()
    {
        OpenDoorSource = GetComponent<AudioSource>();
        CloseDoorSource = GetComponent<AudioSource>();
    }

    // 문을 여는  메서드
    public void OpenDoor(float speed)
    {
        isColliding = true;
        // Coroutine을 시작하여 문을 지정 위치로 이동시킨다
        StartCoroutine(MoveDoor(targetPosition, speed));
    }

    // 문을 닫기 위한 메서드
    public void CloseDoor(float speed)
    {
        isColliding = false;
        // Coroutine을 시작하여 문을 초기 위치로 이동시킨ㄷ
        StartCoroutine(CloseDoorCoroutine(initialPosition, speed));
    }

    private IEnumerator MoveDoor(Vector3 newPosition, float speed)
    {
        //yield return new WaitForSeconds(0.2f);
        while (transform.position != newPosition)
        {
            if (!isColliding) // 만약 isCollding 이 false이라면 코루틴 정지 
        {
            OpenDoorSource.Stop();
            yield break;
        }
            transform.position = Vector3.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
            OpenDoorSource.Play();
            yield return null;
        }
    }

    private IEnumerator CloseDoorCoroutine(Vector3 initialPosition, float speed)
    {
        while (transform.position != initialPosition)
        {
            if (isColliding) // 만약 isCollding 이 true이라면 코루틴 정지 
        {
            CloseDoorSource.Stop();
            yield break;
        }
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, speed * Time.deltaTime);
            CloseDoorSource.Play();
            yield return null;
        }
    }
}
