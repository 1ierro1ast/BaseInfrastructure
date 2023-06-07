using UnityEngine;
using Zenject;

namespace Codebase.Infrastructure.Installers
{
    [CreateAssetMenu(fileName = "SettingsInstaller", menuName = "Installers/SettingsInstaller")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        public override void InstallBindings()
        {
        }
    }
}