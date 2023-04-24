using System.Collections.Generic;
using UnityEngine;

public class FixedArray<T>
{
    public T[] array;
    private int head;

    public int Length { get { return head; } }
    public int FixedLength { get { return array.Length; } }

    public bool headIsTail { get { return FixedLength-1 == head; } }

    public FixedArray(int length)
    {
        array = new T[length];
        head = 0;
    }

    public void Add(T value)
    {
        if(array.Length <= head)
        {
            Debug.Log("The range has been exceeded.");
            return;
        }

        array[head] = value;
        head++;

        if (head < array.Length )
        {
            array[head] = default(T);
        }
    }

    public void AddRange(T[] values)
    {
        foreach (T a in values)
        {
            Add(a);
        }
    }

    public void AddRange(List<T> values)
    {
        foreach (T a in values)
        {
            Add(a);
        }
    }

    public T Get(int index)
    {
        return array[index];
    }


    public void Set(int index, T value)
    {
        array[index] = value;
    }

    public void Clear()
    {
        head = 0;
        array[0] = default(T);
    }

    public void Kill()
    {
        head = 0;
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = default(T);
        }
    }
}
