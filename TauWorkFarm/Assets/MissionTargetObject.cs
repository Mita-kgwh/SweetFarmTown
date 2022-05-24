using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionTargetObject : MonoBehaviour
{
    public virtual MissionTargetID ID { get; }

    public virtual void Accept()
    {

    }

    public virtual void Decline()
    {

    }

    public virtual void Complete()
    {

    }
}
