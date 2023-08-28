using System;
using UnityEngine;

namespace Infrastructure.Services.Level
{
    public interface ISpawnPointService
    {
        void AllowSpawn(Vector3 point);
        event Action<Vector3> AllowSpawnPoint;
        Vector3 GetSpawnPoint { get; }
    }
}