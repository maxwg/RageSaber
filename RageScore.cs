using System;

namespace RageSaber
{
    public class RageScore
    {
        public static RageScore instance;
        public float curScore;
        public int hitCount;

        public RageScore()
        {
            curScore = 0;
            hitCount = 0;
        }

        public static float calculateFromSpeed(float speed)
        {
            return Math.Min(speed, 40f)/40*100;//(1 - (1 / (1 + speed / 20)) + 0.2f) * 100;
        }
    }
}