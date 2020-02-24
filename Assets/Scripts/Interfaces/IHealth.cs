public interface IHealth
{
    int CurrentHealth { get; }
    int MaxHealth { get; }
    int MinHealth { get; }

    void IncreaseHealth(int amount);

    bool DecreaseHealth(int amount);

    void Reset();
}
