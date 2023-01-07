using App.Generation.Grid;
using App.Utils;
using UnityEngine;

namespace App.Rooms
{
    public enum LevelGridTileType
    {
        Pit = 0,
        Ground = 1,
        BridgeVertical = 2,
        BridgeHorizontal = 3,
        TallWall = 4,
        Outside = 50,
    }

    [System.Serializable]
    public class LevelGridTile : ICloneable<LevelGridTile>
    {
        public LevelGridTileType Type = LevelGridTileType.Pit;

        public LevelGridTile Clone()
        {
            return new LevelGridTile()
            {
                Type = this.Type,
            };
        }
    }

    public class LevelGrid : Grid<LevelGridTile>
    {
        public int Scale = 8;

        private void OnDrawGizmos()
        {
            if (Tiles == null)
            {
                return;
            }

            Gizmos.matrix = transform.localToWorldMatrix * Matrix4x4.Scale(Vector3.one * Scale);
            var flat = new Vector3(1, 0, 1);
            var half = new Vector3(0.5f, 0, 0.5f);

            var width = Bounds.width;
            var height = Bounds.height;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var t = Get(x, y);
                    var color = Color.black;
                    if (t == null)
                    {
                        continue;
                    }

                    if (t.Type == LevelGridTileType.Pit)
                    {
                        color = Color.magenta;
                    }

                    if (t.Type == LevelGridTileType.Ground)
                    {
                        color = Color.white;
                    }

                    if (t.Type == LevelGridTileType.BridgeVertical)
                    {
                        color = Color.cyan;
                    }

                    if (t.Type == LevelGridTileType.BridgeHorizontal)
                    {
                        color = Color.yellow;
                    }

                    if (t.Type == LevelGridTileType.TallWall)
                    {
                        color = Color.black;
                    }

                    Gizmos.color = color;
                    Gizmos.DrawCube(new Vector3(x, 0, y) + half, flat);
                    Gizmos.DrawWireCube(new Vector3(x, 0, y) + half, flat);
                }
            }
        }
    }
}