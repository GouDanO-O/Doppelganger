namespace GameFrame
{
    public enum EGameState : byte
    {
        None,
        StartLoading,
        Loading,
        EndLoading,
        Playing,
        Pause,
        GameEnd,
    }
}