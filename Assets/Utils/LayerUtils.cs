using UnityEngine;

namespace Utils
{
    public static class LayerUtils
    {
        public static bool IsMatch(LayerMask mask, int layer)
        {
            return (mask.value & (1 << layer)) != 0;
        }
    }
}
