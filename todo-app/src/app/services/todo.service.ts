import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Task } from '../model/task.model';
import { Observable, switchMap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class TodoService {
  private http: HttpClient = inject(HttpClient);

  api: string = `${environment.apiURL}Todo`

  getAllTodos(): Observable<Task[]> {
    return this.http.get<Task[]>(`${this.api}/all`, {}).pipe()
  }

  deleteTaskFromTodo(id: number): Observable<Task[]> {
    return this.http.delete(`${this.api}/task/${id}`, {}).pipe(switchMap(() => this.getAllTodos()))
  }

  addTaskToTodo(model: Task): Observable<Task[]> {
    return this.http.post(`${this.api}/task/add`, model, {}).pipe(switchMap(() => this.getAllTodos()))
  }
}
