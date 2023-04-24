using System;
using UnityEngine;

namespace pixelflag.UI
{
    [Serializable]
    public struct CharData
    {
        public char c { get; private set; }
        public int index { get; private set; }
        public int width { get; private set; }
        public int offsetY { get; private set; }
        public Sprite sprite;

        public CharData(char c, int index, int width, int offsetY)
        {
            this.c = c;
            this.index = index;
            this.width = width;
            this.offsetY = offsetY;
            sprite = null;
        }
    }
}