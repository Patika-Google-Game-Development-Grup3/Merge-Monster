using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = "Melee", menuName = "Scriptable Objects/Characters/Melee", order = 2)]
    public class MeleePropertiesSO : CharacterPropertiesSO
    {
        public ParticleSystem AttackParticle;
    }
}
