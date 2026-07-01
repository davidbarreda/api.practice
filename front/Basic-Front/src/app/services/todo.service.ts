import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TodoItem } from '../models/todo-item.model';

@Injectable({
  providedIn: 'root'
})
export class TodoService {
  private readonly apiTodoUrl = 'https://localhost:7276/api/Todo';

  constructor(private readonly http: HttpClient) {}

  getTasks(): Observable<TodoItem[]> {
    return this.http.get<TodoItem[]>(this.apiTodoUrl);
  }

  createTask(payload: TodoItem): Observable<number> {
    return this.http.post<number>(this.apiTodoUrl, payload);
  }

  updateTask(taskId: number, payload: TodoItem): Observable<TodoItem> {
    return this.http.put<TodoItem>(`${this.apiTodoUrl}/${taskId}`, payload);
  }
}
