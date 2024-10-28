using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Fire : MonoBehaviour
{
    public Transform firePos;
    public GameObject bulletPrefab;
    private ParticleSystem muzzleFlash;
    private PhotonView pv;
    private bool isMouseClick => Input.GetMouseButtonDown(0); // ���� ���콺 ��ư Ŭ�� �̺�Ʈ ����(���� �Լ�)
    void Start()
    {
        // ����� ������Ʈ ����
        pv = GetComponent<PhotonView>();
        // FirePos ������ �ִ� �ѱ� ȭ�� ȿ�� ����
        muzzleFlash = firePos.Find("MuzzleFlash").GetComponent<ParticleSystem>();
    }
    void Update()
    {
        // ���� �������ο� ���콺 ���� ��ư�� Ŭ������ �� �Ѿ��� �߻�
        if (pv.IsMine && isMouseClick)
        {
            FireBullet();
            //RPC�� �������� �ִ� �Լ��� ȣ��
            pv.RPC("FireBullet", RpcTarget.Others, null);
        }
    }
    [PunRPC]
    void FireBullet()
    {
        // �ѱ�ȭ�� ȿ���� ���� ���� �ƴ� ��쿡 �ѱ� ȭ��ȿ�� ����
        if (!muzzleFlash.isPlaying) muzzleFlash.Play(true);
        GameObject bullet = Instantiate(bulletPrefab, firePos.position, firePos.rotation);
    }
}