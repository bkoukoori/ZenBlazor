import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-task',
  standalone: true,                      
  imports: [CommonModule, FormsModule],   
  templateUrl: './task.component.html',
})
export class TaskComponent {
  // The task object is passed as an input property
  @Input() task!: { id: number; title: string; status: string };
  @Output() update = new EventEmitter<any>();
  @Output() delete = new EventEmitter<number>();

  statuses = ['New', 'InProgress', 'Rejected', 'Verified', 'Completed'];
  onStatusChange(newStatus: string) {
    this.task.status = newStatus as any;
    this.update.emit(this.task);
  }
// Update the task when the title is changed
  deleteTask() {
    this.delete.emit(this.task.id);
  }
}
