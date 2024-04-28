using System;
using UnityEngine;

[Serializable]
public class SerializedData
{
    public Type type;
    public string data;

    public static SerializedData Serialize(object obj)
    {
        var result = new SerializedData()
        {
            type = obj.GetType(),
            data = JsonUtility.ToJson(obj)
        };

        return result;
    }
    //Returns as an object, which can then be cast
    public static object Deserialize(SerializedData sd)
    {
        return JsonUtility.FromJson(sd.data, sd.type);
    }
}