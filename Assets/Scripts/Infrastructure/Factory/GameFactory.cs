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
        [Inject] private IGameObserverService _gameObserverService;
        [Inject] private IInputService _inputService;

        private readonly IAssets _assets;

        public GameFactory(IAssets assetProvider) =>
            _assets = assetProvider;

        public void CreateHero(Ball ball, Vector3 at, bool isPlayer)
        {
            var player = _assets.Instantiate(AssetPath.HeroPath, at);

            RegisterObject(ball, isPlayer, player);
        }

        private void RegisterObject(Ball ball, bool isPlayer, GameObject player)
        {
            if (player.TryGetComponent(out PlayerView configSetter))
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
                _gameObserverService.RegisterPlayer(merge);
            else
                throw new ArgumentException("PlayerConfigSetter not found");
        }
    }
}
