namespace Codebase.Infrastructure.Services.DataStorage
{
    public class TemporaryLevelVariables : ITemporaryLevelVariables
    {
        private bool _isWin;

        public bool IsWin
        {
            get => _isWin; 
            set => _isWin = value;
        }

        public void ClearData()
        {
            _isWin = false;
        }
    }
}