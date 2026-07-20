import { Component, EventEmitter, Input, Output } from '@angular/core';
import { TodoItem } from '../../models/todo-item.model';

@Component({
  selector: 'app-task-list',
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.css']
})
export class TaskListComponent {
  @Input() tasks: TodoItem[] = [];
  @Input() isLoading = false;

  @Output() toggleCompleted = new EventEmitter<TodoItem>();
  @Output() deleteTask = new EventEmitter<number>();

  onToggleCompleted(task: TodoItem): void {
    this.toggleCompleted.emit(task);
  }

  trackByTaskId(index: number, task: TodoItem): number {
    return task.id ?? index;
  }

  onDeleteTask(id?: number): void {
    if (id !== undefined) {
      this.deleteTask.emit(id);
    }
  }
}
