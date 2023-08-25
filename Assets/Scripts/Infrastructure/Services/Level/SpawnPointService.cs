using System;
using UnityEngine;

namespace Infrastructure.Services.Level
{
    public class SpawnPointService : ISpawnPointService
    {
        public event Action<Vector3> AllowSpawnPoint;

        public Vector3 GetSpawnPoint => _spawnPoint;

        private Vector3 _spawnPoint;

        public void AllowSpawn(Vector3 point)
        {
            _spawnPoint = point;

            AllowSpawnPoint?.Invoke(point);
        }
    }

    public interface ISpawnPointService
    {
        void AllowSpawn(Vector3 point);
        event Action<Vector3> AllowSpawnPoint;
        Vector3 GetSpawnPoint { get; }
    }
}
