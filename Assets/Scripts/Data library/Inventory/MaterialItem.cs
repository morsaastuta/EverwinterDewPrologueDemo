using Sirenix.Serialization;
using System;
using Unity.VisualScripting;

[Serializable]
public abstract class MaterialItem : Item
{
    [OdinSerialize] public int price;
}
