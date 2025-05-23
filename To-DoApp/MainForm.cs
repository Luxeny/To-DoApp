using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Input;
using ToDoApp.Commands;

namespace ToDoApp
{
    public class MainForm : Form
    {
        private readonly Stack<ICommand> _undoStack = new Stack<ICommand>();
        private readonly Stack<ICommand> _redoStack = new Stack<ICommand>();

        private TextBox _txtTask;
        private Button _btnAdd;
        private Button _btnDelete;
        private Button _btnUndo;
        private Button _btnRedo;
        private CheckedListBox _lstTasks;

        public MainForm()
        {
            InitializeUI();
            SetupEvents();
        }

        private void InitializeUI()
        {
            // Настройка формы
            this.Text = "ToDoApp (Command Pattern)";
            this.ClientSize = new System.Drawing.Size(500, 400);
            this.KeyPreview = true;

            // Поле ввода задачи
            _txtTask = new TextBox
            {
                Location = new System.Drawing.Point(20, 20),
                Width = 300
            };

            // Кнопки
            _btnAdd = new Button { Text = "Добавить", Location = new System.Drawing.Point(330, 20) };
            _btnDelete = new Button { Text = "Удалить", Location = new System.Drawing.Point(20, 320) };
            _btnUndo = new Button { Text = "Отменить (Ctrl+Z)", Location = new System.Drawing.Point(180, 320) };
            _btnRedo = new Button { Text = "Повторить (Ctrl+Y)", Location = new System.Drawing.Point(320, 320) };

            // Список задач
            _lstTasks = new CheckedListBox
            {
                Location = new System.Drawing.Point(20, 60),
                Size = new System.Drawing.Size(450, 250)
            };

            // Добавление элементов на форму
            this.Controls.AddRange(new Control[] { _txtTask, _btnAdd, _lstTasks, _btnDelete, _btnUndo, _btnRedo });
        }

        private void SetupEvents()
        {
            _btnAdd.Click += (s, e) => AddTask();
            _btnDelete.Click += (s, e) => DeleteTask();
            _btnUndo.Click += (s, e) => Undo();
            _btnRedo.Click += (s, e) => Redo();

            // Горячие клавиши
            this.KeyDown += (s, e) =>
            {
                if (e.Control && e.KeyCode == Keys.Z) Undo();
                if (e.Control && e.KeyCode == Keys.Y) Redo();
            };
        }

        private void AddTask()
        {
            if (!string.IsNullOrWhiteSpace(_txtTask.Text))
            {
                var cmd = new AddTaskCommand(_lstTasks, _txtTask.Text);
                cmd.Execute();
                _undoStack.Push(cmd);
                _redoStack.Clear();
                _txtTask.Clear();
            }
        }

        private void DeleteTask()
        {
            if (_lstTasks.SelectedIndex != -1)
            {
                var cmd = new DeleteTaskCommand(_lstTasks, _lstTasks.SelectedIndex);
                cmd.Execute();
                _undoStack.Push(cmd);
                _redoStack.Clear();
            }
        }

        private void Undo()
        {
            if (_undoStack.Count > 0)
            {
                var cmd = _undoStack.Pop();
                cmd.Undo();
                _redoStack.Push(cmd);
            }
        }

        private void Redo()
        {
            if (_redoStack.Count > 0)
            {
                var cmd = _redoStack.Pop();
                cmd.Execute();
                _undoStack.Push(cmd);
            }
        }
    }
}