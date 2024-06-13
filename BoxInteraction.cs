using System.Collections.Generic;
using UnityEngine;

public class BoxInteraction : MonoBehaviour
{
    public AudioSource PickUpBoxSource;
    public AudioSource DropBoxSource;

    public Transform holdPoint; 
    public Material glowMaterial;
    private GameObject heldBox = null;
    private GameObject potentialBox = null; // 들 수 있는 상자를 임시 저장할 변수
    private Dictionary<GameObject, Material> originalMaterials = new Dictionary<GameObject, Material>();

    void Start()
    {
        PickUpBoxSource = GetComponent<AudioSource>();
        DropBoxSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldBox == null && potentialBox != null) // 들고 있는 상자가 없고, 들 수 있는 상자가 감지되었을 때
            {
                PickUpBox(potentialBox);
                PickUpBoxSource.Play();
            }
            else
            {
                DropBox();
                DropBoxSource.Play();
            }
        }

        // 상자 감지 로직은 OnTrigger 이벤트로 이동
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            potentialBox = other.gameObject; // 들 수 있는 상자로 설정
            // 원래 재질이 저장되어 있지 않다면 저장
            if (!originalMaterials.ContainsKey(potentialBox))
            {
                originalMaterials[potentialBox] = potentialBox.GetComponent<Renderer>().material;
            }
            potentialBox.GetComponent<Renderer>().material = glowMaterial; // 빛나는 재질 적용
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == potentialBox) // 범위를 벗어난 객체가 감지된 상자라면
        {
            // 원래 재질로 복구
            if (originalMaterials.ContainsKey(potentialBox))
            {
                potentialBox.GetComponent<Renderer>().material = originalMaterials[potentialBox];
                originalMaterials.Remove(potentialBox);
            }
            potentialBox = null; // 들 수 있는 상자 없음으로 설정
        }
    }

    void PickUpBox(GameObject box)
    {
        heldBox = box;
        heldBox.transform.SetParent(holdPoint); 
        heldBox.transform.position = holdPoint.position; 
        Rigidbody boxRb = heldBox.GetComponent<Rigidbody>();
        boxRb.isKinematic = true;

        // 상자를 들었으므로 원래 재질로 복구
        if (originalMaterials.ContainsKey(heldBox))
        {
            heldBox.GetComponent<Renderer>().material = originalMaterials[heldBox];
            originalMaterials.Remove(heldBox);
        }
        potentialBox = null; // 들고 있는 상자가 있으므로 잠재적 상자를 초기화
    }

    void DropBox()
    {
        if (heldBox != null)
        {
            Rigidbody boxRb = heldBox.GetComponent<Rigidbody>();
            boxRb.isKinematic = false;
            heldBox.transform.SetParent(null);
            heldBox = null;
        }
    }
}


// using System.Collections.Generic;
// using UnityEngine;

// public class BoxInteraction : MonoBehaviour
// {
//     public Transform holdPoint; 
//     public Material glowMaterial; // 빛나는 재질을 저장할 변수
//     private GameObject heldBox = null;
//     private Dictionary<GameObject, Material> originalMaterials = new Dictionary<GameObject, Material>(); // 원래 재질을 저장할 변수

//     void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.E))
//         {
//             if (heldBox == null)
//             {
//                 RaycastHit hit;
//                 if (Physics.Raycast(transform.position, transform.forward, out hit, 2.5f))
//                 {
//                     if (hit.collider.gameObject.CompareTag("Box"))
//                     {
//                         PickUpBox(hit.collider.gameObject);
//                     }
//                 }
//             }
//             else
//             {
//                 DropBox();
//             }
//         }
//         else
//         {
//             RaycastHit hit;
//             if (Physics.Raycast(transform.position, transform.forward, out hit, 2.5f) && hit.collider.gameObject.CompareTag("Box"))
//             {
//                 GameObject targetBox = hit.collider.gameObject;
//                 if (targetBox != heldBox) // 현재 들고 있는 상자가 아니라면
//                 {
//                     if (!originalMaterials.ContainsKey(targetBox)) // 원래 재질이 저장되어 있지 않다면 저장
//                     {
//                         originalMaterials[targetBox] = targetBox.GetComponent<Renderer>().material;
//                     }
//                     targetBox.GetComponent<Renderer>().material = glowMaterial; // 빛나는 재질 적용
//                 }
//             }
//             else
//             {
//                 // 모든 상자들을 순회하며 원래 재질로 복구
//                 foreach (var item in originalMaterials)
//                 {
//                     item.Key.GetComponent<Renderer>().material = item.Value;
//                 }
//                 originalMaterials.Clear(); // 원래 재질 정보 초기화
//             }
//         }
//     }

//     void PickUpBox(GameObject box)
//     {
//         heldBox = box;
//         heldBox.transform.SetParent(holdPoint); 
//         heldBox.transform.position = holdPoint.position; 
//         Rigidbody boxRb = heldBox.GetComponent<Rigidbody>();
//         boxRb.isKinematic = true;

//         if (originalMaterials.ContainsKey(heldBox)) // 상자를 들었으므로 원래 재질로 복구
//         {
//             heldBox.GetComponent<Renderer>().material = originalMaterials[heldBox];
//             originalMaterials.Remove(heldBox);
//         }
//     }

//     void DropBox()
//     {
//         if (heldBox != null)
//         {
//             Rigidbody boxRb = heldBox.GetComponent<Rigidbody>();
//             boxRb.isKinematic = false; 
//             heldBox.transform.SetParent(null); 
//             heldBox = null; 
//         }
//     }
// }


//using UnityEngine;

// public class BoxInteraction : MonoBehaviour
// {
//     public Transform holdPoint; 
//     private GameObject heldBox = null; 

//     void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.E))
//         {
//             if (heldBox == null)
//             {
//                 RaycastHit hit;
//                 if (Physics.Raycast(transform.position, transform.forward, out hit, 2.5f))
//                 {
//                     if (hit.collider.gameObject.CompareTag("Box"))
//                     {
//                         PickUpBox(hit.collider.gameObject);
//                     }
//                 }
//             }
//             else
//             {
//                 DropBox();
//             }
//         }
//     }

//     void PickUpBox(GameObject box)
//     {
//         heldBox = box;
//         heldBox.transform.SetParent(holdPoint); 
//         heldBox.transform.position = holdPoint.position; 
//         Rigidbody boxRb = heldBox.GetComponent<Rigidbody>();
//         boxRb.isKinematic = true; 
//     }

//     void DropBox()
//     {
//         if (heldBox != null)
//         {
//             Rigidbody boxRb = heldBox.GetComponent<Rigidbody>();
//             boxRb.isKinematic = false; 
//             heldBox.transform.SetParent(null); 
//             heldBox = null; 
//         }
//     }
// }
