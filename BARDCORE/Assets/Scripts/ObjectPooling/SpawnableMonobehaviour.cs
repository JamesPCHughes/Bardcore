using UnityEngine;

// NOTE(JULIAN): If you inherit from this, you should also implement IPoolable. Making SpawnableMonobehaviour
// inherit from IPoolable allows the PooledObjectFactory to bypass the most specific SpawnAt method in favor
// of the one here, which is intended as a base.
public abstract class SpawnableMonobehaviour : MonoBehaviour {
    // NOTE(JULIAN): This should be an auto-property, but Unity serialization can't deal with that in an abstract class :(
    bool _spawned;
    public bool Spawned { get { return _spawned; } }

    protected SpawnableMonobehaviour SpawnAt (Vector3 position, Quaternion rotation) {
        gameObject.SetActive(true);
        _spawned = true;
        transform.position = position;
        transform.rotation = rotation;
        return this;
    }

    public virtual void Despawn () {
        gameObject.SetActive(false);
        _spawned = false;
    }
}