using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FinishMenuPrefab.Scripts.GameFinishPrefab
{
    /// <summary>
    /// Handles the spawning and management of particle effects at predefined positions.
    /// </summary>
    public class ParticleSpawner : MonoBehaviour
    {
        [Tooltip("Prefab for the particle to spawn.")]
        [SerializeField] private GameObject particlePrefab;

        [Tooltip("List of spawn positions for particles.")]
        [SerializeField] private List<Vector2> particleSpawnPoints;

        [Tooltip("Holds references to instantiated particle clones.")]
        [SerializeField] private List<GameObject> particleSpawnClones = new();

        /// <summary>
        /// Public getter for the list of particle clones.
        /// </summary>
        public List<GameObject> ParticleSpawnClones => particleSpawnClones;

        /// <summary>
        /// Initializes the particle spawner by creating particle instances.
        /// </summary>
        private void Awake() => InstantiateParticle();

        /// <summary>
        /// Instantiates particles at specified spawn points and deactivates them.
        /// </summary>
        private void InstantiateParticle()
        {
            // Ensure the list of particle clones matches the size of the spawn points.
            particleSpawnClones.Clear();
            foreach (var particle in particleSpawnPoints.Select(t => Instantiate(particlePrefab, t, Quaternion.identity)))
            {
                particle.SetActive(false); // Deactivates the particle.
                particleSpawnClones.Add(particle); // Adds the particle to the clones list.
            }
        }

        /// <summary>
        /// Draws Gizmos in the Scene view to visualize particle spawn points.
        /// </summary>
        private void OnDrawGizmos()
        {
            if (particleSpawnPoints == null || particleSpawnPoints.Count == 0) return;

            Gizmos.color = Color.red;
            foreach (var point in particleSpawnPoints)
            {
                Gizmos.DrawSphere(point, 0.1f); // Draws a small red sphere at each spawn point.
            }
        }
    }
}
