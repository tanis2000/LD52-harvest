using App.Hero;
using UnityEngine;

namespace App.Configuration
{
    [CreateAssetMenu(menuName = "GameData/CharacterClass", order = 0)]
    public class CharacterClass : ScriptableObject
    {
        [Tooltip("which character this data represents")]
        public CharacterType CharacterType;
        
        [Tooltip("Set to true if this represents an NPC, as opposed to a player.")]
        public bool IsNpc;

    }
}