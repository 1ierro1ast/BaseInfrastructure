using System.Collections;
using UnityEngine;

namespace Codebase.Infrastructure
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
        void StopCoroutine(Coroutine logicLoop);
    }
}