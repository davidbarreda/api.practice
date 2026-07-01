import { Component, EventEmitter, Input, Output } from '@angular/core';
import { TodoItem } from '../../models/todo-item.model';

@Component({
  selector: 'app-add-task-card',
  templateUrl: './add-task-card.component.html',
  styleUrls: ['./add-task-card.component.css']
})
export class AddTaskCardComponent {
  @Input() isSubmitting = false;
  @Input() isLoading = false;

  @Output() createTask = new EventEmitter<TodoItem>();
  @Output() reloadTasks = new EventEmitter<void>();

  newTask: TodoItem = {
    title: '',
    description: '',
    isCompleted: false,
  };

  submitTask(): void {
    const title = (this.newTask.title ?? '').trim();
    if (!title) {
      return;
    }

    this.createTask.emit({
      title,
      description: this.newTask.description?.trim() ?? '',
      isCompleted: this.newTask.isCompleted,
    });

    this.newTask = {
      title: '',
      description: '',
      isCompleted: false,
    };
  }

  reload(): void {
    this.reloadTasks.emit();
  }
}
