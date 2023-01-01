namespace BeanFramework.Define
{
    /// <summary> 캔버스의 종류 </summary>
    public enum ECanvasTag
    {
        Cached, // 추후에 다시 사용될 것을 염두하여 캐싱한 UI가 있는 캔버스.
        Common, // 일반적으로 보여지는 캔버스.
    }

    /// <summary> UI를 어떻게 보여지게 할지 설정 </summary>
    public enum EUIType
    {
        FullScreen, // UI가 보여질때 하위의 모든 UI의 Active를 끄고나서 보여진다.
        Window, // UI가 보여질때 하위의 UI 위에 보여진다.
    }

    /// <summary> 행동트리의 액션 상태 </summary>
    public enum EActionState
    {
        Success,
        Fail,
        Running,
    }
}