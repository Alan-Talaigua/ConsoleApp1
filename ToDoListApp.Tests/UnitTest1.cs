using System;
using Xunit;
using ToDoListApp;


namespace ToDoListApp.Tests
{
    public class TaskTests
    {
        public void AddTask_ShouldIncreaseTaskCount()
        {
            var taskManager = new TaskManager();
            int initialCount = taskManager.Tasks.Count;

            taskManager.AddTask("Test Task", DateTime.Now);

            Assert.Equal(initialCount + 1, taskManager.Tasks.Count);
        }
    }
}



