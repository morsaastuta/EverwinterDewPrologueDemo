using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BowItem : WieldItem
{
    public BowItem() {
        dual = true;
    }
}
