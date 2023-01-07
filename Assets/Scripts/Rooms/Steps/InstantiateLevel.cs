using System.Linq;
using App.Generation;
using App.Rooms.Matchers;
using GameBase.Utils;
using UnityEngine;

namespace App.Rooms.Steps
{
    public class InstantiateLevel : GenerationStep
    {
        public Transform Root;
        public Transform Library;

        public override void Clear()
        {
            base.Clear();
            DestroyUtils.DestroyChildren(Root);
        }

        public override void Generate(int seed)
        {
            var grid = GetComponent<LevelGrid>();
            var random = new XRandom(seed);
            var tileMatchers = Library.GetComponentsInChildren<TileMatcher>();

            foreach (var p in grid.Bounds.allPositionsWithin)
            {
                var t = grid.Get(p);
                var candidates =
                    from tm in tileMatchers
                    where tm.Match(grid, p, t)
                    orderby random.Float()
                    select tm;

                if (candidates.Count() == 0)
                {
                    continue;
                }
                var prefab = candidates.First().transform;
                var position = transform.position + p.AsVector3() * grid.Scale;
                Instantiate(prefab, position, prefab.rotation, Root);
            }
        }   
    }
}