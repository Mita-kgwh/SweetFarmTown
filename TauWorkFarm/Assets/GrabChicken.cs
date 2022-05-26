using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Tool Action/Grab Chicken")]
public class GrabChicken : ToolsAction
{
    [SerializeField] float sizeOfInteractableArea = 0.5f;
    [SerializeField] List<string> objTagCanGrab;
    public override bool OnApply(Vector2 worldPoint)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(worldPoint, sizeOfInteractableArea);
        foreach (Collider2D c in colliders)
        {
            if (objTagCanGrab.Contains(c.transform.tag))
            {

                return true;
            }
        }
        return false;
    }
}
