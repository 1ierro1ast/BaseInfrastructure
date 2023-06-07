using Codebase.Infrastructure.Services.DataStorage;
using TMPro;
using UnityEngine;
using Codebase.Extensions;
using Zenject;

namespace Codebase.Core.UI.Counters
{
    public class CoinsCounter : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private IGameVariables _gameVariables;

        [Inject]
        private void Construct(IGameVariables gameVariables)
        {
            _gameVariables = gameVariables;
        }
        private void Awake()
        {
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
