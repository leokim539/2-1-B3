using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    public AudioSource ShootSource;

    public Rigidbody bulletPrefab; // 총알 프리팹
    public float shootForce = 10f; // 발사 힘
    public Transform bulletSpawnPoint; // 총알 발사 위치
    private Transform player; // 플레이어 트랜스폼

    float timer;
    int waitingTime;

    void Start ()
    {
        ShootSource = GetComponent<AudioSource>();

        player = transform; // 플레이어 트랜스폼 초기화
        timer = 0;
        waitingTime = 2;
    }
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > waitingTime){
            if(Input.GetMouseButtonDown(0))
            {
                ShootSource.Play();
                ShootInBullet();
                timer = 0;
            }
        }
    }

    void ShootInBullet()
    {
        Vector3 direction = bulletSpawnPoint.forward; // 발사 위치의 앞 방향
        Rigidbody bulletInstance = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.LookRotation(direction));
        bulletInstance.velocity = direction * shootForce * Time.deltaTime;
    }    
}

