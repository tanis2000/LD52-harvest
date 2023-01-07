using System.Collections.Generic;
using System.Linq;
using App.Generation;
using App.Utils;
using GameBase.Utils;
using UnityEngine;

namespace App.Rooms.Steps
{
    public class BaseLevelGridLayout : GenerationStep
    {
        public int MaxIterations = 1000;
        public int DesiredGroundTiles = 10;
        public bool Symmetric = false;
        public float DirectionRandom = 0.25f;
        public float JumpChance = 0.25f;
        public RectInt[] BaseRects;

        private void Set(LevelGrid grid, int x, int y, LevelGridTile t)
        {
            grid.Set(new Vector2Int(x, y), t.Clone());
            if (Symmetric)
            {
                grid.Set(new Vector2Int(y, x), t.Clone());
            }
        }

        public override void Generate(int seed)
        {
            var grid = GetComponent<LevelGrid>();
            var random = new XRandom(seed);
            var visited = new List<Vector2Int>();

            // Fill base rects
            foreach (var rect in BaseRects)
            {
                foreach (var p in rect.allPositionsWithin)
                {
                    Set(grid, p.x, p.y, new LevelGridTile() { Type = LevelGridTileType.Ground });
                    visited.Add(p);
                    if (Symmetric)
                    {
                        // NOTE: commenting this makes it more broken up
                        //visited.Add(new Vector2Int(p.y, p.x));
                    }
                }
            }

            // Iterate
            var bounds = new RectInt(
                grid.Bounds.xMin + 1,
                grid.Bounds.yMin + 1,
                grid.Bounds.xMax - 2,
                grid.Bounds.yMax - 2
            );

            var cursor = random.Item(visited);
            for (int i = 0; i < MaxIterations; i++)
            {
                if (random.Float() < JumpChance)
                {
                    var candidates = from p in visited
                        orderby CountNeighbours(grid, p, LevelGridTileType.Ground) +
                                random.Range(-DirectionRandom, DirectionRandom)
                        select p;
                    cursor = candidates.First();
                }
                else
                {
                    var directions =
                        from d in Vector2IntUtils.Directions4()
                        let pd = cursor + d
                        let pdt = grid.Get(pd)
                        where bounds.Contains(pd) && (pdt == null || pdt.Type == LevelGridTileType.Pit)
                        orderby CountNeighbours(grid, pd, LevelGridTileType.Ground) +
                                random.Range(-DirectionRandom, DirectionRandom)
                        select d;

                    if (directions.Count() == 0)
                    {
                        cursor = random.Item(visited);
                        continue;
                    }

                    var p = cursor + directions.First();
                    var t = grid.Get(p);

                    Set(grid, p.x, p.y, new LevelGridTile() { Type = LevelGridTileType.Ground });
                    visited.Add(p);
                    cursor = p;
                }

                if (visited.Count() >= DesiredGroundTiles)
                {
                    break;
                }
            }
        }

        int CountNeighbours(LevelGrid grid, Vector2Int p, LevelGridTileType type)
        {
            var count = 0;
            foreach (var d in Vector2IntUtils.Directions4())
            {
                var t = grid.Get(p);
                if (t != null && t.Type == type)
                {
                    count++;
                }
            }

            return count;
        }
    }
}