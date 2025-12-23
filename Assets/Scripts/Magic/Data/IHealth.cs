public interface IHealth 
{
    public float Value { get; }
    public void TakeDamage(float damage);
    public void Heal(float heal);
}
