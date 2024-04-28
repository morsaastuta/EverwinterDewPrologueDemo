using System;

[Serializable]
public class ClassWrapper<T>
{
    public T type;
    public ClassWrapper(T type) => this.type = type;
}