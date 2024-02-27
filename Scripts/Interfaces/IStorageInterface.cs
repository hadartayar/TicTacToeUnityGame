/*
 * Interface of IStorage complies to the Strategy Design Pattern -
 * <One Option> is PlayerPref Storage.
 * <Second Option> is In memory (application memory) with Json file.
 */

public interface IStorageInterface
{
    /// <summary>
    /// Saves the game state managers object (according the storage type choosen)
    /// </summary>
    /// <param name="game">current game state relevant parameters</param>

    void SaveGame(GameData game);

    /// <summary>
    /// Loads the game from the application memory (according the storage type choosen)
    /// </summary>
    /// <returns>reloaded game state </returns>
    GameData LoadGame();
}
