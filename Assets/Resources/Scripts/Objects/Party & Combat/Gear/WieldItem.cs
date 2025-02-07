using Sirenix.Serialization;
using System;
using Unity.VisualScripting;

[Serializable]
public abstract class WieldItem : GearItem
{
    [OdinSerialize] public bool dual;
}
