using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pixelflag.UI
{
    public class SpritePool : MonoBehaviour
    {
        public static SpritePool instance { get; private set; }

        [SerializeField]
        private GameObject poolingSpritePrefav = default;

        [SerializeField]
        private int spritePoolingCount = 128;

        private List<PoolingSprite> pSprites;

        void Awake()
        {
            instance = this;

            pSprites = new List<PoolingSprite>(spritePoolingCount);
            for (int i = 0; i <= spritePoolingCount; i++)
            {
                GameObject newSprite = Instantiate(poolingSpritePrefav);
                newSprite.GetComponent<PoolingSprite>().SetDefaultParent(transform);
                pSprites.Add(newSprite.GetComponent<PoolingSprite>());
            }
        }

        public PoolingSprite GetPoolSprite()
        {
            foreach (PoolingSprite sprite in pSprites)
            {
                if (!sprite.isActive)
                    return sprite;
            }

            Debug.Log("[BitmapFont] Add New Sprite");
            GameObject newSprite = Instantiate(poolingSpritePrefav);
            newSprite.transform.SetParent(transform);

            PoolingSprite ps = newSprite.GetComponent<PoolingSprite>();
            pSprites.Add(ps);

            return ps;
        }
    }
}