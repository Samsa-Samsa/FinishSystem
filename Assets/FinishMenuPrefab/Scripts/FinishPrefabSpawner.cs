using System.Collections.Generic;
using UnityEngine;

namespace FinishMenuPrefab.Scripts.GameFinishPrefab
{
    public class FinishPrefabSpawner : MonoBehaviour
    {
        [Tooltip("List of prefabs to be spawned. Select the appropriate prefab by index.")]
        [SerializeField] private List<GameObject> prefabs = new(); // Holds prefabs to be spawned, configurable in the Inspector.

        [Tooltip("The position where the selected prefab will spawn.")]
        [SerializeField] private Vector2 startPos; // Spawn position for the prefab.

        [Tooltip("Index to select which prefab to spawn from the list.")]
        [SerializeField] private int index; // Index to determine which prefab to spawn.
        public GameObject Prefab { get; private set; } // Property to access the instantiated prefab.

        private void Awake() =>  InitializePrefabs(); // Initializes the prefab when the object awakes.
        private void InitializePrefabs()
        {
            // Instantiates the selected prefab at the specified position, with no rotation, and under the same parent as this object.
            Prefab = Instantiate(prefabs[index], startPos, Quaternion.identity, transform.parent);
            Prefab.SetActive(false); // Deactivates the prefab after instantiation.
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red; // Sets the Gizmo color to red.
            Gizmos.DrawSphere(startPos, 0.1f); // Draws a small sphere at the spawn position to visualize it in the Scene view.
        }
    }
}