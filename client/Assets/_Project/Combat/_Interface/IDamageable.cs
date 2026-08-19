using UnityEngine;

namespace TopdownRPG.Combat {
    public interface IDamageable {
        GameObject GameObject { get; }
        void TakeDamage(int damage);
    }
}