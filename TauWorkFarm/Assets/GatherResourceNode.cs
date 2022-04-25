using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceNodeType
{
    Undefined,
    Tree,
    Ore
}


[CreateAssetMenu(menuName = "Data/Tool Action/ Gather Resource Node")]
public class GatherResourceNode : ToolsAction
{
    [SerializeField] float sizeOfInteractableArea = 1f;
    [SerializeField] List<ResourceNodeType> canbeHitOfType;
    public override bool OnApply(Vector2 worldPoint)
    {
  
        Collider2D[] colliders = Physics2D.OverlapCircleAll(worldPoint, sizeOfInteractableArea);
        foreach (Collider2D c in colliders)
        {
            ToolHit hit = c.GetComponent<ToolHit>();
            if (hit != null)
            {
                if (hit.CanBeHit(canbeHitOfType))
                {
                    hit.Hit();
                    return true;
                }
            }
        }

        return false;
    }
}
