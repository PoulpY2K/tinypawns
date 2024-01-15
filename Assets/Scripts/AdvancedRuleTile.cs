using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "RT_Advanced", menuName = "Advanced Rule Tile")]
public class AdvancedRuleTile : RuleTile
{
    public Color color = Color.white;

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        base.GetTileData(position, tilemap, ref tileData);

        tileData.color = color;
        tileData.flags = TileFlags.LockColor;
    }
}