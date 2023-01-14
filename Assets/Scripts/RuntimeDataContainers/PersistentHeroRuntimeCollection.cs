using App.Hero;
using App.Infrastructure;
using UnityEngine;

namespace App.RuntimeDataContainers
{
    /// <summary>
    /// A runtime list of <see cref="PersistentHero"/> objects that is populated both on clients and server.
    /// </summary>

    [CreateAssetMenu]
    public class PersistentHeroRuntimeCollection: RuntimeCollection<PersistentHero>
    {
        public bool TryGetHero(ulong clientID, out PersistentHero persistentHero)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (clientID == Items[i].OwnerClientId)
                {
                    persistentHero = Items[i];
                    return true;
                }
            }

            persistentHero = null;
            return false;
        }

    }
}