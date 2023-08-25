using System;
using Logic.Player;
using Utility;

namespace Infrastructure.Services.Score
{
    public interface IScoreService
    {
        event Action OnWin;
        event Action OnLose;

        void Init(BallsTable config);
        void RegisterPlayer(MergeBehavior configSetter);
        void GetRecommendedBall(out Ball ball);
    }
}
