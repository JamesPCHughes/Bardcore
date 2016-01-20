using UnityEngine;
using System.Collections;

public class ButterflySpawner : MonoBehaviour {
    [SerializeField] GameObject [] _butterflyPrefabs;
    [SerializeField] int _maxButterfliesPerType = 20;
    [SerializeField] float _butterflyMaxRadius = 20f;
    [SerializeField] float _butterflyMinRadius = 3f;
    [SerializeField] float _butterflyIntenseRadius = 1f;
    [SerializeField] float _butterflyStartTime;
    [SerializeField] float _butterflySpawnIntervalLower;
    [SerializeField] float _butterflySpawnIntervalUpper;
    PooledObjectFactory<Butterfly>[] _factories;
    IEnumerator _spawnRoutine = null;

    void OnEnable () {
        Events.G.AddListener<DayAnimalsShouldStartEvent>(DayAnimalsShouldStart);
        Events.G.AddListener<DuskStartedEvent>(StopSpawning);
        Events.G.AddListener<SecondDawnStartedEvent>(SecondDawnStarted);
    }

    void OnDisable () {
        Events.G.RemoveListener<DayAnimalsShouldStartEvent>(DayAnimalsShouldStart);
        Events.G.RemoveListener<DuskStartedEvent>(StopSpawning);
        Events.G.RemoveListener<SecondDawnStartedEvent>(SecondDawnStarted);
    }

    void Start () {
        _factories = new PooledObjectFactory<Butterfly>[_butterflyPrefabs.Length];
        for (int i = 0; i < _factories.Length; i++){
            _factories[i] = new PooledObjectFactory<Butterfly>(_butterflyPrefabs[i], _maxButterfliesPerType, transform);
        }
    }

    void DayAnimalsShouldStart (DayAnimalsShouldStartEvent e) {
        StartSpawning();
    }

    void SecondDawnStarted (SecondDawnStartedEvent e) {
        StartSpawning();
    }

    void StartSpawning () {
        if (_spawnRoutine == null) {
            _spawnRoutine = SpawnButterfliesRoutine();
        }
        StartCoroutine(_spawnRoutine);
    }

    void StopSpawning (DuskStartedEvent e) {
        if (_spawnRoutine != null) {
            StopCoroutine(_spawnRoutine);
            _spawnRoutine = null;
        }
    }

    IEnumerator SpawnButterfliesRoutine () {
        while (true) {
            SpawnOneButterfly(_butterflyMaxRadius);
            SpawnOneButterfly(_butterflyIntenseRadius);
            float waitTime = Random.Range(_butterflySpawnIntervalLower, _butterflySpawnIntervalUpper);
            yield return new WaitForSeconds(waitTime);
        }
    }

    void SpawnOneButterfly (float radius) {
        var butterflyPosition = new Vector3(Random.insideUnitCircle.x, 0.1f, Random.insideUnitCircle.y);
        butterflyPosition *= radius;

        if (butterflyPosition.magnitude < _butterflyMinRadius) {
            butterflyPosition.Normalize();
            butterflyPosition *= _butterflyMinRadius;
        }

        SpawnRandomButterflyAtPosRot(butterflyPosition, Quaternion.identity);
    }

    void SpawnRandomButterflyAtPosRot (Vector3 position, Quaternion rotation) {
        _factories[Random.Range(0, _factories.Length)].SpawnAt(position, rotation);
    }
}