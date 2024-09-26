using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Cinemachine;


public class Movement : MonoBehaviourPunCallbacks, IPunObservable//공부*****
{
    // 수신된 위치와 회전값을 저장할 변수
    private Vector3 receivePos;
    private Quaternion receiveRot;
    // 수신된 좌표로 이동 및 회전 속도의 민감도
    public float damping = 10.0f;
    //컴포넌트 캐시 처리를 위한 속성 선언
    private CharacterController controller;
    private new Transform transform;
    private Animator animator;
    private new Camera camera;

    //가상의 Plane에 레이캐스팅하기 위한 속성 선언
    private Plane plane;
    private Ray ray;
    private Vector3 hitPoint;

    //이동 속도 속성
    public float moveSpeed = 10.0f;

    //사용자 이동 키보드 입력
    float h => Input.GetAxis("Horizontal");
    float v => Input.GetAxis("Vertical");

    //PhotonView 컴포넌트 캐시를 위한 변수 선언
    private PhotonView pv;

    //시네머신 가상 카메라를 저장할 변수
    private CinemachineVirtualCamera virtualCamera;


    // Start is called before the first frame update
    void Start()
    {
        //컴포넌트 초기화
        controller = GetComponent<CharacterController>();
        transform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        camera = Camera.main;

        pv = GetComponent<PhotonView>();
        virtualCamera = GameObject.FindObjectOfType<CinemachineVirtualCamera>();

        //PhotonView가 자신의 것일 경우 시네머신 가상카메라를 연결
        if (pv.IsMine)
        {
            virtualCamera.Follow = transform;
            virtualCamera.LookAt = transform;
        }

        //가상의 바닥을 플레이어의 위치로 생성
        plane = new Plane(transform.up, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        //PhotonView에 의해 네트워크의 모든 유저 객체의 위치, 회전값 및 애니메이션이 동기화 됨
        //따라서 자신이 생성한 네트워크 객체(player)만 컨트롤 함
        if (pv.IsMine)
        {
            //이동 구현
            Move();

            //회전 구현
            Turn();
        }
        else
        {
            //시험 내가 컨트롤 안하는 플레이어 
            //시험 델타 타임 넣어야함
            // 수신된 좌표로 보간한 이동처리
            transform.position = Vector3.Lerp(transform.position, receivePos, Time.deltaTime * damping);
            // 수신된 회전값으로 보간한 회전처리
            transform.rotation = Quaternion.Slerp(transform.rotation, receiveRot, Time.deltaTime * damping);
        }
    }



    void Move()
    {
        Vector3 cameraForward = camera.transform.forward;
        Vector3 cameraRight = camera.transform.right;
        cameraForward.y = 0.0f;
        cameraRight.y = 0.0f;

        //이동할 방향 벡터 계산
        Vector3 moveDir = (cameraForward * v) + (cameraRight * h);
        moveDir.Set(moveDir.x, 0.0f, moveDir.z);

        //플레이어 이동
        controller.SimpleMove(moveDir * moveSpeed);

        //플레이어 애니메이션 지정
        float forward = Vector3.Dot(moveDir, transform.forward);
        float strafe = Vector3.Dot(moveDir, transform.right);

        animator.SetFloat("Forward", forward);
        animator.SetFloat("Strafe", strafe);
    }

    void Turn()
    {
        //마우스의 2차원 좌표를 이용해 3차원 레이 생성
        ray = camera.ScreenPointToRay(Input.mousePosition);

        //충돌 지점과의 거리
        float enter = 0.0f;

        //가상의 바닥에 레이를 발사해 충돌한 지점의 거리를 enter에 할당
        plane.Raycast(ray, out enter);

        //가상의 바닥에 레이가 충돌한 좌표 추출
        hitPoint = ray.GetPoint(enter);

        //회전해야 할 방향의 벡터를 계산
        Vector3 lookDir = hitPoint - transform.position;
        lookDir.y = 0;

        //플레이어의 회전값 지정
        transform.localRotation = Quaternion.LookRotation(lookDir);

    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // 자신의 로컬 캐릭터인 경우 자신의 데이터를 다른 네트워크 유저에게 송신
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            receivePos = (Vector3)stream.ReceiveNext();
            receiveRot = (Quaternion)stream.ReceiveNext();
        }
    }
}