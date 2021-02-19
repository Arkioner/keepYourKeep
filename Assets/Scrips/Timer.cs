using System;
using UnityEngine;

namespace Scrips
{
    public class Timer
    {
        private const int ZeroSeconds = 0;
        private float _timer;
        private readonly float _timeSpan;

        public Timer(float desiredTimeSpan, Boolean delayed = true)
        {
            if (desiredTimeSpan <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(desiredTimeSpan),
                    desiredTimeSpan,
                    "Value must be strictly positive"
                );
            }

            if (delayed)
            {
                _timer = desiredTimeSpan;
            }
            _timeSpan = desiredTimeSpan;
        }

        public Boolean IsTime()
        {
            _timer -= Time.deltaTime;
            if (_timer <= ZeroSeconds)
            {
                _timer += _timeSpan;
                return true;
            }

            return false;
        }
    }
}