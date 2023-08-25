using System;
using UnityEngine;

namespace Infrastructure.Services.Input
{
    public interface IInputService
    {
        event Action OnTap;
        event Action OnRelease;
        void GetMousePosition(out Vector3 position);
    }
}
