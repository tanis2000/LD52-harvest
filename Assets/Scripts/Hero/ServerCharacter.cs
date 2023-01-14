using App.Configuration;
using Unity.Netcode;
using UnityEngine;

namespace App.Hero
{
    /// <summary>
    /// Contains all NetworkVariables, RPCs and server-side logic of a character.
    /// This class was separated in two to keep client and server context self contained. This way you don't have to continuously ask yourself if code is running client or server side.
    /// </summary>
    public class ServerCharacter : NetworkBehaviour
    {
        public CharacterClass CharacterClass;
    }
}