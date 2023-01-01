using System.Collections;
using System.Collections.Generic;
using BeanFramework.Define;

namespace BeanFramework.BehaviourTree
{
    /// <summary>
    /// 행동트리에 사용될 최상위 노드
    /// </summary>
    public class RootNode : NodeBase
    {
        DecideResult curResult = new DecideResult();

        public override DecideResult Decide(DecideResult result)
        {
            // 루트노드는 하나의 자식만 가지므로 0번 인덱스에 접근한다.
            // 자신이 가지고 있는 result를 이용해 판정을 한다.
            curResult = children[0].Decide(curResult);

            return null;
        }
    }
}