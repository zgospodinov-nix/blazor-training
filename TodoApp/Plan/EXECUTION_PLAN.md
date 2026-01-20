# Blazor Todo App - Execution Plan

## Project Overview
A Blazor-based Todo application with a multi-layered architecture using separate projects for Domain Models and Business Services, demonstrating clean architecture principles.

---

## Solution Structure

```
TodoApp (Solution Root)
├── TodoApp/                          (Blazor Web App - UI Layer)
│   ├── Components/
│   │   ├── Pages/
│   │   │   ├── Home.razor (updated)
│   │   │   ├── Todos.razor (new)
│   │   │   └── NotFound.razor
│   │   ├── Layout/
│   │   └── [UI Components]
│   │       ├── TodoList.razor
│   │       ├── TodoItem.razor
│   │       ├── TodoForm.razor
│   │       └── TodoEditModal.razor
│   ├── Program.cs (updated with service registration)
│   └── wwwroot/
│
├── Domain/                           (Class Library - Data Models)
│   └── Models/
│       └── TodoItem.cs
│
├── Service/                          (Class Library - Business Logic)
│   └── Services/
│       └── TodoService.cs
│
└── Plan/                             (Documentation)
    └── EXECUTION_PLAN.md

```

---

## Projects to Create

### 1. **Domain Project** (Class Library - .NET 10.0)
**Purpose**: Contains domain models and entities
- **Folder**: `Models/`
- **Contents**:
  - `TodoItem.cs` - Todo entity with properties (Id, Title, Description, IsCompleted, CreatedDate, DueDate)

### 2. **Service Project** (Class Library - .NET 10.0)
**Purpose**: Contains business logic and service layer
- **Folder**: `Services/`
- **Dependencies**: References Domain project
- **Contents**:
  - `TodoService.cs` - In-memory CRUD operations and todo management logic
  - Service interface (IServiceTodo or ITodoService) for abstraction

### 3. **TodoApp Project** (Blazor Web App - Already exists)
**Purpose**: UI layer - Blazor components and pages
- **Dependencies**: References Domain and Service projects
- **Program.cs**: Register TodoService as scoped dependency injection
- **Components**: Create reusable Blazor components for todo management

---

## Phase Breakdown

### **Phase 1: Project Setup**
1. Create `Domain` class library project (.NET 10.0)
2. Create `Service` class library project (.NET 10.0)
3. Add project references:
   - Service → Domain
   - TodoApp → Domain, Service
4. Update `Program.cs` to register services

**Deliverables**:
- 2 new projects with proper references
- Service dependency injection configured

---

### **Phase 2: Domain Models**
1. Create `Models` folder in Domain project
2. Build `TodoItem.cs` with:
   - `Id` (Guid)
   - `Title` (string, required)
   - `Description` (string, optional)
   - `IsCompleted` (bool)
   - `CreatedDate` (DateTime)
   - `DueDate` (DateTime, nullable)

**Deliverables**:
- TodoItem model with validation attributes
- Clean, serializable entity

---

### **Phase 3: Service Layer**
1. Create `Services` folder in Service project
2. Create `ITodoService` interface with methods:
   - `GetAllTodosAsync()` → `List<TodoItem>`
   - `GetTodoByIdAsync(Guid id)` → `TodoItem`
   - `AddTodoAsync(TodoItem todo)` → `TodoItem` (with generated Id)
   - `UpdateTodoAsync(Guid id, TodoItem todo)` → `bool`
   - `DeleteTodoAsync(Guid id)` → `bool`
   - `ToggleCompletionAsync(Guid id)` → `bool`
   - `GetActiveTodosAsync()` → `List<TodoItem>` (filtered)
   - `GetCompletedTodosAsync()` → `List<TodoItem>` (filtered)
   - `SearchTodosAsync(string searchTerm)` → `List<TodoItem>`

3. Create `TodoService.cs` implementing `ITodoService`:
   - In-memory `List<TodoItem>` storage
   - Logic for CRUD operations
   - Filtering and search functionality
   - Async methods for future database integration

**Deliverables**:
- Interface contract for todo operations
- Service implementation with business logic
- Support for filtering and searching

---

