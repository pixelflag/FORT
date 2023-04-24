using UnityEngine;

namespace pixelflag.UI
{
    public class UiFont : FontBase
    {
        [SerializeField]
        private CharData[] charData = new CharData[]
        {
            new CharData('A', 0, 6, 0),
            new CharData('B', 1, 6, 0),
            new CharData('C', 2, 6, 0),
            new CharData('D', 3, 6, 0),
            new CharData('E', 4, 6, 0),
            new CharData('F', 5, 6, 0),
            new CharData('G', 6, 6, 0),
            new CharData('H', 7, 6, 0),
            new CharData('I', 8, 4, 0),
            new CharData('J', 9, 6, 0),
            new CharData('K', 10, 6, 0),
            new CharData('L', 11, 6, 0),
            new CharData('M', 12, 8, 0),
            new CharData('N', 13, 6, 0),
            new CharData('O', 14, 6, 0),
            new CharData('P', 15, 6, 0),
            new CharData('Q', 16, 6, 0),
            new CharData('R', 17, 6, 0),
            new CharData('S', 18, 6, 0),
            new CharData('T', 19, 6, 0),
            new CharData('U', 20, 6, 0),
            new CharData('V', 21, 6, 0),
            new CharData('W', 22, 8, 0),
            new CharData('X', 23, 6, 0),
            new CharData('Y', 24, 6, 0),
            new CharData('Z', 25, 6, 0),

            new CharData('a', 32, 6, 0),
            new CharData('b', 33, 6, 0),
            new CharData('c', 34, 6, 0),
            new CharData('d', 35, 6, 0),
            new CharData('e', 36, 6, 0),
            new CharData('f', 37, 6, 0),
            new CharData('g', 38, 6, 0),
            new CharData('h', 39, 6, 0),
            new CharData('i', 40, 2, 0),
            new CharData('j', 41, 6, 0),
            new CharData('k', 42, 6, 0),
            new CharData('l', 43, 2, 0),
            new CharData('m', 44, 8, 0),
            new CharData('n', 45, 6, 0),
            new CharData('o', 46, 6, 0),
            new CharData('p', 47, 6, 0),
            new CharData('q', 48, 6, 0),
            new CharData('r', 49, 6, 0),
            new CharData('s', 50, 6, 0),
            new CharData('t', 51, 6, 0),
            new CharData('u', 52, 6, 0),
            new CharData('v', 53, 6, 0),
            new CharData('w', 54, 8, 0),
            new CharData('x', 55, 6, 0),
            new CharData('y', 56, 6, 0),
            new CharData('z', 57, 6, 0),

            new CharData('~', 64, 6, 0),
            new CharData('!', 65, 6, 0),
            new CharData('@', 66, 6, 0),
            new CharData('#', 67, 6, 0),
            new CharData('$', 68, 6, 0),
            new CharData('%', 69, 6, 0),
            new CharData('^', 70, 6, 0),
            new CharData('&', 71, 6, 0),
            new CharData('*', 72, 6, 0),
            new CharData('(', 73, 6, 0),
            new CharData(')', 74, 6, 0),
            new CharData('_', 75, 6, 0),
            new CharData('+', 76, 6, 0),
            new CharData('{', 77, 6, 0),
            new CharData('}', 78, 6, 0),
            new CharData('|', 79, 6, 0),
            new CharData(':', 80, 6, 0),
            new CharData('\'', 81, 6, 0),
            new CharData('<', 82, 6, 0),
            new CharData('>', 83, 6, 0),
            new CharData('?', 84, 6, 0),

            new CharData('`', 96, 6, 0),
            new CharData('1', 97, 6, 0),
            new CharData('2', 98, 6, 0),
            new CharData('3', 99, 6, 0),
            new CharData('4', 100, 6, 0),
            new CharData('5', 101, 6, 0),
            new CharData('6', 102, 6, 0),
            new CharData('7', 103, 6, 0),
            new CharData('8', 104, 6, 0),
            new CharData('9', 105, 6, 0),
            new CharData('0', 106, 6, 0),
            new CharData('-', 107, 6, 0),
            new CharData('=', 108, 6, 0),
            new CharData('[', 109, 6, 0),
            new CharData(']', 110, 6, 0),
            new CharData('\\', 111, 6, 0),
            new CharData(';', 112, 6, 0),
            new CharData('\'', 113, 6, 0),
            new CharData(',', 114, 6, 0),
            new CharData('.', 115, 6, 0),
            new CharData('/', 116, 6, 0),
            new CharData(' ', 117, 6, 0)
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