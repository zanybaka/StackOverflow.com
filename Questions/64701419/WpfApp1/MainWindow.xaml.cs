using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using WpfApp1.Annotations;

namespace WpfApp1
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private Discrepancy _selectedDiscrepancy;

        public Discrepancy SelectedDiscrepancy
        {
            get => _selectedDiscrepancy;
            set
            {
                _selectedDiscrepancy = value;
                OnPropertyChanged(nameof(SelectedDiscrepancy));
            }
        }

        public ListCollectionView InvoiceDiscrepancies;

        public MainWindow()
        {
            InitializeComponent();
            List<Discrepancy> list = new List<Discrepancy>();
            list.Add(new Discrepancy()
            {
                Name = "Name1",
                Invoices = new Invoice[]
                {
                    new Invoice() { ID = "ID1 " },
                    new Invoice() { ID = "ID2 " },
                }
            });
            list.Add(new Discrepancy()
            {
                Name = "Name2",
                Invoices = new Invoice[]
                {
                    new Invoice() { ID = "ID3 " },
                }
            });
            InvoiceDiscrepancies = new ListCollectionView(list);
            grid.ItemsSource = InvoiceDiscrepancies;
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Discrepancy : INotifyPropertyChanged
    {
        public string Name { get; set; }

        private Invoice _selectedInvoice;

        public Invoice SelectedInvoice
        {
            get => _selectedInvoice;
            set
            {
                _selectedInvoice = value;
                OnPropertyChanged(nameof(SelectedInvoice));
            }
        }

        public Invoice[] Invoices { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Invoice
    {
        public string ID { get; set; }
    }
}