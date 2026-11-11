import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Libros } from './libros/libros';
import { Menu } from './menu/menu';
import { Inicio } from './inicio/inicio';


@Component({
  selector: 'app-root',
  imports: [RouterOutlet , Libros, Menu, Inicio],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('biblioteca');
}
