# FacultyWpfApp1ButtonStudents
Как увеличить скорость фильтрации ?

Логика фильтрации:
 - Пользователь выбирает **Course** в **CoursesView**;
 - Приложение фильтрует **CoursesStudentsJoinView** в зависимости от **Course**;
 - Приложение фильтрует **StudentsView** в зависимости от **CoursesStudentsJoinView** (исключает из **StudentsView** студентов **Student**,  которые имеются в **CoursesStudentsJoinView**);

Это всё работает медленно.
Как правильно орагнизовать решение или что сделать чтобы существвующее решение работало быстрее?
Как это сделать в минимальном исполнении: NET Framework, без DI, IoC, нтерфейсов для частей проекта.


**CoursesStudentsJoinView** работает с коллекцией элементов **CourseStudentJoin**.
**CourseStudentJoin** является аналаогом **View** в БД.
**CourseStudentJoin** связывает в одну таблицу **CourseStudent** и **Student** поле **NameStudent**


Планируемая логика приложения:  
     - Пользователь добавляет курсы **Course** в **CoursesView**;  
     - Пользователь выбирает **Course** в **CoursesView**;  
     - Пользователь добавляет в **CoursesStudentsJoinView** студента **Student** из **StudentsView**;  
        - т.е.  
            - Пользователь в **StudentsView** выбирает студента **Student**;  
            - Пользователь в **StudentsView** нажимает кнопку **AddInCoursesStudentsJoinView**;  
            - Приложение удаляет студента **Student** из **StudentsView** и добавлет студента в **CoursesStudentsJoinView**.  
  
**Student** с id от 11 до 21 исключены  из **StudentsView**  
потому, что уже хранятся в **CoursesStudentsJoinView** 
Модель:  
  - Course.cs  
  - Student.cs  
  - CourseStudent.cs  
  - CourseStudentJoin.cs 

