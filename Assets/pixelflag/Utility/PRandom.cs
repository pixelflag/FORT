using System;

public class PRandom
{
    private uint x;
    private uint y;
    private uint z;
    private uint w;

    public uint GetSeed() { return x; } 

    public PRandom(uint seed)
    {
        x = seed;
        y = x * 3266489917U + 1;
        z = y * 3266489917U + 1;
        w = z * 3266489917U + 1;
    }

    public uint GetNext()
    {
        uint t = x ^ (x << 11);
        x = y;
        y = z;
        z = w;
        w = (w ^ (w >> 19)) ^ (t ^ (t >> 8));
        return w;
    }

    public int Range(int min, int max)
    {
        return min + Math.Abs((int)GetNext()) % (max - 1);
    }
}