﻿using GUI.DTO;
using CLI.DAO;
using CLI.Model;
using GUI.View.MenuBar;
using GUI.View.Student;
using GUI.View.Profesor;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using CLI.Observer;
using GUI.View.DialogWindows;
using GUI.View.Predmet;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IObserver
    {
        public static readonly DependencyProperty CurrentTabProperty =
            DependencyProperty.Register(
                nameof(CurrentTab),
                typeof(string),
                typeof(MainWindow),
                new PropertyMetadata("Studenti", OnCurrentTabChanged));


        public string CurrentTab
        {
            get { return (string)GetValue(CurrentTabProperty); }
            set { SetValue(CurrentTabProperty, value); }
        }


        public ObservableCollection<StudentDTO> Students { get; set; }
        public StudentDTO SelectedStudent { get; set; }
        private StudentDAO studentsDAO { get; set; }
        public ObservableCollection<ProfesorDTO> Profesors { get; set; }
        public ProfesorDTO SelectedProfesor { get; set; }
        private ProfessorDAO profesorsDAO { get; set; }
        public ObservableCollection<PredmetDTO> Predmets {  get; set; }
        public PredmetDTO SelectedPredmet {  get; set; }
        private SubjectDAO predmetsDAO { get; set; }
        private ExamGradesDAO examGradesDAO { get; set; }
        private AdressDAO adressesDAO { get; set; }
        private DepartmentDAO departmentsDAO { get; set; }
        private StudentSubjectDAO studentSubjectDAO { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            MainTabControl.SelectionChanged += MainTabControl_SelectionChanged;

            InitializeStatusBar();
            DataContext = this;

            Students = new ObservableCollection<StudentDTO>();
            studentsDAO = new StudentDAO();
            studentsDAO.StudentSubject.Subscribe(this);

            Profesors = new ObservableCollection<ProfesorDTO>();
            profesorsDAO = new ProfessorDAO();
            profesorsDAO.ProfesorSubject.Subscribe(this);

            Predmets = new ObservableCollection<PredmetDTO>();
            predmetsDAO = new SubjectDAO();
            predmetsDAO.PredmetSubject.Subscribe(this);

            examGradesDAO = new ExamGradesDAO();
            adressesDAO = new AdressDAO();
            departmentsDAO = new DepartmentDAO();
            studentSubjectDAO = new StudentSubjectDAO();
            fillObjects(studentsDAO, profesorsDAO, predmetsDAO, examGradesDAO, adressesDAO, departmentsDAO, studentSubjectDAO);
            CurrentTab = "Studenti";
            UpdateTabStatus();
            Update();
        }

        private void fillObjects(StudentDAO studentsDAO, ProfessorDAO profesorsDAO, SubjectDAO predmetsDAO, ExamGradesDAO examGradesDAO, AdressDAO adressesDAO, DepartmentDAO departmentsDAO, StudentSubjectDAO studentSubjectDAO)
        {
            studentsDAO.fillObjectsAndLists(studentSubjectDAO, predmetsDAO, adressesDAO, examGradesDAO);
            profesorsDAO.fillObjectsAndLists(predmetsDAO, adressesDAO);
            predmetsDAO.fillObjectsAndLists(studentsDAO, studentSubjectDAO, profesorsDAO, examGradesDAO);
            examGradesDAO.fillObjectsAndLists(studentsDAO, predmetsDAO);
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.H)
            {
                OpenAboutWindow();
            }
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.N)
            {
                CreateEntityButton_Click();
            }
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.S)
            {
                //OpenAboutWindow();
            }
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.O)
            {
                //OpenAboutWindow();
            }
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.E)
            {
                EditEntityButton_Click();
            }
            if (Keyboard.Modifiers == (ModifierKeys.Control | ModifierKeys.Alt) && e.Key == Key.P)
            {
                MenuItem_Predmeti_Click();
            }
            if (Keyboard.Modifiers == (ModifierKeys.Control | ModifierKeys.Alt) && e.Key == Key.S)
            {
                MenuItem_Studenti_Click();
            }
            if (Keyboard.Modifiers == (ModifierKeys.Control | ModifierKeys.Alt) && e.Key == Key.R)
            {
                MenuItem_Profesori_Click();
            }
            if (e.Key == Key.Delete)
            {
                DeleteEntityButton_Click();
            }
        }

        private void SaveApp(object sender, RoutedEventArgs e)
        {

        }

        private void CloseApp_Execution(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OpenAboutWindow()
        {
            var aboutWindow = new About();
            aboutWindow.Owner = this;
            aboutWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            aboutWindow.ShowDialog();
        }

        private void OpenAboutWindow(object sender, RoutedEventArgs e)
        {
            OpenAboutWindow();
        }

        private void MenuItem_Predmeti_Click()
        {
            MainTabControl.SelectedItem = MainTabControl.Items.Cast<TabItem>().First(tab => tab.Header.Equals("Predmeti"));
        }
        private void MenuItem_Predmeti_Click(object sender, RoutedEventArgs e)
        {
            MenuItem_Predmeti_Click();
        }

        private void MenuItem_Profesori_Click()
        {
            MainTabControl.SelectedItem = MainTabControl.Items.Cast<TabItem>().First(tab => tab.Header.Equals("Profesori"));
        }
        private void MenuItem_Profesori_Click(object sender, RoutedEventArgs e)
        {
            MenuItem_Profesori_Click();
        }

        private void MenuItem_Studenti_Click()
        {
            MainTabControl.SelectedItem = MainTabControl.Items.Cast<TabItem>().First(tab => tab.Header.Equals("Studenti"));
        }
        private void MenuItem_Studenti_Click(object sender, RoutedEventArgs e)
        {
            MenuItem_Studenti_Click();
        }

        private void CreateEntityButton_Click(object sender, RoutedEventArgs e)
        {
            CreateEntityButton_Click();
        }

        private void CreateEntityButton_Click()
        {
            if (MainTabControl.SelectedItem == StudentsTab)
            {
                var addStudentWindow = new AddStudent(studentsDAO);
                addStudentWindow.Owner = this;
                addStudentWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                addStudentWindow.ShowDialog();
                //Update();
            }
            else if (MainTabControl.SelectedItem == ProffesorsTab)
            {
                var addProfesorWindow = new AddProfesor(profesorsDAO);
                addProfesorWindow.Owner = this;
                addProfesorWindow.WindowStartupLocation= WindowStartupLocation.CenterOwner;
                addProfesorWindow.ShowDialog();
            }
            else if (MainTabControl.SelectedItem == SubjectsTab)
            {
                var addPredmetWindow = new AddPredmet(predmetsDAO);
                addPredmetWindow.Owner = this;
                addPredmetWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                addPredmetWindow.ShowDialog();
            }
        }

        private void DeleteEntityButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteEntityButton_Click();
        }

        private void DeleteEntityButton_Click()
        {
            if (MainTabControl.SelectedItem == StudentsTab)
            {
                // Delete student logic
                if (SelectedStudent == null)
                {
                    MessageBox.Show(this, "Please choose a student to delete!");
                }
                else
                {
                    var confirmationDialog = new ConfirmationWindow("Student");
                    confirmationDialog.Owner = this;
                    confirmationDialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    confirmationDialog.ShowDialog();

                    if (confirmationDialog.UserConfirmed)
                    {
                        studentsDAO.RemoveStudent(SelectedStudent.StudentId);
                    }
                }
            }
            else if (MainTabControl.SelectedItem == ProffesorsTab)
            {
                if(SelectedProfesor == null)
                {
                    MessageBox.Show(this, "Please choose a profesor to delete!");
                }
                else
                {
                    var confirmationDialog = new ConfirmationWindow("Profesor");
                    confirmationDialog.Owner = this;
                    confirmationDialog.WindowStartupLocation= WindowStartupLocation.CenterOwner;
                    confirmationDialog.ShowDialog();

                    if (confirmationDialog.UserConfirmed)
                    {
                        profesorsDAO.RemoveProfessor(SelectedProfesor.ProfesorId);
                    }
                }
            }
            else if (MainTabControl.SelectedItem == SubjectsTab)
            {
                if (SelectedPredmet == null)
                {
                    MessageBox.Show(this, "Please choose a subject to delete!");
                }
                else
                {
                    var confirmationDialog = new ConfirmationWindow("Subject");
                    confirmationDialog.Owner = this;
                    confirmationDialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    confirmationDialog.ShowDialog();

                    if(confirmationDialog.UserConfirmed)
                    {
                        predmetsDAO.RemovePredmet(SelectedPredmet.PredmetId);
                    }
                }
            }
        }

        private void EditEntityButton_Click(object sender, RoutedEventArgs e)
        {
            EditEntityButton_Click();
        }

        private void EditEntityButton_Click()
        {
            if (MainTabControl.SelectedItem == StudentsTab)
            { 
                if (SelectedStudent == null)
                {
                    MessageBox.Show(this, "Please choose a student to edit!");
                }
                else
                {
                    var editStudentWindow = new EditStudent(studentsDAO, SelectedStudent.Clone(), studentSubjectDAO);
                    editStudentWindow.Owner = this;
                    editStudentWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    editStudentWindow.ShowDialog();
                }
            }else if(MainTabControl.SelectedItem == ProffesorsTab) 
            {
                if(SelectedProfesor == null)
                {
                    MessageBox.Show(this, "Please choose a professor to edit!");
                }
                else
                {
                    var editsProfesorWindow = new EditProfesor(profesorsDAO, SelectedProfesor.Clone());
                    editsProfesorWindow.Owner = this;
                    editsProfesorWindow.WindowStartupLocation= WindowStartupLocation.CenterOwner;
                    editsProfesorWindow.ShowDialog();
                }
            }
            else if (MainTabControl.SelectedItem == SubjectsTab)
            {
                if(SelectedPredmet == null)
                {
                    MessageBox.Show(this, "Please choose a subject to edit!");
                }
                else
                {
                    var editSubjectWindow = new EditPredmet(predmetsDAO, SelectedPredmet.Clone());
                    editSubjectWindow.Owner = this;
                    editSubjectWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    editSubjectWindow.ShowDialog();
                }
            }
            Update();
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Search button clicked!");
            // Add logic to perform the search based on the criteria
        }

        private void InitializeStatusBar()
        {
            // Update date and time every second
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += (sender, e) =>
            {
                UpdateDate();
                UpdateTime();
            };
            timer.Start();
        }

        private void UpdateDate()
        {
            statusDate.Text = $"Date: {DateTime.Now.ToString("yyyy-MM-dd")}";
        }

        private void UpdateTime()
        {
            statusTime.Text = $"Time: {DateTime.Now.ToString("HH:mm:ss")}";
        }

        public void Update()
        {
            Students.Clear();
            foreach (Student student in studentsDAO.GetAllStudents()) Students.Add(new StudentDTO(student));

            Predmets.Clear();
            foreach(Predmet predmet in predmetsDAO.GetAllPredmets()) Predmets.Add(new PredmetDTO(predmet));

            Profesors.Clear();
            foreach(Profesor profesor in profesorsDAO.GetAllProfessors()) Profesors.Add(new ProfesorDTO(profesor));

        }

        private void StudentsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ProfesorsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void SubjectsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainTabControl.SelectedItem != null)
            {
                CurrentTab = ((TabItem)MainTabControl.SelectedItem).Header.ToString();
            }
        }
        private void UpdateTabStatus()
        {
            statusTab.Text = $"Tab: {CurrentTab}";
        }
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            // Postavite početni tab
            // CurrentTab = ((TabItem)MainTabControl.SelectedItem).Header.ToString();
        }

        private static void OnCurrentTabChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is MainWindow mainWindow)
            {
                mainWindow.UpdateTabStatus();
            }
        }
    }
}
