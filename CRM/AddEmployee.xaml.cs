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
using System.Windows.Shapes;
using CRM.Models;

namespace CRM
{
    /// <summary>
    /// Interaction logic for AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Window
    {
        CRMEntities db = new CRMEntities();

        private string phone = "";

        private MainWindow crmWindow;

        public AddEmployee(MainWindow mainWindow)
        {
            crmWindow = mainWindow;
            InitializeComponent();
            FillPositions();
        }

        // Vəzifə siyahısının doldurulması:
        private void FillPositions()
        {
            foreach (Position positions in db.Positions.ToList())
            {
                CmbPosition.Items.Add(positions.Name);
            }
            CmbPosition.SelectedIndex = 0;
        }

        // Kontragent məlumatlarının yenilənməsi:
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtEmployeeName.Text))
            {
                TxtEmployeeName.BorderBrush = Brushes.Red;
                TxtEmployeeName.BorderThickness = new Thickness(2);
            }
            if (string.IsNullOrWhiteSpace(TxtEmployeeSurname.Text))
            {
                TxtEmployeeSurname.BorderBrush = Brushes.Red;
                TxtEmployeeSurname.BorderThickness = new Thickness(2);
            }
            if (string.IsNullOrWhiteSpace(TxtPhone.Text))
            {
                TxtPhone.BorderBrush = Brushes.Red;
                TxtPhone.BorderThickness = new Thickness(2);
            }
            if (string.IsNullOrWhiteSpace(TxtEmployeeName.Text) ||
                string.IsNullOrWhiteSpace(TxtEmployeeSurname.Text) ||
                string.IsNullOrWhiteSpace(TxtPhone.Text))
            {
                TxtBlcAttention.Text = "Qırmızı sahələr doldurulmalıdır";
                return;
            }
            if (CmbPosition.SelectedItem == null)
            {
                MessageBox.Show("Vəzifə seçin");
                return;
            }
            TxtBlcAttention.Text = "";
            if (db.Employees.Count(c => c.Phone == TxtPhone.Text) > 0)
            {
                TxtBlcAttention.Text = "Bu telefon nömrəsi artıq qeyd olunub";
                TxtPhone.Focus();
                return;
            }

            Employee employee = new Employee
            {
                Name = TxtEmployeeName.Text,
                Surname = TxtEmployeeSurname.Text,
                Phone = TxtPhone.Text,
                PositionID = db.Positions.First(p => p.Name == CmbPosition.SelectedValue.ToString()).Id,
                RecruitmentDate = DpRecruitmentDate.SelectedDate.Value.Date,
            };

            db.Employees.Add(employee);
            db.SaveChanges();
            TxtBlcAttention.Text = "Yeni işçi əlavə olundu.";
            crmWindow.Refresh();
        }

        // Kontragentin məlumat bazasından silinməsi:
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            phone = "";
            TxtEmployeeName.Text = "";
            TxtEmployeeSurname.Text = "";
            CmbPosition.SelectedIndex = -1;
            TxtPhone.Text = "";
            TxtEmployeeName.BorderBrush = Brushes.DarkCyan;
            TxtEmployeeName.BorderThickness = new Thickness(1);
            TxtEmployeeSurname.BorderBrush = Brushes.DarkCyan;
            TxtEmployeeSurname.BorderThickness = new Thickness(1);
            TxtPhone.BorderBrush = Brushes.DarkCyan;
            TxtPhone.BorderThickness = new Thickness(1);
            TxtBlcAttention.Text = "";
        }

        // Səhf verilən üzrə yoxlanılmış sahələrə fokuslanma:
        #region GotFocus

        private void TxtEmployeeName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TxtEmployeeName.BorderBrush == Brushes.Red)
            {
                TxtEmployeeName.BorderBrush = Brushes.DarkCyan;
                TxtEmployeeName.BorderThickness = new Thickness(1);
            }
        }

        private void TxtEmployeeSurname_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TxtEmployeeSurname.BorderBrush == Brushes.Red)
            {
                TxtEmployeeSurname.BorderBrush = Brushes.DarkCyan;
                TxtEmployeeSurname.BorderThickness = new Thickness(1);
            }
        }

        private void TxtPhone_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TxtPhone.BorderBrush == Brushes.Red)
            {
                TxtPhone.BorderBrush = Brushes.DarkCyan;
                TxtPhone.BorderThickness = new Thickness(1);
            }
        }
        
        #endregion

        // Nömrələrin ədəd olaraq daxil edilməsi:
        #region Write phone numbers

        private void TxtPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TxtPhone.Text.Length == 0)
            {
                phone = "";
            }
            if (!decimal.TryParse(TxtPhone.Text, out decimal number) || TxtPhone.Text.Length > 10)
            {
                TxtPhone.Text = phone;
            }
            else
            {
                phone = TxtPhone.Text;
            }
        }

        #endregion

    }
}
