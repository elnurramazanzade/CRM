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
	/// Interaction logic for AddCounterparty.xaml
	/// </summary>
	public partial class AddCounterparty : Window
	{
        CRMEntities db = new CRMEntities();

        private string phone = "";
        private string mobile = "";

		public AddCounterparty()
		{
			InitializeComponent();
            FillPositions();
		}

        // Vəzifə siyahısının doldurulması:
        private void FillPositions()
        {
            foreach (Positions positions in db.Positions.ToList())
            {
                CmbPosition.Items.Add(positions.Name);
            }
        }

        // Yeni kontragentin yaddaşda saxlanılması:
        private void Save_Click(object sender, RoutedEventArgs e)
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
                LblAttention.Visibility = Visibility.Visible;
                return;
            }
            if (CmbPosition.SelectedItem == null)
            {
                MessageBox.Show("Vəzifə seçin");
                return;
            }
            if (db.Counterparties.Count(c => c.Phone == TxtPhone.Text) > 0)
            {
                MessageBox.Show("Bu telefon nömrəsi artıq qeyd olunub");
                TxtPhone.Focus();
                return;
            }
            Counterparties counterparty = new Counterparties
            {
                Name = TxtCounterpartyName.Text,
                ResponsiblePerson = TxtResponsiblePerson.Text,
                PositionID = CmbPosition.SelectedIndex + 1,
                Phone = TxtPhone.Text,
                Mobile = TxtMobile.Text,
                Address = TxtAddress.Text
            };
            db.Counterparties.Add(counterparty);
            db.SaveChanges();
            MessageBox.Show("Yeni kontragent yaddaşa yazıldı");
        }

        // Formun təmizlənməsi:
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            phone = "";
            mobile = "";
            TxtCounterpartyName.Text = "";
            TxtResponsiblePerson.Text = "";
            CmbPosition.SelectedIndex = -1;
            TxtPhone.Text = "";
            TxtMobile.Text = "";
            TxtAddress.Text = "";
            TxtCounterpartyName.BorderBrush = Brushes.DarkCyan;
            TxtCounterpartyName.BorderThickness = new Thickness(1);
            TxtResponsiblePerson.BorderBrush = Brushes.DarkCyan;
            TxtResponsiblePerson.BorderThickness = new Thickness(1);
            TxtPhone.BorderBrush = Brushes.DarkCyan;
            TxtPhone.BorderThickness = new Thickness(1);
            TxtAddress.BorderBrush = Brushes.DarkCyan;
            TxtAddress.BorderThickness = new Thickness(1);
            LblAttention.Visibility = Visibility.Hidden;
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
