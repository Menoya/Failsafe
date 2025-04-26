namespace Failsafe.Scripts.Character.Components.AnimatorComponents
{
    public class CharacterAnimatorParameters
    {
        public readonly float StandHeightValue = 2f;
        public readonly float CrouchHeightValue = 1f;
        public readonly float CrawlHeightValue = 0f;
        
        public readonly int MoveHeightHash = UnityEngine.Animator.StringToHash("MoveHeight");
        public readonly int ForwardInputHash = UnityEngine.Animator.StringToHash("ForwardInput");
        public readonly int SideInputHash = UnityEngine.Animator.StringToHash("SideInput");
    }
}