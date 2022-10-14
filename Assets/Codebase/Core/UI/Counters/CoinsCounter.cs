using Codebase.Infrastructure.Services;
using Codebase.Infrastructure.Services.DataStorage;
using Codebase.Utils;
using TMPro;
using UnityEngine;

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

            _text.text = AbbrevationUtility.AbbreviateNumber(_gameVariables.CoinsCount);
        }
        

        private void GameVariablesOnChangeCoinsCountEvent(int amount)
        {
            _text.text = AbbrevationUtility.AbbreviateNumber(amount);
        }

        private void OnDestroy()
        {
            _gameVariables.ChangeCoinsCountEvent -= GameVariablesOnChangeCoinsCountEvent;
        }
    }
}
