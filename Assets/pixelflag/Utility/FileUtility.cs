using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class FileUtility
{
    public static string[] GetFileNames(string path)
    {
        List<string> list = new List<string>();

        string[] fileNames = Directory.GetFiles(path);

        // metaƒtƒ@ƒCƒ‹‚Ìíœ
        foreach (string s in fileNames)
        {
            string[] sp = s.Split('.');
            if(sp[sp.Length-1] != "meta")
            {
                list.Add(s);
            }
        }

        fileNames = list.ToArray();

        // ”Ô†‡‚É•À‚Ñ‘Ö‚¦
        for (int i = 0; i < fileNames.Length - 1; i++)
        {
            for (int j = fileNames.Length - 1; j > i; j--)
            {
                string[] s1 = fileNames[j].Split('.');
                string[] s2 = fileNames[j - 1].Split('.');

                string[] s3 = s1[0].Split('_');
                string[] s4 = s2[0].Split('_');
                int num1 = Int32.Parse(s3[s3.Length - 1]);
                int num2 = Int32.Parse(s4[s4.Length - 1]);

                if (num1 < num2)
                {
                    string s = fileNames[j];
                    fileNames[j] = fileNames[j - 1];
                    fileNames[j - 1] = s;
                }
            }
        }
        return fileNames;
    }

    public static Sprite[] LoadSprite(string path)
    {
        Sprite[] sprites = new Sprite[0];
#if UNITY_EDITOR
        sprites = AssetDatabase.LoadAllAssetsAtPath(path).OfType<Sprite>().ToArray();
        SortSprites(sprites);
#endif
        return sprites;
    }

    private static void SortSprites(Sprite[] sprites)
    {
        for (int i = 0; i < sprites.Length - 1; i++)
        {
            for (int j = sprites.Length - 1; j > i; j--)
            {
                string[] str1 = sprites[j].name.Split('_');
                string[] str2 = sprites[j - 1].name.Split('_');
                int num1 = Int32.Parse(str1[str1.Length - 1]);
                int num2 = Int32.Parse(str2[str2.Length - 1]);

                if (num1 < num2)
                {
                    Sprite t = sprites[j];
                    sprites[j] = sprites[j - 1];
                    sprites[j - 1] = t;
                }
            }
        }
    }
}
