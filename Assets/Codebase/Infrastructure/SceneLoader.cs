using Codebase.Core.UI.Counters;
using System;
using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Codebase.Infrastructure
{
    public class SceneLoader
    {
        public void LoadScene(string name, bool validateSceneName = true, Action onLoaded = null, ProgressBar progressView = null)
        {
            MainThreadDispatcher.StartUpdateMicroCoroutine(LoadSceneCoroutine(name, validateSceneName, onLoaded, progressView));
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
            SetProgress(progressView, loadOperation);
            loadOperation.allowSceneActivation = true;

            while (!loadOperation.isDone)
            {
                SetProgress(progressView, loadOperation);
                yield return null;
            }

            onLoaded?.Invoke();
        }

        private void SetProgress(ProgressBar progressView, AsyncOperation loadOperation)
        {
            if (progressView != null)
                progressView.SetProgress(loadOperation.progress);
        }
    }
}