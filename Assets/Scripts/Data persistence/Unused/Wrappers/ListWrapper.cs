using System.Collections.Generic;

[System.Serializable]
public class ListWrapper<T>
{
    public List<T> list;
    public ListWrapper(List<T> list) => this.list = list;
}