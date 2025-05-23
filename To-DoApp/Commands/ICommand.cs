namespace ToDoApp.Commands
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
}