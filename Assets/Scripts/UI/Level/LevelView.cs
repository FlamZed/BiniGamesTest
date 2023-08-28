using Infrastructure.Services.Level;
using Infrastructure.Services.Score;
using UnityEngine;
using Zenject;

namespace UI.Level
{
    public class LevelView : MonoBehaviour
    {
        [Header("Spawn Point")]
        [SerializeField] private Transform _spawnPoint;

        [Header("Popup")]
        [SerializeField] private GameObject _congratulationPopup;
        [SerializeField] private GameObject _losingPopup;

        [Inject] private ISpawnPointService _spawnPointService;
        [Inject] private IGameObserverService _gameObserverService;
        [Inject] private IGameResetService _gameResetService;

        private void Start()
        {
            _spawnPointService.AllowSpawn(_spawnPoint.position);

            _gameObserverService.OnWin += ShowCongratulationPopup;
            _gameObserverService.OnLose += ShowLosingPopup;
        }

        private void OnDestroy()
        {
            _gameObserverService.OnWin -= ShowCongratulationPopup;
            _gameObserverService.OnLose -= ShowLosingPopup;
        }

        public void Restart() =>
            _gameResetService.RestartLevel();

        private void ShowCongratulationPopup() =>
            _congratulationPopup.SetActive(true);

        private void ShowLosingPopup() =>
            _losingPopup.SetActive(true);
    }
}
