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
    /// Interaction logic for CounterpartyInfo.xaml
    /// </summary>
    public partial class CounterpartyInfo : Window
    {
        CRMEntities db = new CRMEntities();

        private string phone = "";
        private string mobile = "";

        private MainWindow crmWindow;
        private Counterparty selectedCounterparty;

        public CounterpartyInfo(MainWindow mainWindow, Counterparty counterparty)
        {
            crmWindow = mainWindow;
            selectedCounterparty = counterparty;
            InitializeComponent();
            FillPositions();
            FillDatas();
        }

        // Vəzifə siyahısının doldurulması:
        private void FillPositions()
        {
            foreach (Position positions in db.Positions.ToList())
            {
                CmbPosition.Items.Add(positions.Name);
            }
            CmbPosition.SelectedIndex = selectedCounterparty.PositionID - 1;
        }

        // Məlumatların doldurulması:
        private void FillDatas()
        {
            TxtCounterpartyName.Text = selectedCounterparty.Name;
            TxtResponsiblePerson.Text = selectedCounterparty.ResponsiblePerson;
            TxtPhone.Text = selectedCounterparty.Phone;
            TxtMobile.Text = selectedCounterparty.Mobile;
            TxtAddress.Text = selectedCounterparty.Address;
        }

        // Kontragent məlumatlarının yenilənməsi:
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtCounterpartyName.Text))
            {
                TxtCounterpartyName.BorderBrush = Brushes.Red;
                TxtCounterpartyName.BorderThickness = new Thickness(2);
            }
            if (string.IsNullOrWhiteSpace(TxtResponsiblePerson.Text))
            {
                TxtResponsiblePerson.BorderBrush = Brushes.Red;
                TxtResponsiblePerson.BorderThickness = new Thickness(2);
            }
            if (string.IsNullOrWhiteSpace(TxtPhone.Text))
            {
                TxtPhone.BorderBrush = Brushes.Red;
                TxtPhone.BorderThickness = new Thickness(2);
            }
            if (string.IsNullOrWhiteSpace(TxtAddress.Text))
            {
                TxtAddress.BorderBrush = Brushes.Red;
                TxtAddress.BorderThickness = new Thickness(2);
            }
            if (string.IsNullOrWhiteSpace(TxtCounterpartyName.Text) ||
                string.IsNullOrWhiteSpace(TxtResponsiblePerson.Text) ||
                string.IsNullOrWhiteSpace(TxtPhone.Text) ||
                string.IsNullOrWhiteSpace(TxtAddress.Text))
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
            if (db.Counterparties.Count(c => c.Phone == TxtPhone.Text) > 0 && TxtPhone.Text != selectedCounterparty.Phone)
            {
                TxtBlcAttention.Text = "Bu telefon nömrəsi artıq qeyd olunub";
                TxtPhone.Focus();
                return;
            }
            if (db.Counterparties.Count(c => c.Mobile == TxtMobile.Text) > 0 && TxtMobile.Text != selectedCounterparty.Mobile)
            {
                TxtBlcAttention.Text = "Bu telefon nömrəsi artıq qeyd olunub";
                TxtMobile.Focus();
                return;
            }
            Counterparty counterparty = db.Counterparties.Find(selectedCounterparty.Id);
            counterparty.Name = TxtCounterpartyName.Text;
            counterparty.ResponsiblePerson = TxtResponsiblePerson.Text;
            counterparty.PositionID = CmbPosition.SelectedIndex + 1;
            counterparty.Phone = TxtPhone.Text;
            counterparty.Mobile = TxtMobile.Text;
            counterparty.Address = TxtAddress.Text;
            db.SaveChanges();
            TxtBlcAttention.Text = "Kontragent yeniləndi";
            crmWindow.Refresh();
        }

        // Kontragentin məlumat bazasından silinməsi:
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Counterparty counterparty = db.Counterparties.Find(selectedCounterparty.Id);
            db.Counterparties.Remove(counterparty);
            db.SaveChanges();
            TxtBlcAttention.Text = "Kontragent silindi";
            crmWindow.Refresh();
        }
        
        // Səhf verilən üzrə yoxlanılmış sahələrə fokuslanma:
        #region GotFocus
        private void TxtCounterpartyName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TxtCounterpartyName.BorderBrush == Brushes.Red)
            {
                TxtCounterpartyName.BorderBrush = Brushes.DarkCyan;
                TxtCounterpartyName.BorderThickness = new Thickness(1);
            }
        }

        private void TxtResponsiblePerson_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TxtResponsiblePerson.BorderBrush == Brushes.Red)
            {
                TxtResponsiblePerson.BorderBrush = Brushes.DarkCyan;
                TxtResponsiblePerson.BorderThickness = new Thickness(1);
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

        private void TxtAddress_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TxtAddress.BorderBrush == Brushes.Red)
            {
                TxtAddress.BorderBrush = Brushes.DarkCyan;
                TxtAddress.BorderThickness = new Thickness(1);
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

        private void TxtMobile_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TxtMobile.Text.Length == 0)
            {
                mobile = "";
            }
            if (!decimal.TryParse(TxtMobile.Text, out decimal number) || TxtMobile.Text.Length > 10)
            {
                TxtMobile.Text = mobile;
            }
            else
            {
                mobile = TxtMobile.Text;
            }
        }
        #endregion

    }
}
