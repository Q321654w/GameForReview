using System;
using GameUpdate;
using Ranges;

namespace Common
{
    public class RandomLoopTimer : IGameUpdate, ICleanUp
    {
        public event Action TimIsUp;
        public event Action<IGameUpdate> UpdateRemoveRequested;

        private FloatRange _intervalRange;
        private float _currentInterval;

        private float _passedTime;
        private bool _isStopped = true;

        public RandomLoopTimer(FloatRange intervalRange)
        {
            _intervalRange = intervalRange;
            _currentInterval = _intervalRange.GetRandomValue();
        }

        public void Resume()
        {
            _isStopped = false;
        }

        public void Stop()
        {
            _isStopped = true;
        }

        public void GameUpdate(float deltaTime)
        {
            if (_isStopped)
                return;

            _passedTime += deltaTime;

            if (_passedTime < _currentInterval)
                return;

            Reset();
        }

        private void Reset()
        {
            _passedTime = 0;
            _currentInterval = _intervalRange.GetRandomValue();
            TimIsUp?.Invoke();
        }

        public void CleanUp()
        {
            UpdateRemoveRequested?.Invoke(this);
        }
    }
}