using System.Windows.Forms;

namespace ToDoApp.Commands
{
    public class DeleteTaskCommand : ICommand
    {
        private readonly CheckedListBox _listBox;
        private readonly int _index;
        private string _task;

        public DeleteTaskCommand(CheckedListBox listBox, int index)
        {
            _listBox = listBox;
            _index = index;
        }

        public void Execute()
        {
            _task = (string)_listBox.Items[_index];
            _listBox.Items.RemoveAt(_index);
        }

        public void Undo() => _listBox.Items.Insert(_index, _task);
    }
}