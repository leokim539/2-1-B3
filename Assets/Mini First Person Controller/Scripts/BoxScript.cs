using UnityEngine;

public class BoxScript : MonoBehaviour
{
    private bool isBeingHeld = false;

    public bool IsBeingHeld()
    {
        return isBeingHeld;
    }

    public void Pickup(Transform player)
    {
        isBeingHeld = true;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().isKinematic = true;
        this.transform.parent = player;
    }

    public void Drop()
    {
        isBeingHeld = false;
        this.transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
