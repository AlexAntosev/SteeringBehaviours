using Assets.Scripts.Models;

namespace Assets.Scripts.Movement.Providers
{
    public interface IAffectiveVelocityProvider
    {
        Creature NearestCreature { get; set; }
    }
}