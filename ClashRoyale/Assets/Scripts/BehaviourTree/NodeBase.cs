using System.Collections.Generic;
using BeanFramework.Define;

namespace BeanFramework.BehaviourTree
{
    /// <summary>
    /// 판정 결과
    /// </summary>
    public class DecideResult
    {
        public EActionState State { get; set; } = EActionState.Fail;
        public ActionNode Action { get; set; } = null;
    }

    /// <summary>
    /// 행동트리에 사용될 노드의 기본 클래스.
    /// </summary>
    public abstract class NodeBase
    {
        /// <summary> 부모 노드. 부모 노드가 null이면 최상위 노드. </summary>
        protected NodeBase parent = null;
        /// <summary> 자식 노드를 가지고 있는 리스트 </summary>
        protected List<NodeBase> children = new List<NodeBase>();

        /// <summary> 판정. </summary>
        public abstract DecideResult Decide(DecideResult result);
    }
}