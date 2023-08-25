using System.Collections;
using UnityEngine;

namespace Infrastructure
{
    public class CoroutineRunner : MonoBehaviour, ICoroutineRunner
    {
        public Coroutine Run(IEnumerator coroutine) =>
            StartCoroutine(coroutine);
    }
}
