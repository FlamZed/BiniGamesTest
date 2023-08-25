using UnityEngine;
using Utility;

namespace Infrastructure.Factory
{
    public interface IGameFactory
    {
        void CreateHero(Ball ball, Vector3 at, bool isPlayer);
    }
}
