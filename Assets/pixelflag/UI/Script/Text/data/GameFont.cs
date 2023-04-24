using UnityEngine;

namespace pixelflag.UI
{
    public class GameFont : FontBase
    {
        [SerializeField]
        private CharData[] charData = new CharData[]
        {
            new CharData('A', 0, 8, 0),
            new CharData('B', 1, 8, 0),
            new CharData('C', 2, 8, 0),
            new CharData('D', 3, 8, 0),
            new CharData('E', 4, 8, 0),
            new CharData('F', 5, 8, 0),
            new CharData('G', 6, 8, 0),
            new CharData('H', 7, 8, 0),
            new CharData('I', 8, 8, 0),
            new CharData('J', 9, 8, 0),
            new CharData('K', 10, 8, 0),
            new CharData('L', 11, 8, 0),
            new CharData('M', 12, 8, 0),
            new CharData('N', 13, 8, 0),
            new CharData('O', 14, 8, 0),
            new CharData('P', 15, 8, 0),
            new CharData('Q', 16, 8, 0),
            new CharData('R', 17, 8, 0),
            new CharData('S', 18, 8, 0),
            new CharData('T', 19, 8, 0),
            new CharData('U', 20, 8, 0),
            new CharData('V', 21, 8, 0),
            new CharData('W', 22, 8, 0),
            new CharData('X', 23, 8, 0),
            new CharData('Y', 24, 8, 0),
            new CharData('Z', 25, 8, 0),

            new CharData('a', 32, 8, 0),
            new CharData('b', 33, 8, 0),
            new CharData('c', 34, 8, 0),
            new CharData('d', 35, 8, 0),
            new CharData('e', 36, 8, 0),
            new CharData('f', 37, 8, 0),
            new CharData('g', 38, 8, 0),
            new CharData('h', 39, 8, 0),
            new CharData('i', 40, 8, 0),
            new CharData('j', 41, 8, 0),
            new CharData('k', 42, 8, 0),
            new CharData('l', 43, 8, 0),
            new CharData('m', 44, 8, 0),
            new CharData('n', 45, 8, 0),
            new CharData('o', 46, 8, 0),
            new CharData('p', 47, 8, 0),
            new CharData('q', 48, 8, 0),
            new CharData('r', 49, 8, 0),
            new CharData('s', 50, 8, 0),
            new CharData('t', 51, 8, 0),
            new CharData('u', 52, 8, 0),
            new CharData('v', 53, 8, 0),
            new CharData('w', 54, 8, 0),
            new CharData('x', 55, 8, 0),
            new CharData('y', 56, 8, 0),
            new CharData('z', 57, 8, 0),

            new CharData('~', 64, 8, 0),
            new CharData('!', 65, 8, 0),
            new CharData('@', 66, 8, 0),
            new CharData('#', 67, 8, 0),
            new CharData('$', 68, 8, 0),
            new CharData('%', 69, 8, 0),
            new CharData('^', 70, 8, 0),
            new CharData('&', 71, 8, 0),
            new CharData('*', 72, 8, 0),
            new CharData('(', 73, 8, 0),
            new CharData(')', 74, 8, 0),
            new CharData('_', 75, 8, 0),
            new CharData('+', 76, 8, 0),
            new CharData('{', 77, 8, 0),
            new CharData('}', 78, 8, 0),
            new CharData('|', 79, 8, 0),
            new CharData(':', 80, 8, 0),
            new CharData('\'', 81, 8, 0),
            new CharData('<', 82, 8, 0),
            new CharData('>', 83, 8, 0),
            new CharData('?', 84, 8, 0),

            new CharData('`', 96, 8, 0),
            new CharData('1', 97, 8, 0),
            new CharData('2', 98, 8, 0),
            new CharData('3', 99, 8, 0),
            new CharData('4', 100, 8, 0),
            new CharData('5', 101, 8, 0),
            new CharData('6', 102, 8, 0),
            new CharData('7', 103, 8, 0),
            new CharData('8', 104, 8, 0),
            new CharData('9', 105, 8, 0),
            new CharData('0', 106, 8, 0),
            new CharData('-', 107, 8, 0),
            new CharData('=', 108, 8, 0),
            new CharData('[', 109, 8, 0),
            new CharData(']', 110, 8, 0),
            new CharData('\\', 111, 8, 0),
            new CharData(';', 112, 8, 0),
            new CharData('\'', 113, 8, 0),
            new CharData(',', 114, 8, 0),
            new CharData('.', 115, 8, 0),
            new CharData('/', 116, 8, 0),
            new CharData(' ', 117, 8, 0)
        };

        public override CharData GetCharData(char c)
        {
            for (int i = 0; i < charData.Length; i++)
            {
                if (charData[i].c == c)
                {
                    charData[i].sprite = sprites[charData[i].index];
                    return charData[i];
                }
            }

            throw new System.Exception("There is no char [" + c + "].");
        }
    }
}