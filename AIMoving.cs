using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMoving : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;
    public Transform startPos;
    public Transform endPos;
    private Transform desPos;

    private Transform TargetPos;

    public void Start()
    {
        transform.position = startPos.position;
        desPos = endPos;
    }

    IEnumerator Enemyturn()
    {
        float time = 0.0f;

        while (time < 2.0f)
        {
            time += Time.deltaTime;

            Vector3 dir = TargetPos.position - transform.position;
            dir.Normalize();
            Quaternion TargetRotation = Quaternion.LookRotation(dir); //위에서 구한 목표 방향(Vector3)을 사분위수로 전환하는 메서드
                                                                      //회전값 적용
            
            Quaternion rotateAmount = Quaternion.RotateTowards(transform.rotation,
                TargetRotation, rotateSpeed * Time.deltaTime); //(시작값, 목표값, 회전 속도)를 인자로 받아 회전 값을 연산해주는 메서드 
            transform.rotation = rotateAmount;
        }
        yield return null;
    }
    // 코루틴 https://wergia.tistory.com/226

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        
        transform.position = Vector3.MoveTowards(transform.position,
            desPos.position, Time.deltaTime * moveSpeed);

        if (Vector3.Distance(transform.position, desPos.position) <= 0.05f)
        {
            if (desPos == endPos)
            {
                TargetPos = startPos;
                desPos = startPos;
            }
            else
            {
                TargetPos = endPos;
                desPos = endPos;
            }
            StartCoroutine(Enemyturn()); // 회전 속도
        }
    }
}

