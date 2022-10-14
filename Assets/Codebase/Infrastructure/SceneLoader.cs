using System;
using System.Collections;
using Codebase.Core.UI.Counters;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Codebase.Infrastructure
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void LoadScene(string name, bool validateSceneName = true, Action onLoaded = null,
            ProgressBar progressView = null)
        {
            _coroutineRunner.StartCoroutine(LoadSceneCoroutine(name, validateSceneName, onLoaded, progressView));
        }

        private IEnumerator LoadSceneCoroutine(string name, bool validateSceneName, Action onLoaded = null,
            ProgressBar progressView = null)
        {
            if (validateSceneName && SceneManager.GetActiveScene().name == name)
            {
                onLoaded?.Invoke();
                yield break;
            }

            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(name);

            loadOperation.allowSceneActivation = false;
            progressView?.SetProgress(loadOperation.progress);
            loadOperation.allowSceneActivation = true;
            while (!loadOperation.isDone)
            {
                progressView?.SetProgress(loadOperation.progress);
                yield return null;
            }

            onLoaded?.Invoke();
        }
    }
}