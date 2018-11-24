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
        private VwMission selectedMission = new VwMission();                 // Axtarış nəticəsində tapılmış və seçilmiş tapşırıq:
        private bool isSelectedCounterparty = false;
        private bool isSelectedMission = false;
        private bool showCompleted = false;

        public DateTime firstTime = new DateTime();
        public DateTime lastTime = new DateTime();
        public DateTime currentTime = DateTime.Now;
        
        // Konstruktor:
        public MainWindow()
        {
            InitializeComponent();
            SetTimeInterval();
            DpMissionDate.DisplayDateStart = firstTime;
            DpMissionDate.DisplayDateEnd = lastTime;
            DpMissionEndDate.DisplayDateStart = DateTime.Now.Date;
            FillDataGridCounterparties();
            SearchMissions();
            FillEmployees();
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
            BtnCounterpartyInfo.IsEnabled = false;
        }

        // Tapşırıqların axtarışı:
        private void SearchMissions()
        {
            if (db.Missions.Count() == 0)
            {
                DgMissions.Visibility = Visibility.Hidden;
                DgComments.Visibility = Visibility.Hidden;
                LblNoResult.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                DgMissions.Visibility = Visibility.Visible;
                LblNoResult.Visibility = Visibility.Hidden;
            }

            BtnMarkCompleted.IsEnabled = false;
            DgMissions.Items.Clear();
            try
            {
                List<Mission> missions = db.Missions.Where(m =>
                                                          (isSelectedCounterparty ? m.CounterpartyID == selectedCounterparty.Id : true) &&
                                                          (showCompleted ? m.Completed : !m.Completed) &&
                                                           m.EndTime >= firstTime && m.EndTime <= lastTime).ToList();
                foreach (Mission mission in missions)
                {
                    DgMissions.Items.Add(new VwMission
                    {
                        Id = mission.Id,
                        Counterparty = mission.Counterparty.Name,
                        Employee = mission.Employee.Name,
                        Text = mission.Text,
                        Date = mission.Date.ToShortDateString(),
                        EndTime = mission.EndTime.ToShortDateString(),
                        Completed = mission.Completed ? "Tamamlanıb" : "İcradadır"
                    });
                }
            }
            catch
            {
                return;
            }
        }

        // Rəylərin göstərilməsi:
        private void FillComments()
        {
            DgComments.Items.Clear();
            try
            {
                List<Comment> comments = db.Comments.Where(c => (isSelectedMission ? c.MissionID == selectedMission.Id : false)).ToList();
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
            if (db.Missions.Count() == 0)
            {
                return;
            }
            if (RbFullTime.IsChecked.Value)
            {
                firstTime = db.Missions.First().Date;
                lastTime = db.Missions.ToList().OrderByDescending(l => l.EndTime).First().EndTime;
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
                firstTime = DpMissionDate.SelectedDate.Value;
                lastTime = firstTime.AddDays(1);
            }
        }

        // İşçilərin siyahısının doldurulması:
        private void FillEmployees()
        {
            List<Employee> employees = db.Employees.ToList();
            foreach (Employee employee in employees)
            {
                CmbEmployees.Items.Add(employee.Name + ", " + employee.Surname);
            }
        }

        // Dialoq pəncərələri bağlandığında əsas pəncərənin yenilənməsi üçün:
        public void Refresh() => FillDataGridCounterparties();

        #endregion

        #region Events

        // Kontragent cədvəlindən kontragent seçilməsi:
        private void DgCounterparties_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            selectedCounterparty = DgCounterparties.SelectedItem as VwCounterparty;
            isSelectedCounterparty = true;
            BtnCounterpartyInfo.IsEnabled = true;
            RbFullTime.IsChecked = true;
            RbLastDay.IsChecked = false;
            RbLastWeek.IsChecked = false;
            CbShowAll.IsChecked = false;
            SetTimeInterval();
            SearchMissions();
            LblMissionAttention.Content = "";
        }

        // Tapşırıq cədvəlindən tapşırıq seçilməsi:
        private void DgMissions_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            selectedMission = DgMissions.SelectedItem as VwMission;
            isSelectedMission = true;
            FillComments();
            if (selectedMission == null)
            {
                return;
            }
            if (db.Missions.Find(selectedMission.Id).Completed)
            {
                BtnMarkCompleted.IsEnabled = false;
            }
            else
            {
                BtnMarkCompleted.IsEnabled = true;
            }
            TxtBlcMissionText.Text = selectedMission.Text;
        }

        // Son bir gün seçilməsi:
        private void RbLastDay_Click(object sender, RoutedEventArgs e)
        {
            SetTimeInterval();
            SearchMissions();
        }

        // Son bir həftə seçilməsi:
        private void RbLastWeek_Click(object sender, RoutedEventArgs e)
        {
            SetTimeInterval();
            SearchMissions();
        }

        // Tam dövrün seçilməsi:
        private void RbFullTime_Click(object sender, RoutedEventArgs e)
        {
            SetTimeInterval();
            SearchMissions();
        }

        // Bütün tapşırıqların göstərilməsi:
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
            SearchMissions();
        }

        // Tamamlanmış tapşırıqların göstərilməsi:
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
            SearchMissions();
        }

        // Rəylərə baxışın aktivləşdirilməsi:
        private void CbShowComments_Click(object sender, RoutedEventArgs e)
        {
            GrdMission.Visibility = Visibility.Hidden;
            GrdComment.Visibility = Visibility.Hidden;
            if (CbShowComments.IsChecked.Value)
            {
                DgMissions.Height = 170;
                DgMissions.VerticalAlignment = VerticalAlignment.Top;
                DgMissions.Margin = new Thickness(50, 200, 50, 0);
                DgComments.Visibility = Visibility.Visible;
            }
            else
            {
                DgComments.Visibility = Visibility.Hidden;
                DgMissions.Height = Double.NaN;
                DgMissions.VerticalAlignment = VerticalAlignment.Stretch;
                DgMissions.Margin = new Thickness(50, 200, 50, 75);
            }
            FillComments();
        }

        #endregion

        #region Buttons

        // Yeni kontragent yaratma düyməsi:
        private void BtnAddCounterparty_Click(object sender, RoutedEventArgs e)
        {
			AddCounterparty addCounterparty = new AddCounterparty();
			addCounterparty.ShowDialog();
        }

        // Kriteriyaya uyğun kontragentlərin axtarış düyməsi:
        private void BtnSearchCounterparty_Click(object sender, RoutedEventArgs e)
        {
            FillDataGridCounterparties();
        }

        // Kontragent haqqında məlumat və yenilənməsi:
        private void BtnCounterpartyInfo_Click(object sender, RoutedEventArgs e)
        {
            Counterparty counterparty = db.Counterparties.Find(selectedCounterparty.Id);
            CounterpartyInfo counterpartyInfo = new CounterpartyInfo(this, counterparty);
            counterpartyInfo.ShowDialog();
        }

        // Tarixə görə tapşırıqların axtarış düyməsi:
        private void BtnShowMissions_Click(object sender, RoutedEventArgs e)
        {
            isSelectedCounterparty = false;
            RbFullTime.IsChecked = false;
            RbLastWeek.IsChecked = false;
            RbLastDay.IsChecked = false;
            CbShowAll.IsChecked = true;
            CbShowComments.IsChecked = false;
            SetTimeInterval();
            SearchMissions();
        }

        // Yeni tapşırıq yazılması:
        private void BtnAddMission_Click(object sender, RoutedEventArgs e)
        {
            CbShowComments.IsChecked = false;
            GrdComment.Visibility = Visibility.Hidden;
            DgComments.Visibility = Visibility.Hidden;
            if (GrdMission.Visibility == Visibility.Hidden)
            {
                DgMissions.Height = Double.NaN;
                DgMissions.VerticalAlignment = VerticalAlignment.Stretch;
                DgMissions.Margin = new Thickness(50, 200, 50, 280);
                GrdMission.Visibility = Visibility.Visible;

                if (isSelectedCounterparty)
                {
                    LblMissionAttention.Content = "";
                }
                else
                {
                    LblMissionAttention.Content = "Kontragent seçin.";
                }
            }
            else
            {
                GrdMission.Visibility = Visibility.Hidden;
                DgMissions.Height = Double.NaN;
                DgMissions.VerticalAlignment = VerticalAlignment.Stretch;
                DgMissions.Margin = new Thickness(50, 200, 50, 75);
            }
        }

        // Tapşırığın göndərilməsi:
        private void BtnSendMission_Click(object sender, RoutedEventArgs e)
        {
            if (CmbEmployees.SelectedIndex == -1)
            {
                LblMissionAttention.Content = "İşçi seçin.";
                return;
            }
            if (!isSelectedCounterparty)
            {
                LblMissionAttention.Content = "Kontragent seçin.";
                return;
            }
            if (DpMissionEndDate.SelectedDate.Value.Date == DateTime.Now.Date)
            {
                LblMissionAttention.Content = "Son tarix seçin.";
                return;
            }
            if (string.IsNullOrWhiteSpace(TxtMissionText.Text))
            {
                LblMissionAttention.Content = "Tapşırıq yazın.";
                return;
            }
            LblMissionAttention.Content = "";
            string name = CmbEmployees.SelectedValue.ToString().Split(',')[0];
            string surname = CmbEmployees.SelectedValue.ToString().Split(',')[1].TrimStart();
            Mission mission = new Mission
            {
                CounterpartyID = selectedCounterparty.Id,
                EmployeeID = db.Employees.First(em => em.Name == name && em.Surname == surname).Id,
                Text = TxtMissionText.Text,
                Date = DateTime.Now,
                EndTime = DpMissionEndDate.SelectedDate.Value,
                Completed = false
            };
            db.Missions.Add(mission);
            db.SaveChanges();
            TxtMissionText.Text = "";
            DpMissionEndDate.SelectedDate = DateTime.Now;
            LblMissionAttention.Content = "Tapşırıq yadda saxlanıldı.";
            SearchMissions();
        }

        // Yeni işçi qeydiyyatı:
        private void BtnAddEmployee_Click(object sender, RoutedEventArgs e)
        {

        }

        // Tapşırığın tamamlanmış kimi qeyd olunması:
        private void BtnMarkCompleted_Click(object sender, RoutedEventArgs e)
        {
            db.Missions.Find(selectedMission.Id).Completed = true;
            db.SaveChanges();
            SearchMissions();
        }

        // Yeni rəy yazılması:
        private void BtnAddComment_Click(object sender, RoutedEventArgs e)
        {
            CbShowComments.IsChecked = false;
            GrdMission.Visibility = Visibility.Hidden;
            DgComments.Visibility = Visibility.Hidden;
            if (GrdComment.Visibility == Visibility.Hidden)
            {
                DgMissions.Height = Double.NaN;
                DgMissions.VerticalAlignment = VerticalAlignment.Stretch;
                DgMissions.Margin = new Thickness(50, 200, 50, 220);
                GrdComment.Visibility = Visibility.Visible;
                if (isSelectedMission)
                {
                    TxtBlcMissionText.Text = selectedMission.Text;
                }
                else
                {
                    TxtBlcMissionText.Text = "Tapşırıq seçin.";
                }
            }
            else
            {
                GrdComment.Visibility = Visibility.Hidden;
                DgMissions.Height = Double.NaN;
                DgMissions.VerticalAlignment = VerticalAlignment.Stretch;
                DgMissions.Margin = new Thickness(50, 200, 50, 75);
            }
        }

        // Rəyin göndərilməsi:
        private void BtnSendComment_Click(object sender, RoutedEventArgs e)
        {
            if (!isSelectedMission)
            {
                TxtBlcMissionText.Text = "Tapşırıq seçin.";
                return;
            }
            if (string.IsNullOrWhiteSpace(TxtCommentText.Text))
            {
                LblCommentAttention.Visibility = Visibility.Visible;
                return;
            }
            LblCommentAttention.Visibility = Visibility.Hidden;
            Comment comment = new Comment
            {
                MissionID = selectedMission.Id,
                Text = TxtCommentText.Text,
                Date = DateTime.Now
            };
            db.Comments.Add(comment);
            db.SaveChanges();
            isSelectedMission = false;
            TxtCommentText.Text = "";
        }

        #endregion
    }
}
