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
        CRMEntities db = new CRMEntities();
        
        VwCounterparty selectedCounterparty = new VwCounterparty(); // Axtarış nəticəsində tapılmış və seçilmiş kontragent:

        public MainWindow()
        {
            InitializeComponent();
        }

        // Kontragent cədvəlinin axtarış kriteriyalarına uyğun doldurulması:
        private void FillDataGridCounterparties()
        {
            List<Counterparties> counterparties = db.Counterparties.Where(c =>
                                                                         (TxtCounterparty.Text != "" ? c.Name.Contains(TxtCounterparty.Text) : true) &&
                                                                         (TxtResponsiblePerson.Text != "" ? c.ResponsiblePerson.Contains(TxtResponsiblePerson.Text) : true)).ToList();
            DgCounterparties.Items.Clear();
            foreach (Counterparties counterparty in counterparties)
            {
                DgCounterparties.Items.Add(new VwCounterparty
                {
                    Id = counterparty.Id,
                    Name = counterparty.Name,
                    Person = counterparty.ResponsiblePerson,
                    Position = counterparty.Positions.Name
                });
            }
        }

        // Kontragent cədvəlindən kontragent seçilməsi:
        private void DgCounterparties_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            selectedCounterparty = DgCounterparties.SelectedItem as VwCounterparty;
        }

        #region Buttons

        // Yeni kontragent yaratma düyməsi:
        private void BtnAddCounterparty_Click(object sender, RoutedEventArgs e)
        {
			AddCounterparty addCounterparty = new AddCounterparty();
			addCounterparty.ShowDialog();
        }

        // Axtarış düyməsi:
        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            FillDataGridCounterparties();
        }

        #endregion
    }
}
