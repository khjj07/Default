using System.Collections;
using System.Collections.Generic;

namespace Default
{
    public interface IAbilityStats 
    {
        IEnumerable<AbilityStat> Get();
    }
}