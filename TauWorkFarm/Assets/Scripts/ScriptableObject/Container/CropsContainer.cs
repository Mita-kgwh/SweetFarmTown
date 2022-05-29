using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Container/Crops Container")]
public class CropsContainer : ScriptableObject
{
    public List<CropTile> crops;

    public CropTile Get(Vector3Int _position)
    {
        return crops.Find(x => x.position == _position);
    }

    public void Add(CropTile _crop)
    {
        crops.Add(_crop);
    }

    internal void Clear()
    {
        crops = new List<CropTile>();
    }
}
