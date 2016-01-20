using UnityEngine;

public class PooledObjectFactory<T> where T: MonoBehaviour, IPoolable {

	readonly int _poolSize;
	readonly T[] _pool;
	readonly Transform _container;
	int _lookUpIndex;

	public PooledObjectFactory(GameObject prefab, int poolSize, Transform container = null){
		_poolSize = poolSize;
		_container = container;
		_pool = new T[_poolSize];
		_lookUpIndex = 0;
		Prespawn(prefab);
	}

	void Prespawn (GameObject prefab){
		for(int i = 0; i < _poolSize; i++){
			GameObject newObject = Object.Instantiate<GameObject>(prefab);
			_pool[i] = newObject.GetComponent<T>();
			newObject.transform.SetParent(_container, true);
			newObject.SetActive(false);
		}
	}

	public T SpawnAt (Vector3 position) {
		return SpawnAt (position, Quaternion.identity);
	}

	public T SpawnAt (Vector3 position, Quaternion rotation) {
		int amountChecked = 0;
		while (amountChecked < _poolSize){
			_lookUpIndex = _lookUpIndex % _poolSize;
			T possibleObject = _pool[_lookUpIndex];
			if (!possibleObject.Spawned){
				_lookUpIndex++;
				var spawned = possibleObject.SpawnAt(position, rotation) as T;
#if UNITY_ENGINE
				if (spawned == null) {
					Debug.LogError("Incorrect SpawnAt implementation detected for "+typeof(T));
				}
#endif
				return spawned;
			}
			_lookUpIndex++;
			amountChecked++;
		}
		return default(T);
	}
}