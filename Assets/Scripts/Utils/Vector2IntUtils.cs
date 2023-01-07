using System.Collections.Generic;
using UnityEngine;

namespace App.Utils
{
    public static class Vector2IntUtils
    {
        public static List<Vector2Int> Directions4()
        {
            return new List<Vector2Int>()
            {
                new Vector2Int(1, 0),
                new Vector2Int(-1, 0),
                new Vector2Int(0, 1),
                new Vector2Int(0, -1),
            };
        }
    
        public static List<Vector2Int> Directions8()
        {
            return new List<Vector2Int>()
            {
                new Vector2Int(1, 0),
                new Vector2Int(1, 1),
                new Vector2Int(1, -1),
                new Vector2Int(-1, 0),
                new Vector2Int(-1, 1),
                new Vector2Int(-1, -1),
                new Vector2Int(0, 1),
                new Vector2Int(0, -1),
            };
        }

    }
}