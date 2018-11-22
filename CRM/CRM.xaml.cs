using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CRM.Models;

namespace CRM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Constructor & Variables

        // Dəyişənlər:
        CRMEntities db = new CRMEntities();
        
        private VwCounterparty selectedCounterparty = new VwCounterparty();     // Axtarış nəticəsində tapılmış və seçilmiş kontragent:
        private VwReminder selectedReminder = new VwReminder();                 // Axtarış nəticəsində tapılmış və seçilmiş xatırlatma:
        private bool isSelectedCounterparty = false;
        private bool isSelectedReminer = false;
        private bool showCompleted = false;

        public DateTime firstTime = new DateTime();
        public DateTime lastTime = new DateTime();
        public DateTime currentTime = DateTime.Now;
        
        // Konstruktor:
        public MainWindow()
        {
            InitializeComponent();
            SetTimeInterval();
            DpReminderDate.DisplayDateStart = firstTime;
            DpReminderDate.DisplayDateEnd = lastTime;
            FillDataGridCounterparties();
            SearchReminders();
        }

        #endregion

        #region Metods

        // "Kontragent" cədvəlinin axtarış kriteriyalarına uyğun doldurulması:
        private void FillDataGridCounterparties()
        {
            List<Counterparty> counterparties = db.Counterparties.Where(c =>
                                                                       (TxtCounterparty.Text != "" ? c.Name.Contains(TxtCounterparty.Text) : true) &&
                                                                       (TxtResponsiblePerson.Text != "" ? c.ResponsiblePerson.Contains(TxtResponsiblePerson.Text) : true)).OrderBy(c => c.Name).ToList();
            DgCounterparties.Items.Clear();
            foreach (Counterparty counterparty in counterparties)
            {
                DgCounterparties.Items.Add(new VwCounterparty
                {
                    Id = counterparty.Id,
                    Name = counterparty.Name,
                    Person = counterparty.ResponsiblePerson,
                    Position = counterparty.Position.Name
                });
            }
        }

        // "Xatırlatma"-ların axtarışı:
        private void SearchReminders()
        {
            if (db.Reminders.Count() == 0)
            {
                DgReminders.Visibility = Visibility.Hidden;
                DgComments.Visibility = Visibility.Hidden;
                LblNoResult.Visibility = Visibility.Visible;
                return;
            }

            DgReminders.Items.Clear();
            List<Reminder> reminders = db.Reminders.Where(r =>
                                                         (isSelectedCounterparty ? r.CounterpartyID == selectedCounterparty.Id : true) &&
                                                         (showCompleted ? r.Completed : !r.Completed) &&
                                                          r.EndTime >= firstTime && r.EndTime <= lastTime).ToList();
            foreach (Reminder reminder in reminders)
            {
                DgReminders.Items.Add(new VwReminder
                {
                    Id = reminder.Id,
                    Counterparty = reminder.Counterparty.Name,
                    Employee = reminder.Employee.Name,
                    Text = reminder.Text,
                    Date = reminder.Date.ToShortDateString(),
                    EndTime = reminder.EndTime.ToShortDateString(),
                    Completed = reminder.Completed ? "Tamamlanıb" : "İcradadır"
                });
            }
        }

        // "Rəy"-lərin göstərilməsi:
        private void ShowComments()
        {
            DgComments.Items.Clear();
            try
            {
                List<Comment> comments = db.Comments.Where(c => (isSelectedReminer ? c.ReminderID == selectedReminder.Id : false)).ToList();
                foreach (Comment comment in comments)
                {
                    DgComments.Items.Add(new VwComment
                    {
                        Id = comment.Id,
                        Date = comment.Date.ToShortTimeString() + " / " + comment.Date.ToShortDateString(),
                        Text = comment.Text
                    });
                }
            }
            catch
            {
                return;
            }
        }

        // Hesabat tarixinin təyini:
        private void SetTimeInterval()
        {
            if (RbFullTime.IsChecked.Value)
            {
                firstTime = db.Reminders.First().Date;
                lastTime = db.Reminders.ToList().OrderByDescending(l => l.EndTime).First().EndTime;
            }
            else if (RbLastWeek.IsChecked.Value)
            {
                firstTime = currentTime;
                lastTime = currentTime.AddDays(7);
            }
            else if (RbLastDay.IsChecked.Value)
            {
                firstTime = currentTime;
                lastTime = currentTime.AddDays(1);
            }
            else
            {
                firstTime = DpReminderDate.SelectedDate.Value;
                lastTime = firstTime.AddDays(1);
            }
        }

        #endregion

        #region Events

        // Kontragent cədvəlindən kontragent seçilməsi:
        private void DgCounterparties_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            selectedCounterparty = DgCounterparties.SelectedItem as VwCounterparty;
            isSelectedCounterparty = true;
            RbFullTime.IsChecked = true;
            RbLastDay.IsChecked = false;
            RbLastWeek.IsChecked = false;
            CbShowAll.IsChecked = false;
            SetTimeInterval();
            SearchReminders();
        }

        // Xatırlatma cədvəlindən xatırlatma seçilməsi:
        private void DgReminders_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            selectedReminder = DgReminders.SelectedItem as VwReminder;
            isSelectedReminer = true;
            ShowComments();
        }

        // Son bir gün seçilməsi:
        private void RbLastDay_Click(object sender, RoutedEventArgs e)
        {
            SetTimeInterval();
            SearchReminders();
        }

        // Son bir həftə seçilməsi:
        private void RbLastWeek_Click(object sender, RoutedEventArgs e)
        {
            SetTimeInterval();
            SearchReminders();
        }

        // Tam dövrün seçilməsi:
        private void RbFullTime_Click(object sender, RoutedEventArgs e)
        {
            SetTimeInterval();
            SearchReminders();
        }

        // Bütün "xatırlatma"-ların göstərilməsi:
        private void CbShowAll_Click(object sender, RoutedEventArgs e)
        {
            if (CbShowAll.IsChecked.Value)
            {
                isSelectedCounterparty = false;
            }
            else
            {
                isSelectedCounterparty = true;
            }
            SearchReminders();
        }

        // Tamamlanmış "xatırlatma"-ların göstərilməsi:
        private void CbShowCompleted_Click(object sender, RoutedEventArgs e)
        {
            if (CbShowCompleted.IsChecked.Value)
            {
                showCompleted = true;
            }
            else
            {
                showCompleted = false;
            }
            SearchReminders();
        }

        // Rəylərə baxışın aktivləşdirilməsi:
        private void CbShowComments_Click(object sender, RoutedEventArgs e)
        {
            if (CbShowComments.IsChecked.Value)
            {
                DgReminders.Height = 170;
                DgReminders.VerticalAlignment = VerticalAlignment.Top;
                DgReminders.Margin = new Thickness(50, 200, 50, 0);
            }
            else
            {
                DgReminders.Height = Double.NaN;
                DgReminders.VerticalAlignment = VerticalAlignment.Stretch;
                DgReminders.Margin = new Thickness(50, 200, 50, 75);
            }
            ShowComments();
        }

        #endregion

        #region Buttons

        // Yeni "kontragent" yaratma düyməsi:
        private void BtnAddCounterparty_Click(object sender, RoutedEventArgs e)
        {
			AddCounterparty addCounterparty = new AddCounterparty();
			addCounterparty.ShowDialog();
        }

        // Kriteriyaya uyğun "Kontragent"-lərin axtarış düyməsi:
        private void BtnSearchCounterparty_Click(object sender, RoutedEventArgs e)
        {
            FillDataGridCounterparties();
        }
        
        // Tarixə görə "Xatırlatma"-ların axtarış düyməsi:
        private void BtnShowReminders_Click(object sender, RoutedEventArgs e)
        {
            isSelectedCounterparty = false;
            RbFullTime.IsChecked = false;
            RbLastWeek.IsChecked = false;
            RbLastDay.IsChecked = false;
            CbShowAll.IsChecked = true;
            CbShowComments.IsChecked = false;
            SetTimeInterval();
            SearchReminders();
        }

        #endregion

    }
}
