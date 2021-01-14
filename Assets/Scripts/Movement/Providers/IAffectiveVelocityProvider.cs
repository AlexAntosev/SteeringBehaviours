using Models;

namespace Movement.Providers
{
    public interface IAffectiveVelocityProvider
    {
        Creature NearestCreature { get; set; }
    }
}