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
        
        private VwCounterparty selectedCounterparty = new VwCounterparty(); // Axtarış nəticəsində tapılmış və seçilmiş kontragent:
        private Boolean isSelectedCounterparty = false;

        public DateTime firstTime = new DateTime();
        public DateTime lastTime = DateTime.Now;
        public DateTime currentTime = DateTime.Now;
        
        // Konstruktor:
        public MainWindow()
        {
            InitializeComponent();
            SetFirstTime();
            DpReminderDate.DisplayDateStart = firstTime;
            DpReminderDate.DisplayDateEnd = currentTime;
            FillDataGridCounterparties();
            SearchReminders();
        }

        #endregion

        #region Metods

        // Kontragent cədvəlinin axtarış kriteriyalarına uyğun doldurulması:
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

        // Xatırlatmaların axtarışı:
        private void SearchReminders()
        {
            if (db.Reminders.Count() == 0)
            {
                DgReminders.Visibility = Visibility.Hidden;
                LblNoResult.Visibility = Visibility.Visible;
                return;
            }

            List<Reminder> reminders = db.Reminders.Where(r =>
                                                         (isSelectedCounterparty ? r.CounterpartyID == selectedCounterparty.Id : true) &&
                                                          r.Date >= firstTime && r.Date <= lastTime).ToList();
            DgReminders.Items.Clear();
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

        //Hesabat tarixinin təyini:
        private void SetFirstTime()
        {
            if (RbFullTime.IsChecked.Value)
            {
                firstTime = db.Reminders.First().Date;
            }
            else if (RbLastWeek.IsChecked.Value)
            {
                firstTime = currentTime.AddDays(-7);
            }
            else if (RbLastDay.IsChecked.Value)
            {
                firstTime = currentTime.AddDays(-1);
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
            CbShowComments.IsChecked = false;
            SetFirstTime();
            SearchReminders();
        }

        // Son bir gün seçilməsi:
        private void RbLastDay_Click(object sender, RoutedEventArgs e)
        {
            SetFirstTime();
            SearchReminders();
        }

        // Son bir həftə seçilməsi:
        private void RbLastWeek_Click(object sender, RoutedEventArgs e)
        {
            SetFirstTime();
            SearchReminders();
        }

        // Tam dövrün seçilməsi:
        private void RbFullTime_Click(object sender, RoutedEventArgs e)
        {
            SetFirstTime();
            SearchReminders();
        }

        // Bütün Xatırlatmaların göstərilməsi:
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

        // Rəylərə baxışın aktivləşdirilməsi:
        private void CbShowComments_Click(object sender, RoutedEventArgs e)
        {

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
            SetFirstTime();
            SearchReminders();
        }

        #endregion
        
    }
}
