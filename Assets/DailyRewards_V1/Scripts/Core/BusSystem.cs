using System;

namespace DailyRewards_V1.Scripts.Core
{
    public static class BusSystem
    {
        //Economy
        public static Action OnSetMoneys;
        public static void CallSetMoneys() { OnSetMoneys?.Invoke(); }
    }
}