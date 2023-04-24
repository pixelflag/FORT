using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringTools
{
    public static string Combine(params string[] strings)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        foreach(string st in strings)
        {
            sb.Append(st);
        }

        return sb.ToString();
    }
}
