using System;
using Infrastructure.AssetManagement;
using Infrastructure.Services.Input;
using Infrastructure.Services.Score;
using Logic.Player;
using UnityEngine;
using Utility;
using Zenject;

namespace Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        [Inject] private IScoreService _scoreService;
        [Inject] private IInputService _inputService;

        private IAssets _assets;

        public GameFactory(IAssets assetProvider) =>
            _assets = assetProvider;

        public GameObject CreateHero(GameObject go) =>
            InstantiateRegistered(AssetPath.HeroPath, go.transform.position);

        public void CreateHero(Ball ball, Vector3 at) =>
            CreateHero(ball, at, true);

        public void CreateHero(Ball ball, Vector3 at, bool isPlayer)
        {
            var player = _assets.Instantiate(AssetPath.HeroPath, at);

            if (player.TryGetComponent(out PlayerConfigSetter configSetter))
                configSetter.Init(ball, isPlayer);
            else
                throw new ArgumentException("PlayerConfigSetter not found");

            if (player.TryGetComponent(out PlayerImpulse impulse))
                impulse.Init(_inputService);
            else
                throw new ArgumentException("PlayerConfigSetter not found");

            if (player.TryGetComponent(out LineSetter lineSetter))
                lineSetter.Init(_inputService);
            else
                throw new ArgumentException("PlayerConfigSetter not found");

            if (player.TryGetComponent(out MergeBehavior merge))
                _scoreService.RegisterPlayer(merge);
            else
                throw new ArgumentException("PlayerConfigSetter not found");
        }

        private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject gameObject = _assets.Instantiate(prefabPath);
            return gameObject;
        }

        private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
        {
            GameObject gameObject = _assets.Instantiate(prefabPath, at);
            return gameObject;
        }
    }
}
