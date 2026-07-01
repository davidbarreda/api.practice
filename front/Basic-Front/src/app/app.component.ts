import { Component, OnInit } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { TodoItem } from './models/todo-item.model';
import { TodoService } from './services/todo.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Basic-Front';

  tasks: TodoItem[] = [];
  isLoading = false;
  isSubmitting = false;
  errorMessage = '';
  successMessage = '';

  constructor(private readonly todoService: TodoService) {}

  ngOnInit(): void {
    this.loadTasks();
  }

  loadTasks(): void {
    this.isLoading = true;
    this.errorMessage = '';
    this.successMessage = '';

    this.todoService.getTasks().subscribe({
      next: (items) => {
        this.tasks = items;
        this.isLoading = false;
      },
      error: (error) => {
        this.errorMessage = this.getFriendlyError(error, 'No se pudieron cargar las tareas.');
        this.isLoading = false;
      }
    });
  }

  createTask(payload: TodoItem): void {
    this.isSubmitting = true;
    this.errorMessage = '';
    this.successMessage = '';

    this.todoService.createTask(payload).subscribe({
      next: () => {
        this.successMessage = 'Tarea creada correctamente.';
        this.isSubmitting = false;
        this.loadTasks();
      },
      error: (error) => {
        this.errorMessage = this.getFriendlyError(error, 'No se pudo crear la tarea.');
        this.isSubmitting = false;
      }
    });
  }

  toggleCompleted(task: TodoItem): void {
    if (task.id === undefined || task.id === null) {
      return;
    }

    this.errorMessage = '';
    this.successMessage = '';

    const payload: TodoItem = {
      title: task.title ?? '',
      description: task.description ?? '',
      isCompleted: !task.isCompleted,
    };

    this.todoService.updateTask(task.id, payload).subscribe({
      next: (updatedTask) => {
        this.tasks = this.tasks.map((current) => current.id === updatedTask.id ? updatedTask : current);
      },
      error: (error) => {
        this.errorMessage = this.getFriendlyError(error, 'No se pudo actualizar la tarea.');
      }
    });
  }

  private getFriendlyError(error: unknown, fallback: string): string {
    if (error instanceof HttpErrorResponse) {
      const status = String(error.status);
      const statusText = String(error.statusText);
      return `${fallback} (${status} ${statusText})`;
    }

    return fallback;
  }
}
