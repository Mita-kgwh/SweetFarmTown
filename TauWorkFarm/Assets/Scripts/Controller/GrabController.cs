using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabController : MonoBehaviour
{
    [SerializeField] GameObject grabHolder;
    [SerializeField] float sizeOfGrabArea = 0.5f;
    [SerializeField] List<string> canGrabTags;
    bool grabingsth;

    public void Grab(Vector2 position)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfGrabArea);
        foreach (Collider2D c in colliders)
        {
            if (canGrabTags.Contains(c.transform.tag))
            {
                if (grabingsth)
                {
                    Debug.Log("release");
                    c.gameObject.transform.parent = null;
                    c.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
                    //c.enabled = true;
                    PetManager pet = c.gameObject.GetComponent<PetManager>();
                    pet.Grabed(false);
                    pet.enabled = true;
                    grabingsth = false;
                }
                else
                {
                    Debug.Log("catch");
                    //c.enabled = false;
                    c.gameObject.transform.parent = grabHolder.transform;
                    c.gameObject.transform.position = grabHolder.transform.position;
                    c.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                    PetManager pet = c.gameObject.GetComponent<PetManager>();
                    pet.Grabed(true);
                    pet.enabled = false;
                    grabingsth = true;
                }
                return;
            }
        }
    }
}
