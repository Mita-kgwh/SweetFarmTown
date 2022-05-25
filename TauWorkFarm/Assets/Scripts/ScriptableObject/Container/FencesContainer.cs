using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Fences Container")]
public class FencesContainer : ScriptableObject
{
    public List<FenceTile> fences;

    public FenceTile Get(Vector3Int _position)
    {
        return fences.Find(x => x.position == _position);
    }

    public void Add(FenceTile fenceTile)
    {
        fences.Add(fenceTile);
    }

    public void Remove(FenceTile fenceTile)
    {
        fences.Remove(fenceTile);
    }
}
