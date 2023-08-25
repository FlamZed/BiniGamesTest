using System.Collections;
using UnityEngine;

namespace Infrastructure
{
    public interface ICoroutineRunner
    {
        Coroutine Run(IEnumerator coroutine);
    }
}
