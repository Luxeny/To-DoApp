using System.Windows.Forms;

namespace ToDoApp.Commands
{
    public class AddTaskCommand : ICommand
    {
        private readonly CheckedListBox _listBox;
        private readonly string _task;

        public AddTaskCommand(CheckedListBox listBox, string task)
        {
            _listBox = listBox;
            _task = task;
        }

        public void Execute() => _listBox.Items.Add(_task);
        public void Undo() => _listBox.Items.Remove(_task);
    }
}