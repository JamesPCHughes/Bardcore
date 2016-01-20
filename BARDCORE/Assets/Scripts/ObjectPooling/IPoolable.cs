using UnityEngine;

public interface IPoolable {
	bool Spawned {get;}
	IPoolable SpawnAt (Vector3 position, Quaternion rotation);
	void Despawn();
}