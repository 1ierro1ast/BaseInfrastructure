using Codebase.Infrastructure.Services;
using Codebase.Infrastructure.Services.DataStorage;
using TMPro;
using UnityEngine;
using Codebase.Extensions;

namespace Codebase.Core.UI.Counters
{
    public class CoinsCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private IGameVariables _gameVariables;

        private void Awake()
        {
            _gameVariables = AllServices.Container.Single<IGameVariables>();
            _gameVariables.ChangeCoinsCountEvent += GameVariablesOnChangeCoinsCountEvent;

            _text.text = _gameVariables.CoinsCount.AbbreviateNumber();
        }
        

        private void GameVariablesOnChangeCoinsCountEvent(int amount)
        {
            _text.text = _gameVariables.CoinsCount.AbbreviateNumber();
        }

        private void OnDestroy()
        {
            _gameVariables.ChangeCoinsCountEvent -= GameVariablesOnChangeCoinsCountEvent;
        }
    }
}
