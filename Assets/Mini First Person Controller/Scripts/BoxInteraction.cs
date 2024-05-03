using UnityEngine;

public class BoxInteraction : MonoBehaviour
{
    public Transform holdPoint; 
    private GameObject heldBox = null; 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldBox == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, 2f))
                {
                    if (hit.collider.gameObject.CompareTag("Box"))
                    {
                        PickUpBox(hit.collider.gameObject);
                    }
                }
            }
            else
            {
                DropBox();
            }
        }
    }

    void PickUpBox(GameObject box)
    {
        heldBox = box;
        heldBox.transform.SetParent(holdPoint); 
        heldBox.transform.position = holdPoint.position; 
        Rigidbody boxRb = heldBox.GetComponent<Rigidbody>();
        boxRb.isKinematic = true; 
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
