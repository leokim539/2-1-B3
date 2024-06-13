using UnityEngine;

public class ButtonInteraction : MonoBehaviour
{
    public AudioSource SpawnBoxSource;

    public Transform playerCamera; // 플레이어 카메라
    public GameObject boxPrefab; // Box 프리팹
    private GameObject currentBox; // 현재 생성된 Box 오브젝트
    public Transform boxOriginalPosition; // Box의 원래 위치를 정의하는 빈 오브젝트의 Transform
    
    void Start()
    {
        SpawnBoxSource = GetComponent<AudioSource>();
    }
    // 외부에서 호출 가능한 상자 생성 및 위치 복귀 메서드
    public void SpawnOrResetBox()
    {
        SpawnBoxSource.Play();
        if (currentBox == null) // Box가 생성되지 않은 경우
        {
            // Box 프리팹으로부터 새로운 Box 오브젝트 생성
            currentBox = Instantiate(boxPrefab, boxOriginalPosition.position, Quaternion.identity);
        }
        else // Box가 이미 생성된 경우
        {
            // Box 오브젝트를 원위치로 이동
            currentBox.transform.position = boxOriginalPosition.position;
        }
    }
}

// using UnityEngine;

// public class ButtonInteraction : MonoBehaviour
// {
//     public Transform playerCamera; // 플레이어 카메라
//     public GameObject boxPrefab; // Box 프리팹
//     private GameObject currentBox; // 현재 생성된 Box 오브젝트
//     public Transform boxOriginalPosition; // Box의 원래 위치를 정의하는 빈 오브젝트의 Transform

//     void Update()
//     {
//         RaycastHit hit;
//         // 플레이어가 버튼을 바라보고 E키를 눌렀는지 확인
//         if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, 2f) && hit.collider.gameObject == gameObject && Input.GetKeyDown(KeyCode.E))
//         {
//             if (currentBox == null) // Box가 생성되지 않은 경우
//             {
//                 // Box 프리팹으로부터 새로운 Box 오브젝트 생성
//                 currentBox = Instantiate(boxPrefab, boxOriginalPosition.position, Quaternion.identity);
//             }
//             else // Box가 이미 생성된 경우
//             {
//                 // Box 오브젝트를 원위치로 이동
//                 currentBox.transform.position = boxOriginalPosition.position;
//             }
//         }
//     }
// }

