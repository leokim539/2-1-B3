using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private GameObject currentInteractableButton = null;

    public Material glowMaterial; // 빛나는 효과를 줄 머티리얼
    private Material defaultMaterial; // 버튼의 기본 머티리얼

    void Update()
    {
        // 플레이어가 버튼 근처에 있고, "E" 키를 누르면 버튼을 활성화.
        if (currentInteractableButton != null && Input.GetKeyDown(KeyCode.E))
        {
            ButtonInteraction buttonScript = currentInteractableButton.GetComponent<ButtonInteraction>();
            if (buttonScript != null)
            {
                buttonScript.SpawnOrResetBox();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Button")) // 버튼의 태그를 확인.
        {
            currentInteractableButton = other.gameObject; // 상호작용 가능한 버튼으로 설정.
            defaultMaterial = currentInteractableButton.GetComponent<Renderer>().material; // 기본 머티리얼 저장
            currentInteractableButton.GetComponent<Renderer>().material = glowMaterial; // 빛나는 효과 적용
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == currentInteractableButton) // 상호작용 중이던 버튼에서 벗어난 경우
        {
            currentInteractableButton.GetComponent<Renderer>().material = defaultMaterial; // 기본 머티리얼로 복원
            currentInteractableButton = null; // 상호작용 가능한 버튼을 초기화.
        }
    }
}


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PlayerInteraction : MonoBehaviour
// {
//     private GameObject currentInteractableButton = null;

//     void Update()
//     {
//         // 플레이어가 버튼 근처에 있고, "E" 키를 누르면 버튼을 활성화.
//         if (currentInteractableButton != null && Input.GetKeyDown(KeyCode.E))
//         {
//             // 버튼과 상호작용
//             // 예: currentInteractableButton.GetComponent<ButtonScript>().ActivateButton();
//             Debug.Log("버튼 활성화");
//         }
//     }

//     void OnTriggerEnter(Collider other)
//     {
//         if (other.CompareTag("Button")) // 버튼의 태그를 확인.
//         {
//             currentInteractableButton = other.gameObject; // 상호작용 가능한 버튼으로 설정.
//         }
//     }

//     void OnTriggerExit(Collider other)
//     {
//         if (other.gameObject == currentInteractableButton) // 상호작용 중이던 버튼에서 벗어난 경우
//         {
//             currentInteractableButton = null; // 상호작용 가능한 버튼을 초기화.
//         }
//     }
// }
