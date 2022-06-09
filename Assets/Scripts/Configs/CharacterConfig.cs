
namespace Assets.Scripts.Configs
{
    public class CharacterConfig
    {
        public int HealthPoints { get; set; }
        public int Damage { get; private set; }

        public CharacterConfig(int HealthPoints, int Damage)
        {
            this.HealthPoints = HealthPoints;
            this.Damage = Damage;
        }
    }
}