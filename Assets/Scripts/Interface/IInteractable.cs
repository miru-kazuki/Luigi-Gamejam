public enum PlayerType { Player1, Player2 }

public interface IInteractable
{
    void Interact(PlayerType player);
}