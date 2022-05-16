using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Dirts Container")]
public class DirtsContainer : ScriptableObject
{
    public List<DirtTile> dirts;

    public DirtTile Get(Vector3Int _position)
    {
        return dirts.Find(x => x.position == _position);
    }

    public void Add(DirtTile dirtTile)
    {
        dirts.Add(dirtTile);
    }
}
