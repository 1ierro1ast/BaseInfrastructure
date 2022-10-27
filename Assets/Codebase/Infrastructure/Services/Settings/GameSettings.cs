using UnityEngine;

namespace Codebase.Infrastructure.Services.Settings
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "GameSettings", order = 51)]
    public partial class GameSettings : ScriptableObject, IService
    {
    }
}