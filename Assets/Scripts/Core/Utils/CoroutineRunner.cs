using Core.Misc;

namespace Core.Utils
{
    /// <summary>
    /// Made for coroutines who wants to 'yield', but they are not MonoBehaviour
    /// kind of work-around
    /// </summary>
    public class CoroutineRunner : MonoSingleton<CoroutineRunner>
    {
        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}