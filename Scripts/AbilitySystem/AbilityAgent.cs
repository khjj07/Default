using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Default
{
    public class AbilityAgent : MonoBehaviour
    {
        [SerializeField] PlayerInput playerInput;

        private SortedSet<AbilityStat> stats = new SortedSet<AbilityStat>();
        public List<AbilityEffectBase> effects = new List<AbilityEffectBase>();

        public void AddStat(AbilityStat stat)
        {
            stats.Add(stat);
        }

        public void RemoveStat(AbilityStat stat)
        {
            stats.Remove(stat);
        }

        public void AddEffect(AbilityEffectBase effect)
        {
            DeactivateAllEffect();
            effects.Add(effect);
            playerInput.onActionTriggered += effect.OnActionTriggered;
            ReorderEffect();
            ResetAllStats();
            ActivateAllEffect();
        }

        public void RemoveEffect(AbilityEffectBase effect)
        {
            DeactivateAllEffect();
            effects.Remove(effect);
            playerInput.onActionTriggered -= effect.OnActionTriggered;
            ResetAllStats();
            ActivateAllEffect();
        }

        public AbilityStat GetAgentStat(string name)
        {
            return stats.FirstOrDefault(x => x.name == name);
        }

        public SortedSet<AbilityStat> GetAgentStats()
        {
            return stats;
        }

        public AbilityEffectBase GetAgentEffect(string name)
        {
            return effects.Find(x => x.name == name);
        }

        public List<AbilityEffectBase> GetAgentEffects(string tag)
        {
            return effects.FindAll(x => x.tag.Contains(tag));
        }

        private void ReorderEffect()
        {
            effects.Sort((a, b) => a.order - b.order);
        }

        public void ResetAllStats()
        {
            foreach (var stat in stats)
            {
                stat.Reset();
            }
        }

        public void ActivateAllEffect()
        {
            foreach (var effect in effects)
            {
                effect.Activate();
            }
        }

        public void DeactivateAllEffect()
        {
            foreach (var effect in effects)
            {
                effect.Deactivate();
            }
        }
    }
}