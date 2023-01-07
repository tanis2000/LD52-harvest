using App.Generation;
using UnityEngine;

namespace App.Rooms.Steps
{
    public class RotateLevel : GenerationStep
    {
        public float Angle = -45.0f;
    
        public override void Clear()
        {
            base.Clear();
            transform.rotation = Quaternion.identity;
        }
        public override void Generate(int seed)
        {
            transform.rotation = Quaternion.Euler(0, Angle, 0);
        }
    }
}