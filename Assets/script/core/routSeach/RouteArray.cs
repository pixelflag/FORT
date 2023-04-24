using UnityEngine;

public class RouteArray
{
    public Vector3[] array;
    private int head;

    public int Length { get { return head == -1? 0 : head + 1; } }
    public int FixedLength { get { return array.Length; } }

    public RouteArray(int length)
    {
        array = new Vector3[length];

        for (int i = 0; i < length; i++)
            array[i] = new Vector3();

        head = -1;
    }

    public Vector3 GetPosition(int index)
    {
        if (head < index)
            throw new System.Exception("out of index : " + index + ", head : " + head);

        return array[index];
    }

    public void AddRoute(Vector3 position)
    {
        head++;
        array[head] = position;
    }

    public void Clear()
    {
        head = -1;
    }
}
