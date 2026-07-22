import { Component, inject, signal, WritableSignal } from '@angular/core';
import { TodoService } from '../services/todo.service';
import { Task } from '../model/task.model';
import { LucidePlus, LucideTrash } from '@lucide/angular';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FieldTree, form, FormField } from '@angular/forms/signals';

@Component({
  selector: 'app-todo',
  imports: [LucideTrash, LucidePlus, ReactiveFormsModule, FormsModule, FormField],
  templateUrl: './todo.html',
  styleUrl: './todo.css',
  standalone: true
})
export class Todo {
  todoService = inject(TodoService);

  todos: WritableSignal<Task[]> = signal<Task[]>([]);
  showForm: boolean = false;
  taskModel: WritableSignal<Task>;
  taskForm: FieldTree<Task>;

  constructor() {

    this.todoService.getAllTodos().subscribe(tasks => this.todos.set(tasks));

    this.taskModel = signal<Task> ({
      id: 0,
      name: ''
    })

    this.taskForm = form(this.taskModel);
  }

  deleteTask(id: number) {
    this.todoService.deleteTaskFromTodo(id).subscribe( {
      next: todos => {
        this.todos.set(todos)
      }
    })
  }

  onSubmit() {
    const model: Task = this.taskForm().value();
    model.id = this.todos().length + 1;

    this.showForm = false;
    this.todoService.addTaskToTodo(model).subscribe({
      next: todos => {
        this.todos.set(todos);

        this.taskModel.set({
          id: 0,
          name: '',
        });
      }
    })
  }
}
