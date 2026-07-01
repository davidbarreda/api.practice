import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { AddTaskCardComponent } from './components/add-task-card/add-task-card.component';
import { TaskListComponent } from './components/task-list/task-list.component';

describe('AppComponent', () => {
  let httpTestingController: HttpTestingController;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HttpClientTestingModule, FormsModule],
      declarations: [
        AppComponent,
        AddTaskCardComponent,
        TaskListComponent
      ],
    }).compileComponents();

    httpTestingController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpTestingController.verify();
  });

  it('should create the app', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });

  it(`should have as title 'Basic-Front'`, () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app.title).toEqual('Basic-Front');
  });

  it('should load tasks on init', () => {
    const fixture = TestBed.createComponent(AppComponent);

    fixture.detectChanges();

    const req = httpTestingController.expectOne('https://localhost:7276/api/Todo');
    expect(req.request.method).toBe('GET');
    req.flush([]);

    expect(fixture.componentInstance.tasks.length).toBe(0);
  });

  it('should render todo title', () => {
    const fixture = TestBed.createComponent(AppComponent);

    fixture.detectChanges();

    const req = httpTestingController.expectOne('https://localhost:7276/api/Todo');
    req.flush([]);
    fixture.detectChanges();

    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('h1')?.textContent).toContain('Gestor de tareas');
  });
});
