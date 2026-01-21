namespace Default
{


    public class AbilityStat
    {
        public readonly float baseValue;
        public float currentValue;
        public string name;

        public AbilityStat(string name, float baseValue = 0)
        {
            this.name = name;
            this.baseValue = baseValue;
            this.currentValue = baseValue;
        }
        
        public void Set(float value)
        {
            currentValue = value;
        }
        
        public float Get()
        {
            return currentValue;
        }
        
        public void Reset()
        {
            currentValue = baseValue;
        }

        
    }
}