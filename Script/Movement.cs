using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Cinemachine;


public class Movement : MonoBehaviourPunCallbacks, IPunObservable//����*****
{
    // ���ŵ� ��ġ�� ȸ������ ������ ����
    private Vector3 receivePos;
    private Quaternion receiveRot;
    // ���ŵ� ��ǥ�� �̵� �� ȸ�� �ӵ��� �ΰ���
    public float damping = 10.0f;
    //������Ʈ ĳ�� ó���� ���� �Ӽ� ����
    private CharacterController controller;
    private new Transform transform;
    private Animator animator;
    private new Camera camera;

    //������ Plane�� ����ĳ�����ϱ� ���� �Ӽ� ����
    private Plane plane;
    private Ray ray;
    private Vector3 hitPoint;

    //�̵� �ӵ� �Ӽ�
    public float moveSpeed = 10.0f;

    //����� �̵� Ű���� �Է�
    float h => Input.GetAxis("Horizontal");
    float v => Input.GetAxis("Vertical");

    //PhotonView ������Ʈ ĳ�ø� ���� ���� ����
    private PhotonView pv;

    //�ó׸ӽ� ���� ī�޶� ������ ����
    private CinemachineVirtualCamera virtualCamera;


    // Start is called before the first frame update
    void Start()
    {
        //������Ʈ �ʱ�ȭ
        controller = GetComponent<CharacterController>();
        transform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        camera = Camera.main;

        pv = GetComponent<PhotonView>();
        virtualCamera = GameObject.FindObjectOfType<CinemachineVirtualCamera>();

        //PhotonView�� �ڽ��� ���� ��� �ó׸ӽ� ����ī�޶� ����
        if (pv.IsMine)
        {
            virtualCamera.Follow = transform;
            virtualCamera.LookAt = transform;
        }

        //������ �ٴ��� �÷��̾��� ��ġ�� ����
        plane = new Plane(transform.up, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        //PhotonView�� ���� ��Ʈ��ũ�� ��� ���� ��ü�� ��ġ, ȸ���� �� �ִϸ��̼��� ����ȭ ��
        //���� �ڽ��� ������ ��Ʈ��ũ ��ü(player)�� ��Ʈ�� ��
        if (pv.IsMine)
        {
            //�̵� ����
            Move();

            //ȸ�� ����
            Turn();
        }
        else
        {
            //���� ���� ��Ʈ�� ���ϴ� �÷��̾� 
            //���� ��Ÿ Ÿ�� �־����
            // ���ŵ� ��ǥ�� ������ �̵�ó��
            transform.position = Vector3.Lerp(transform.position, receivePos, Time.deltaTime * damping);
            // ���ŵ� ȸ�������� ������ ȸ��ó��
            transform.rotation = Quaternion.Slerp(transform.rotation, receiveRot, Time.deltaTime * damping);
        }
    }



    void Move()
    {
        Vector3 cameraForward = camera.transform.forward;
        Vector3 cameraRight = camera.transform.right;
        cameraForward.y = 0.0f;
        cameraRight.y = 0.0f;

        //�̵��� ���� ���� ���
        Vector3 moveDir = (cameraForward * v) + (cameraRight * h);
        moveDir.Set(moveDir.x, 0.0f, moveDir.z);

        //�÷��̾� �̵�
        controller.SimpleMove(moveDir * moveSpeed);

        //�÷��̾� �ִϸ��̼� ����
        float forward = Vector3.Dot(moveDir, transform.forward);
        float strafe = Vector3.Dot(moveDir, transform.right);

        animator.SetFloat("Forward", forward);
        animator.SetFloat("Strafe", strafe);
    }

    void Turn()
    {
        //���콺�� 2���� ��ǥ�� �̿��� 3���� ���� ����
        ray = camera.ScreenPointToRay(Input.mousePosition);

        //�浹 �������� �Ÿ�
        float enter = 0.0f;

        //������ �ٴڿ� ���̸� �߻��� �浹�� ������ �Ÿ��� enter�� �Ҵ�
        plane.Raycast(ray, out enter);

        //������ �ٴڿ� ���̰� �浹�� ��ǥ ����
        hitPoint = ray.GetPoint(enter);

        //ȸ���ؾ� �� ������ ���͸� ���
        Vector3 lookDir = hitPoint - transform.position;
        lookDir.y = 0;

        //�÷��̾��� ȸ���� ����
        transform.localRotation = Quaternion.LookRotation(lookDir);

    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // �ڽ��� ���� ĳ������ ��� �ڽ��� �����͸� �ٸ� ��Ʈ��ũ �������� �۽�
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