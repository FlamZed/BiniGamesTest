using UnityEngine;
using Zenject;

namespace Infrastructure.DIContainer.Extensions
{
    public static class ZenjectExtension
    {
        public static void BindService<TService, TRealisation>(this DiContainer diContainer)
            where TRealisation : TService
        {
            diContainer
                .Bind<TService>()
                .To<TRealisation>()
                .AsSingle()
                .NonLazy();
        }
        public static void BindService<TService, TRealisation>(this DiContainer diContainer,
            GameObject gameObject)
            where TRealisation : TService
        {
            diContainer
                .Bind<TService>()
                .To<TRealisation>()
                .FromComponentInNewPrefab(gameObject)
                .AsSingle()
                .NonLazy();
        }
    }
}