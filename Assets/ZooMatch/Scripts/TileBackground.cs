using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gestiona el fondo de las casillas. Pensado para poner distintos tipos de casillas dependiendo de su posición en el tablero. Finalmente se dejó una única casilla para mayor limpieza visual.
/// </summary>
public class TileBackground : MonoBehaviour
{
    public enum TileType
    {
        TOP,
        TOPLEFT,
        TOPRIGHT,
        LEFT,
        RIGHT,
        BOTTOM,
        BOTTOMLEFT,
        BOTTOMRIGHT,
        CENTER1,
        CENTER2,
        CENTER3,
        COUNT
    }

    [System.Serializable]
    public struct TileSprite
    {
        public TileType tile;
        public Sprite sprite;
    }

    public TileSprite[] tileSprites;

    private TileType tile;
    public TileType Tile
    {
        get { return tile; }
        set { SetTile(value); }
    }

    public int NumTiles
    {
        get { return tileSprites.Length; }
    }

    private SpriteRenderer sprite;

    private Dictionary<TileType, Sprite> tilesDICT;


    private void Awake()
    {
        sprite = transform.Find("bgSprite").GetComponent<SpriteRenderer>();
        tilesDICT = new Dictionary<TileType, Sprite>();
        for (int i = 0; i < tileSprites.Length; i++)
        {
            if (!tilesDICT.ContainsKey(tileSprites[i].tile))
            {
                tilesDICT.Add(tileSprites[i].tile, tileSprites[i].sprite);
            }
        }
    }

    /// <summary>
    /// Pone el fondo de la casilla
    /// </summary>
    /// <param name="newTile"></param>
    public void SetTile(TileType newTile)
    {
        tile = newTile;
        if (tilesDICT.ContainsKey(newTile))
        {
            sprite.sprite = tilesDICT[newTile];
        }
    }
}
