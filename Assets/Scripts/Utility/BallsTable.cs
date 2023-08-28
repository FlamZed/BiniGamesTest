using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utility
{
    [CreateAssetMenu(fileName = "BallsTable", menuName = "Balls Table", order = 51)]
    public class BallsTable : ScriptableObject
    {
        [SerializeField] private List<Ball> _balls;

        public bool IsMaxValue(int index) =>
            index >= _balls.Count - 1;

        public void GetRandomBall(int maxIndex, out Ball ball)
        {
            var randomIndex = Random.Range(0, maxIndex);
            ball = _balls[randomIndex];
        }

        public Ball GetIncrementedBall(int currentBallIndex)
        {
            if (currentBallIndex < 0)
                throw new ArgumentOutOfRangeException();

            if (currentBallIndex >= _balls.Count)
                return _balls.Last();

            return _balls[currentBallIndex];
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            for (int i = 0; i < _balls.Count; i++)
                _balls[i].SetName(Math.Pow(2, 1 + i));
        }
#endif
    }

    [Serializable]
    public struct Ball
    {
        [SerializeField][HideInInspector] private string _name;

        [SerializeField] private int _index;
        [SerializeField] private int _increment;

        [SerializeField] private Sprite _sprite;
        [SerializeField] private Gradient _trialColor;

        public int Index => _index;
        public int Increment => _increment;
        public Sprite Sprite => _sprite;
        public Gradient Gradient => _trialColor;

        public void SetName(double pow)
        {
            _name = "Ball " + pow;
        }
    }
}
