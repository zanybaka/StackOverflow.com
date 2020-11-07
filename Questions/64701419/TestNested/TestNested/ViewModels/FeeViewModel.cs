using GalaSoft.MvvmLight.CommandWpf;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using TestNested.Models;

namespace TestNested.ViewModels
{
	public class FeeViewModel : ViewModelsBase
	{
		private BackgroundWorker _paymentWorker;

		public RelayCommand LoadPaymentsCommand { get; private set; }


		private ObservableCollection<InvoiceDiscrepancy> _invoiceDiscrepancies = new ObservableCollection<InvoiceDiscrepancy>();
		public ObservableCollection<InvoiceDiscrepancy> InvoiceDiscrepancies
		{
			get => _invoiceDiscrepancies;
			set
			{
				if (_invoiceDiscrepancies != value)
				{
					_invoiceDiscrepancies = value;
					RaisePropertyChanged(() => InvoiceDiscrepancies);
				}
			}
		}

		public ICollectionView InvoiceDiscrepanciesView { get; private set; }

		private InvoiceDiscrepancy _selectedDiscrepancy { get; set; }
		public InvoiceDiscrepancy SelectedDiscrepancy
		{
			get => _selectedDiscrepancy;
			set
			{
				if (_selectedDiscrepancy != value)
				{
					_selectedDiscrepancy = value;
					RaisePropertyChanged(() => SelectedDiscrepancy);
				}
			}
		}

		private Invoice _selectedInvoice { get; set; }
		public Invoice SelectedInvoice
		{
			get => _selectedInvoice;
			set
			{
                if (_selectedInvoice != value)
                {
                    if (_selectedInvoice != null) // <- add the 'if' block
                    {
                        _selectedInvoice = null;
                        RaisePropertyChanged(() => SelectedInvoice);
                    }
                    _selectedInvoice = value;
                    RaisePropertyChanged(() => SelectedInvoice);
                }
			}
		}

		
		public FeeViewModel()
		{
			LoadPaymentsCommand = new RelayCommand(LoadPayments, CanLoadPayments);

			_paymentWorker = new BackgroundWorker()
			{
				WorkerReportsProgress = true
			};
			_paymentWorker.DoWork += PaymentWorker_DoWork;
			_paymentWorker.ProgressChanged += PaymentWorker_ProgressChanged;
			_paymentWorker.RunWorkerCompleted += PaymentWorker_RunWorkerCompleted;

			InvoiceDiscrepanciesView = CollectionViewSource.GetDefaultView(InvoiceDiscrepancies);
			if (InvoiceDiscrepanciesView != null && InvoiceDiscrepanciesView.CanGroup == true)
			{
				InvoiceDiscrepanciesView.GroupDescriptions.Clear();
				InvoiceDiscrepanciesView.GroupDescriptions.Add(new PropertyGroupDescription("RowNumber"));
			}
		}

		private void PaymentWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			var open = true;

			if (open)
			{
				int count = 0;
				for(int i = 0; i < 10; i++)
				{
					Application.Current.Dispatcher.Invoke(delegate
					{
						var discrepancy = new InvoiceDiscrepancy()
						{
							Invoices = new ObservableCollection<Invoice>()
							{
								new Invoice()
								{
									CustomerName = "Test Johnsson",
									DocumentNumber = count++.ToString()
								},
								new Invoice()
								{
									CustomerName = "Test Johnsson",
									DocumentNumber = count++.ToString()
								}
							},
							Details = new InvoiceDiscrepancyDetails()
							{
								CustomerName = "Test Johnsson"
							},
							Handle = false,
							PaymentsExist = false,
							RowNumber = i.ToString(),
							RowType = "Order",
							WarningColor = Brushes.Green,
							WarningIcon = PackIconKind.CheckCircle
						};
						(sender as BackgroundWorker).ReportProgress(i, discrepancy);
					});
				}				
			}
		}

		private void PaymentWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{

			if (e.UserState is InvoiceDiscrepancy invoiceDiscrepancy)
			{
				_invoiceDiscrepancies.Add(invoiceDiscrepancy);
			}
			else if (e.UserState is DateTime dateTime)
			{
			}
			else
			{
			}
		}

		private void PaymentWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Error != null)
			{
				MessageBox.Show(e.Error.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			GC.Collect();
		}

		

		public void LoadPayments()
		{
			_invoiceDiscrepancies.Clear();
			try
			{
				_paymentWorker.RunWorkerAsync();
			}
			catch (InvalidOperationException ex)
			{
			}
		}

		public bool CanLoadPayments()
		{
			if (_paymentWorker.IsBusy)
			{
				return false;
			}
			return true;
		}
	}
}
