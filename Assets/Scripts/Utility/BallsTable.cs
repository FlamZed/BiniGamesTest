using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
            var randomIndex = UnityEngine.Random.Range(0, maxIndex);
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
            {
                var increment = 2;

                if (i > 0)
                    increment = _balls[i - 1].Increment * 2;

                _balls[i] = new Ball(
                    $"Ball index: {i}, increment: {increment}",
                    i,
                    increment,
                    _balls[i].Sprite,
                    _balls[i].Gradient);
            }
        }
#endif
    }

    [Serializable]
    public struct Ball
    {
        [SerializeField][HideInInspector] private string _name;

        private int _index;
        private int _increment;

        [SerializeField] private Sprite _sprite;
        [SerializeField] private Gradient _trialColor;

        public int Index => _index;
        public int Increment => _increment;
        public Sprite Sprite => _sprite;
        public Gradient Gradient => _trialColor;

        public Ball(string name, int index ,int increment, Sprite sprite, Gradient trialColor)
        {
            _name = name;
            _index = index;
            _increment = increment;
            _sprite = sprite;
            _trialColor = trialColor;
        }
    }
}