![image](https://github.com/jhon65496/FacultyWpfApp1ButtonStudents/assets/128128859/6a0a1b40-d370-4347-a319-7f1e32980ee8)

![image](https://github.com/jhon65496/FacultyWpfApp1ButtonStudents/assets/128128859/5b8877bb-8167-4813-8ebc-e7d2cfdb903b)


# Model
## Course
```cs
public class Course
{
    public int IdCourse { get; set; }
    public int Sort { get; set; }
    public string NameCourse { get; set; }
    public string AliasCourse { get; set; }    
    public string Description { get; set; }    
}
```

## Student
```cs
public class Student
{
    public int IdStudent { get; set; }
    public int Sort { get; set; }
    public string NameStudent { get; set; }
    public string AliasStudent { get; set; }    
    public string Description { get; set; }
    
}
```

## CourseStudent
```cs
public class CourseStudent
{
    // CourseStudent
    public int IdCourseStudent { get; set; }
    public int IdStudent { get; set; }
    public int IdCourse { get; set; }
    public int Sort { get; set; }
}
```


## CourseStudentJoin  
Это аналог View(CourseStudent + Student) в БД.
```cs
public class CourseStudentJoin  
{   
    public int IdCourseStudent { get; set; }
    public int IdCourse { get; set; }
    public int IdStudent { get; set; }
    public string NameStudent { get; set; }
}
```


# Логика фильтрации
Пользователь в  **CoursesViewModel** выбирает курс.
**CoursesViewModel** передаёт выбранный курс `SelectedCourse` в **MainWindowViewModel**  в свойство `SelectedCourse`.
Код: `this.mainWindowViewModel.SelectedCourse = SelectedCourse;`

**CoursesViewModel**
```cs
public Course SelectedCourse
{
    get { return _selectedCourse; }
    set
    {
        _selectedCourse = value;
        
        this.mainWindowViewModel.SelectedCourse = SelectedCourse;
        
        RaisePropertyChanged(nameof(SelectedCourse));
    }
}
```


**MainWindowViewModel** передаёт выбранный курс `selectedCourse` в `CoursesStudentsJoinViewModel.CourseFilter`.
Код: `coursesStudentsJoinViewModel.CourseFilter = selectedCourse;`

**MainWindowViewModel**
```cs
 public Course SelectedCourse
{
    get { return selectedCourse; }
    set
    {
        selectedCourse = value;

        // Установить критерий фильтрации для `CoursesStudentsJoinViewModel`
        coursesStudentsJoinViewModel.CourseFilter = selectedCourse;

        // Установить критерий фильтрации для `StudentsViewModel`
        var cSJ = coursesStudentsJoinViewModel.GetCoursesStudentsJoin(selectedCourse);
        this.studentsViewModel.LoadDataUnion(cSJ);

        RaisePropertyChanged(nameof(SelectedCourse));
    }
}
```
---- 

Фильтруем `CoursesStudentsJoinViewModel`
**CoursesStudentsJoinViewModel**
```cs
#region Filter == === === === === ==
private Course _сourseFilter;

public Course CourseFilter
{
    get { return _сourseFilter; }
    set
    {
        _сourseFilter = value;
        _CoursesStudentsJoinsViewSource.View.Refresh();
    }
}


private void OnCoursesStudentsJoinsFilter(object sender, FilterEventArgs e)
{
    //.... Code
    if (courseStudentJoin.IdCourse == CourseFilter.IdCourse)
    {
        e.Accepted = true;
    }
    else
    {
        e.Accepted = false;
    }
}

#region CollectionView
private CollectionViewSource _CoursesStudentsJoinsViewSource;

public ICollectionView CoursesStudentsJoinsView => _CoursesStudentsJoinsViewSource?.View;
#endregion

#endregion
```

---- 

Возвращаемся в **MainWindowViewModel**.
В **MainWindowViewModel** выполняем:
    - в **CoursesStudentsJoinViewModel** фильтруем `ObservableCollection<CourseStudentJoin> CoursesStudentsJoins`;
    - из **CoursesStudentsJoinViewModel** получаем отфильтрованную коллекцию `ObservableCollection<CourseStudentJoin> CoursesStudentsJoins`;
В **CoursesStudentsJoinViewModel** свойство `ObservableCollection<CourseStudentJoin> CoursesStudentsJoins` заполняется при создании **CoursesStudentsJoinViewModel** .

В **MainWindowViewModel** Код: 
```cs
var cSJ = coursesStudentsJoinViewModel.GetCoursesStudentsJoin(selectedCourse);
this.studentsViewModel.LoadDataUnion(cSJ);
```


**CoursesStudentsJoinViewModel**
```cs
// CoursesStudentsJoins
private ObservableCollection<CourseStudentJoin> _coursesStudentsJoin;

public ObservableCollection<CourseStudentJoin> CoursesStudentsJoins
{
    get { return _coursesStudentsJoin; }
    set
    {
        _coursesStudentsJoin = value;

        _CoursesStudentsJoinsViewSource = new CollectionViewSource();
        _CoursesStudentsJoinsViewSource.Source = value;
        _CoursesStudentsJoinsViewSource.Filter += OnCoursesStudentsJoinsFilter;
        _CoursesStudentsJoinsViewSource.View.Refresh(); // 

        // RaisePropertyChanged(nameof(CoursesStudentsJoins));
        // CoursesStudentsJoinsView
        RaisePropertyChanged(nameof(CoursesStudentsJoinsView));
    }
}

public ObservableCollection<CourseStudentJoin> GetCoursesStudentsJoin(Course course)
{
    int IdCourse = course.IdCourse;

    var res = CoursesStudentsJoins.Where(cSJ => cSJ.IdCourse == IdCourse).ToList();
    var coursesStudentsJoins = new ObservableCollection<CourseStudentJoin>(res);

    return coursesStudentsJoins;
}
```

**MainWindowViewModel**
```cs
 public Course SelectedCourse
{
    get { return selectedCourse; }
    set
    {
        selectedCourse = value;

        // Установить критерий фильтрации для `CoursesStudentsJoinViewModel`
        coursesStudentsJoinViewModel.CourseFilter = selectedCourse;

        // Установить критерий фильтрации для `StudentsViewModel`
        var cSJ = coursesStudentsJoinViewModel.GetCoursesStudentsJoin(selectedCourse);
        this.studentsViewModel.LoadDataUnion(cSJ);

        RaisePropertyChanged(nameof(SelectedCourse));
    }
}
```

Передаём отфильтрованный `ObservableCollection<CourseStudentJoin> CoursesStudentsJoins` в **StudentsViewModel**
Кодв **MainWindowViewModel**:  `this.studentsViewModel.LoadDataUnion(cSJ);`

В **StudentsViewModel**  
    -    метод `LoadDataUnion(ObservableCollection<CourseStudentJoin> courseStudentJoin)`
исключает из `ObservableCollection<Student> Students` студунтов. которые содержаться в `ObservableCollection<CourseStudentJoin> courseStudentJoin`
    -    результат помещает в `ObservableCollection<Student> StudentsView`
    - `ObservableCollection<Student> StudentsView` отображаем в представлении **StudentsView.xaml**

`ObservableCollection<Student> Students` - хранит в себе всех студентов. Заполнется при создании **StudentsViewModel** .

**MainWindowViewModel**
```cs
public Course SelectedCourse
{
    get { return selectedCourse; }
    set
    {
        selectedCourse = value;

        // Установить критерий фильтрации для `CoursesStudentsJoinViewModel`
        coursesStudentsJoinViewModel.CourseFilter = selectedCourse;

        // Установить критерий фильтрации для `StudentsViewModel`
        var cSJ = coursesStudentsJoinViewModel.GetCoursesStudentsJoin(selectedCourse);
        this.studentsViewModel.LoadDataUnion(cSJ);

        RaisePropertyChanged(nameof(SelectedCourse));
    }
}
```

**StudentsViewModel**
```cs
// Students. Все студенты. Заполнется при создании **StudentsViewModel**. 
private ObservableCollection<Student> _students;

public ObservableCollection<Student> Students
{
    get { return _students; }
    set 
    {
        _students = value;
        RaisePropertyChanged(nameof(Students));
    }
}

private ObservableCollection<Student> _studentViews;

// Students. Студенты для предсталения `CoursesView`
public ObservableCollection<Student> StudentsView
{
    get { return _studentViews; }
    set
    {
        _studentViews = value;
        RaisePropertyChanged(nameof(StudentsView));
    }
}

public void LoadDataUnion(ObservableCollection<CourseStudentJoin> courseStudentJoin)
{
    StudentsView = new ObservableCollection<Student>(Students
                        .Where(cSJ => !courseStudentJoin
                            .Select(s => s.IdStudent).Contains(cSJ.IdStudent)));
}
```
