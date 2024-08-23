using System;
using System.Collections.Generic;

namespace ToDoListApp
{
    public class Task
    {
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }

        public Task(string description, DateTime? dueDate = null)
        {
            Description = description ?? throw new ArgumentNullException(nameof(description));
            DueDate = dueDate;
            IsCompleted = false;
        }

        public override string ToString()
        {
            string dueDateString = DueDate.HasValue ? DueDate.Value.ToString("d") : "No due date";
            return $"{(IsCompleted ? "[X]" : "[ ]")} {Description} (Due: {dueDateString})";
        }
    }

    public class TaskManager
    {
        public List<Task> Tasks { get; private set; } = new List<Task>();

        // Método para agregar una tarea a la lista
        public void AddTask(string description, DateTime? dueDate = null)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException("Task description cannot be empty.");
            }

            Tasks.Add(new Task(description, dueDate));
        }

        // Método para marcar una tarea como completada
        public void MarkTaskAsCompleted(int taskIndex)
        {
            if (taskIndex < 0 || taskIndex >= Tasks.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(taskIndex), "Invalid task index.");
            }

            Tasks[taskIndex].IsCompleted = true;
        }

        // Método para eliminar una tarea de la lista
        public void RemoveTask(int taskIndex)
        {
            if (taskIndex < 0 || taskIndex >= Tasks.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(taskIndex), "Invalid task index.");
            }

            Tasks.RemoveAt(taskIndex);
        }

        // Método para listar todas las tareas
        public List<string> ListTasks()
        {
            List<string> taskDescriptions = new List<string>();

            for (int i = 0; i < Tasks.Count; i++)
            {
                taskDescriptions.Add($"{i + 1}. {Tasks[i]}");
            }

            return taskDescriptions;
        }
    }

    class Program
    {
        static TaskManager taskManager = new TaskManager();

        static void Main(string[] args)
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\nTask Manager:");
                Console.WriteLine("1. Add Task");
                Console.WriteLine("2. List Tasks");
                Console.WriteLine("3. Mark Task as Completed");
                Console.WriteLine("4. Remove Task");
                Console.WriteLine("5. Exit");
                Console.Write("Choose an option: ");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        AddTask();
                        break;
                    case "2":
                        ListTasks();
                        break;
                    case "3":
                        MarkTaskAsCompleted();
                        break;
                    case "4":
                        RemoveTask();
                        break;
                    case "5":
                        exit = true;
                        Console.WriteLine("Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void AddTask()
        {
            Console.Write("Enter the task description: ");
            string description = Console.ReadLine();

            Console.Write("Enter the due date (optional, format: yyyy-mm-dd): ");
            string dueDateInput = Console.ReadLine();

            DateTime? dueDate = null;
            if (DateTime.TryParse(dueDateInput, out DateTime parsedDate))
            {
                dueDate = parsedDate;
            }

            try
            {
                taskManager.AddTask(description, dueDate);
                Console.WriteLine("Task added successfully!");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void ListTasks()
        {
            Console.WriteLine("\nTasks:");

            var taskDescriptions = taskManager.ListTasks();

            if (taskDescriptions.Count == 0)
            {
                Console.WriteLine("No tasks available.");
            }
            else
            {
                foreach (var taskDescription in taskDescriptions)
                {
                    Console.WriteLine(taskDescription);
                }
            }
        }

        static void MarkTaskAsCompleted()
        {
            ListTasks();
            Console.Write("Enter the number of the task to mark as completed: ");
            if (int.TryParse(Console.ReadLine(), out int taskNumber))
            {
                try
                {
                    taskManager.MarkTaskAsCompleted(taskNumber - 1);
                    Console.WriteLine("Task marked as completed!");
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("Invalid task number. Please try again.");
            }
        }

        static void RemoveTask()
        {
            ListTasks();
            Console.Write("Enter the number of the task to remove: ");
            if (int.TryParse(Console.ReadLine(), out int taskNumber))
            {
                try
                {
                    taskManager.RemoveTask(taskNumber - 1);
                    Console.WriteLine("Task removed successfully!");
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("Invalid task number. Please try again.");
            }
        }
    }
}
