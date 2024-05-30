using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swinging: MonoBehaviour
{
    RaycastHit hit;
    LineRenderer lr;
    SpringJoint sj;

    float dis;
    Vector3 spot;
    bool isSwing;

    [Header("Raycast")]
    public Transform cam;
    public float rayDistance;

    [Header("SpringJoint")]
    public float springForce;
    public float springDamper;
    public float springMass;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        HookPoint();        // 로프를 연결할 지점을 설정하는 함수

        if (Input.GetMouseButtonDown(1))
        {
            StartSwing();   // 마우스 우클릭을 누르면 스윙 시작
        }
        else if (Input.GetMouseButtonUp(1))
        {
            EndSwing();     // 마우스 우클릭을 떼면 스윙 끝
        }

        DrawRope();         // 시각적으로 로프를 보여주는 함수
    }

    void HookPoint()
    {
        RaycastHit rayCastHit;
        Physics.Raycast(cam.position, cam.forward, out rayCastHit, rayDistance);

        RaycastHit sphereCastHit;
        Physics.SphereCast(cam.position, 4, cam.forward, out sphereCastHit, rayDistance);

        Vector3 realHitPoint;

        if (rayCastHit.point != Vector3.zero)
        {
            realHitPoint = rayCastHit.point;
        }
        else if (sphereCastHit.point != Vector3.zero)
        {
            realHitPoint = sphereCastHit.point;
        }

        hit = rayCastHit.point == Vector3.zero ? sphereCastHit : rayCastHit;
    }

    void StartSwing()
    {
        if (hit.point == Vector3.zero) return;

        isSwing = true;

        spot = hit.point;   // 로프를 연결할 지점 설정

        lr.positionCount = 2;                   // 라인 렌더러의 점 개수 설정
        lr.SetPosition(0, transform.position);  // 첫 번째 점을 플레이어 위치로 설정
        lr.SetPosition(1, hit.point);           // 두 번째 점을 레이캐스트 위치로 설정

        sj = gameObject.AddComponent<SpringJoint>();    // 스프링 조인트 컴포넌트 추가
        sj.autoConfigureConnectedAnchor = false;        // 연결된 앵커 자동 설정 비활성화
        sj.connectedAnchor = spot;                      // 연결 앵커를 훅 지점으로 설정

        sj.spring = springForce;    // 스프링 힘 설정
        sj.damper = springDamper;   // 스프링 댐퍼 설정
        sj.massScale = springMass;  // 스프링 질량 설정

        dis = Vector3.Distance(transform.position, spot);   // 플레이어와 로프 연결 지점 간의 거리 계산

        sj.maxDistance = dis * 0.3f;    // 스프링의 최대 길이 설정
        sj.minDistance = dis * 0.2f;    // 스프링의 최소 길이 설정
    }

    void EndSwing()
    {
        isSwing = false;
        lr.positionCount = 0;   // 라인 렌더러의 점 개수를 0으로 설정하여 선을 지움
        Destroy(sj);            // 스프링 조인트 컴포넌트 파괴
    }

    void DrawRope()
    {
        if (isSwing)
        {
            lr.SetPosition(0, transform.position);  // 로프의 첫 번째 점을 플레이어 위치로 설정하여 선을 그림
        }
    }
}