### **Phase 4: Blazor Components**
1. **TodoList.razor** - Main list component
   - Displays todos in a table/card format
   - Shows title, description, due date, completion status
   - Passes actions (edit, delete, complete) to parent

2. **TodoItem.razor** - Individual todo item component
   - Display single todo with details
   - Action buttons with event callbacks
   - Visual indicators for completion status

3. **TodoForm.razor** - Add/Edit form component
   - Input fields: Title, Description, DueDate
   - Form validation
   - Submit/Cancel callbacks
   - Reusable for both create and edit operations

4. **TodoEditModal.razor** - Modal dialog
   - Wraps TodoForm for editing
   - Modal open/close logic
   - Overlay and backdrop

**Deliverables**:
- Reusable, composable Blazor components
- Proper parameter binding and event callbacks

---

### **Phase 5: Pages**
1. **Home.razor** - Update existing
   - Add overview of todo statistics
   - Link to Todos page

2. **Todos.razor** - Main todo management page
   - Inject TodoService
   - Display TodoList component
   - Implement CRUD operations via service
   - Handle add/edit/delete modal interactions
   - Filter tabs (All, Active, Completed)
   - Search functionality

**Deliverables**:
- Functional todo management interface
- Integration of all components
- Service interaction layer

---

### **Phase 6: UI/UX Enhancement**
1. **Styling**:
   - Use Bootstrap 5 classes
   - Add custom CSS for todo-specific styling
   - Status badges (Active/Completed)
   - Color coding for due dates (overdue, due today, etc.)

2. **User Feedback**:
   - Toast notifications for add/update/delete confirmation
   - Confirmation dialog for delete operations
   - Loading states during operations
   - Empty state messaging

3. **Polish**:
   - Icons for actions (delete, edit, complete, clear)
   - Responsive design
   - Keyboard shortcuts (optional)
   - Smooth animations

**Deliverables**:
- Professional, user-friendly interface
- Clear feedback for all actions

---

### **Phase 7: Advanced Features** (Optional)
- Priority levels for todos
- Categories/Tags
- Due date filtering and sorting
- Statistics dashboard (total, completed %)
- Export todos (CSV, JSON)

**Deliverables**:
- Extended functionality
- Enhanced user experience

---

## Data Models

### TodoItem
```csharp
public class TodoItem
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? DueDate { get; set; }
}
```

---

## Service Interface

### ITodoService
```csharp
public interface ITodoService
{
    Task<List<TodoItem>> GetAllTodosAsync();
    Task<TodoItem?> GetTodoByIdAsync(Guid id);
    Task<TodoItem> AddTodoAsync(TodoItem todo);
    Task<bool> UpdateTodoAsync(Guid id, TodoItem todo);
    Task<bool> DeleteTodoAsync(Guid id);
    Task<bool> ToggleCompletionAsync(Guid id);
    Task<List<TodoItem>> GetActiveTodosAsync();
    Task<List<TodoItem>> GetCompletedTodosAsync();
    Task<List<TodoItem>> SearchTodosAsync(string searchTerm);
}
```

---

## Technology Stack

- **Framework**: .NET 10.0
- **UI Framework**: Blazor Web App (Interactive Server render mode)
- **Styling**: Bootstrap 5
- **Data Storage**: In-memory (List<T>)
- **Architecture**: Clean Architecture (Domain, Service, Presentation layers)
- **DI Container**: Built-in ASP.NET Core Dependency Injection

---

## Implementation Order

1. ✅ Create Domain project + TodoItem model
2. ✅ Create Service project + ITodoService interface + TodoService implementation
3. ✅ Register services in Program.cs
4. ✅ Create Blazor components (TodoList, TodoItem, TodoForm, TodoEditModal)
5. ✅ Create/Update pages (Home.razor, Todos.razor)
6. ✅ Add styling and user feedback
7. (Optional) Add advanced features

---

## Notes

- **No Database**: In-memory storage only (data resets on app restart)
- **Demo Purpose**: Designed as a training/demo application
- **Async Pattern**: All service methods are async for future database integration
- **Clean Architecture**: Separation of concerns with Domain, Service, and UI layers
- **Dependency Injection**: Services registered as scoped for proper lifecycle management

