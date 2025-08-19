using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monogameShooter
{
    internal class GameManager
    {
        public int score;

        private int frame;
        private int difficultyIncreaseInterval;
        private int difficultyScoreMult;
        public int difficulty
        {
            get
            {
                return 1 + (frame / difficultyIncreaseInterval);
            }
        }
        private int scoreIncrease
        {
            get
            {
                return difficulty * difficultyScoreMult;
            }
        }

        public GameManager(int difficultyIncreaseInterval, int difficultyScoreMult)
        {
            this.difficultyIncreaseInterval = difficultyIncreaseInterval;
            this.difficultyScoreMult = difficultyScoreMult;
            this.frame = 0;
            this.score = 0;
        }

        public void update(BallSpawner ballSpawner)
        {
            this.frame += 1;
            this.score += scoreIncrease;
            ballSpawner.ballsAlive = difficulty;
        }

    }
}
