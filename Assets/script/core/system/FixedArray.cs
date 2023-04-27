using UnityEngine;

public class FixedArray<T>
{
    public T[] array;
    public int length => head;
    public int fixedLength => array.Length;
    private int head;

    public FixedArray(int length)
    {
        array = new T[length];
        head = 0;
    }

    public T Get(int index)
    {
        if (head <= index)
            throw new System.Exception("out of index : " + index + ", head : " + head);

        return array[index];
    }

    public void Add(T value)
    {
        array[head] = value;
        head++;

        if (head < array.Length )
            array[head] = default(T);
    }

    public void Clear()
    {
        head = 0;
        array[0] = default(T);
    }
}
