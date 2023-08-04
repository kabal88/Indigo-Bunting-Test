namespace Models
{
    public class PlayerModel
    {
        public int Level { get; private set; }
        public int Money { get; private set; }
        public int StartNumber { get; private set; } = 2;

        public void SetLevel(int level)
        {
            Level = level;
        }

        public void SetMoney(int score)
        {
            Money = score;
        }

        public void AddMoney(int value)
        {
            Money += value;
        }
    }
}