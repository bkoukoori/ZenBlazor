import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { TaskComponent } from './task/task.component';

interface Task {
  id: number;
  title: string;
  status: 'New' | 'InProgress' | 'Rejected' | 'Verified' | 'Completed';
}
@Component({
  selector: 'app-root',
  standalone: true,                  
  imports: [CommonModule, FormsModule, TaskComponent],
  templateUrl: './app.component.html',
})
// Main component for the application
export class AppComponent {
  tasks: Task[] = [];
  newTaskTitle: string = '';
// Add a new task
  addTask() {
    if (this.newTaskTitle.trim()) {
      const newTask: Task = {
        id: Date.now(),
        title: this.newTaskTitle,
        status: 'New'
      };
      this.tasks.push(newTask);
      this.newTaskTitle = '';
      this.saveTasks();
    }
  }
// Update a task
  updateTask(updatedTask: Task) {
    const index = this.tasks.findIndex(task => task.id === updatedTask.id);
    if (index !== -1) {
      this.tasks[index] = updatedTask;
      this.saveTasks();
    }
  }
// Delete a task
  deleteTask(taskId: number) {
    this.tasks = this.tasks.filter(task => task.id !== taskId);
    this.saveTasks();
  }
// Save tasks to localStorage
  saveTasks() {
    localStorage.setItem('tasks', JSON.stringify(this.tasks));
  }
// Load tasks from localStorage when the component initializes
  loadTasks() {
    const storedTasks = localStorage.getItem('tasks');
    if (storedTasks) {
      this.tasks = JSON.parse(storedTasks);
    }
  }

  // Lifecycle hook to load tasks when the component initializes
  ngOnInit() {
    this.loadTasks();
  }
}
