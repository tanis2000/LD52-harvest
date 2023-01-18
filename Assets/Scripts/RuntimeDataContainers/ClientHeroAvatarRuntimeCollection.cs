using App.Hero;
using App.Infrastructure;
using UnityEngine;

namespace App.RuntimeDataContainers
{
    /// <summary>
    /// A runtime list of <see cref="ClientHeroAvatar"/> objects that is populated both on clients and server.
    /// </summary>
    [CreateAssetMenu]
    public class ClientHeroAvatarRuntimeCollection : RuntimeCollection<ClientHeroAvatar>
    {
        
    }
}