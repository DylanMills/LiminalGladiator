public interface IDamageable 
{
    public void Damage(int damage, float knockback);//This is our damage interface to use to apply to several different objects in a list.
    public void Damage(int damage)
    {
        Damage(damage, 10);
    }
}
