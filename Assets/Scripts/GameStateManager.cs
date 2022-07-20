public class GameStateManager
{
    private static GameStateManager _instance;

    public static GameStateManager Instance
    {
        get { return _instance ??= new GameStateManager(); }
    }

    public GameState CurrentGameState { get; private set; }

    public delegate void GameStageChangeHandler(GameState newGameState);

    public event GameStageChangeHandler OnGameStateChanged;

    public GameStateManager()
    {
    }

    public void SetState(GameState newGameState)
    {
        if (newGameState == CurrentGameState)
            return;

        CurrentGameState = newGameState;
        OnGameStateChanged?.Invoke(newGameState);
    }
}