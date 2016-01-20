using UnityEngine;
using System.Collections;
namespace CompleteProject {
	
public class EnemyManger : MonoBehaviour {

	[SerializeField] GameObject enemy;
	[SerializeField] float spawnTime = .25f;
	[SerializeField] Transform[] spawnPoints;
	PooledObjectFactory<EnemyMovement> _factory;
		[SerializeField] int _maxEnemies;

	bool _isSpawning = true;

	void Start () {
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
		_factory = new PooledObjectFactory<EnemyMovement>(enemy, _maxEnemies, transform);
		StartCoroutine (SpawnRoutine ());
	}

	IEnumerator SpawnRoutine () {
		while (_isSpawning) {
			int spawnPointIndex = Random.Range (0, spawnPoints.Length);
			_factory.SpawnAt(spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
			yield return new WaitForSeconds (0.25f);
		}
	}

	}
}