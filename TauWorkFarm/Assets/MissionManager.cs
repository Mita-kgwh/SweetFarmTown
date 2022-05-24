using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MissionTargetID
{
    [Type(typeof(CollectingResource))]
    COLLECTING_RESOURCE,
    [Type(typeof(FindingObjects))]
    FINDING_OBJECT,

}

public class MissionManager : MonoBehaviour
{
    private static MissionManager _instance;
    public static MissionManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<MissionManager>();
            return _instance;
        }
    }

    public MissionTargetObject SpawnAMission(MissionTargetID id)
    {
        return System.Activator.CreateInstance(EnumUtil.GetStringType(id)) as MissionTargetObject;
    }
}
public static class EnumUtil
{
    public static System.Type GetStringType(System.Enum value)
    {
        System.Type result = null;
        System.Type type = value.GetType();
        if (type != null)
        {
            System.Reflection.FieldInfo field = type.GetField(value.ToString());
            if (field != null)
            {
                TypeAttribute[] array = field.GetCustomAttributes(typeof(TypeAttribute), false) as TypeAttribute[];
                if (array != null)
                {
                    if (array.Length > 0)
                    {
                        result = array[0].Type;
                    }
                }

            }

        }

        return result;
    }
}
public class TypeAttribute : System.Attribute
{
    private System.Type _type;

    public TypeAttribute(System.Type type)
    {
        this._type = type;
    }
    public System.Type Type
    {
        get
        {
            return this._type;
        }
    }
}