using System;
using System.Collections;
using Entity;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Map.Structures
{
    public class GoldMine : Structure
    {
        [Header("Gold Mine Parameters")] public float miningTime = 3f;
        public float miningTimerElapsed = -0.1f;

        [Header("Loot References")] public Lootable loot;
        public GameObject lootContainer;
        public float lootSpawnRangeX = 1.5f;
        public float lootSpawnRangeY = 1f;

        private Coroutine _spawnCoroutine;

        private static readonly int Spawn = Animator.StringToHash("Spawn");

        private void Update()
        {
            if (Activated)
            {
                if (_spawnCoroutine == null)
                {
                    StartSpawning();
                }

                miningTimerElapsed += Time.deltaTime;

                if (miningTimerElapsed > miningTime)
                {
                    StopSpawning();
                    _sr.sprite = destroyed;
                    Destroyed = true;
                    Activated = false;
                }
            }
            else
            {
                StopSpawning();
            }
        }

        public void StartSpawning()
        {
            _spawnCoroutine = StartCoroutine(SpawnCoroutine());
        }

        public void StopSpawning()
        {
            if (_spawnCoroutine == null) return;

            StopCoroutine(_spawnCoroutine);
            _spawnCoroutine = null;
        }

        private IEnumerator SpawnCoroutine()
        {
            yield return new WaitForSeconds(1f);
            InstantiateLoot();
            StartSpawning();
        }

        public void InstantiateLoot()
        {
            var lootPos = gameObject.transform.position;
            lootPos.x += Random.Range(-lootSpawnRangeX, lootSpawnRangeX);
            lootPos.y -= lootSpawnRangeY;

            var lootInstance = Instantiate(loot, lootPos, Quaternion.identity, lootContainer.transform);
            lootInstance.GetComponent<SpriteRenderer>().sortingLayerName = _sr.sortingLayerName;
            lootInstance.transform.gameObject.layer = gameObject.layer;
            lootInstance.GetComponent<Animator>().SetTrigger(Spawn);
        }
    }
}