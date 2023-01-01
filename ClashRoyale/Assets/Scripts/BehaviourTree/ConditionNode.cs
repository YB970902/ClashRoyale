using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeanFramework.BehaviourTree
{
    /// <summary>
    /// 행동트리에 사용될 조건노드.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ConditionNode<T> : NodeBase
    {
        /// <summary> 조건의 타입 </summary>
        public enum EConditionType
        {
            IfElse, // if else
            Greater, // 초과
            GreaterOrEqual, // 이상
            Less, // 미만
            LessOrEqual, // 이하
            Equal, // 같다
            NotEqual, // 다르다
        }

        public class BehaviourCondition
        {

        }

        /// <summary> 조건이 여러개인경우 </summary>
        public List<System.Predicate<T>> Conditions { get; set; } = new List<System.Predicate<T>>();

        public System.Predicate<T> Condition { get; set; }

        public override DecideResult Decide(DecideResult result)
        {
            return null;
        }
    }
}