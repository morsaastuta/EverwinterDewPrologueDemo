using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Item
{
    public Sprite icon;
    public string name;
    public string description;
    public bool stackable;
}
