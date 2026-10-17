using System;

namespace Core.Misc
{
    public static class EventBus
    {
        // General
        public static Action OnCompletedLevel;
        public static Action OnLoseLevel;

        // Submarine
        public static Action OnStartedTeleporting;
        public static Action OnEndedTeleporting;
   }
}