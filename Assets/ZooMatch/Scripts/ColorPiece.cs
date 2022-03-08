using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPiece : MonoBehaviour
{
    public enum ColorType { 
        YELLOW,
        PURPLE, 
        RED,
        BLUE,
        GREEN,
        PINK, 
        ANY,
        COUNT
    }

    [System.Serializable]
    public struct ColorSprite
    {
        public ColorType color;
        public Sprite sprite;
    }

    public ColorSprite[] colorSprites;

    private ColorType color;
    public ColorType Color {
        get { return color; }
        set { SetColor(value); }
    }

    public int NumColors
    {
        get { return colorSprites.Length; }
    }

    private SpriteRenderer sprite;

    private Dictionary<ColorType, Sprite> colorSpriteDICT;

    private void Awake()
    {
        sprite = transform.Find("pieceSprite").GetComponent<SpriteRenderer>();
        colorSpriteDICT = new Dictionary<ColorType, Sprite>();
        for (int i = 0; i < colorSprites.Length; i++)
        {
            if (!colorSpriteDICT.ContainsKey(colorSprites[i].color)) {
                colorSpriteDICT.Add(colorSprites[i].color, colorSprites[i].sprite);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetColor(ColorType newColor) {
        color = newColor;
        if (colorSpriteDICT.ContainsKey(newColor)) {
            sprite.sprite = colorSpriteDICT[newColor];
        }
    }
}
