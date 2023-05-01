using UnityEngine;

namespace PorfirioPartida.Delibeery.Common
{
    public static class AnimatorConstants
    {
        public static readonly int TriggerDie = Animator.StringToHash("Die");
        public static readonly int TriggerAnnoy = Animator.StringToHash("Annoy");

        public static readonly int IsDraining = Animator.StringToHash("IsDraining");
        public static readonly int IsFull = Animator.StringToHash("IsFull");
        // public static readonly int FullPct = Animator.StringToHash("FullPct");
    }
}