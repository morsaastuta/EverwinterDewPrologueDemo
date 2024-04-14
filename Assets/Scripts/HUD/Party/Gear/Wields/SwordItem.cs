using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class SwordItem : WieldItem
{
    public SwordItem() {
        dual = false;
    }
}
