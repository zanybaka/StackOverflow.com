using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;

namespace TestNested.Models
{
	public class InvoiceDiscrepancy : INotifyPropertyChanged
	{
		private string _rowType;

		public string RowType
		{
			get => _rowType;
			set
			{
				if (_rowType == value) return;
				_rowType = value;
				RaisePropertyChanged("RowType");
			}
		}

		private string _rowNumber;

		public string RowNumber
		{
			get => _rowNumber;
			set
			{
				if (_rowNumber == value) return;
				_rowNumber = value;
				RaisePropertyChanged("RowNumber");
			}
		}


		private InvoiceDiscrepancyDetails _details;

		public InvoiceDiscrepancyDetails Details
		{
			get => _details;
			set
			{
				if (_details == value) return;
				_details = value;
				RaisePropertyChanged("Details");
			}
		}

		private bool _paymentsExist;
		public bool PaymentsExist
		{
			get => _paymentsExist;
			set
			{
				if (_paymentsExist == value) return;
				_paymentsExist = value;
				RaisePropertyChanged("PaymentsExist");
			}
		}

		private bool _handle;

		public bool Handle
		{
			get => _handle;
			set
			{
				if (_handle == value) return;
				_handle = value;
				RaisePropertyChanged("Handle");
			}
		}

		private PackIconKind _warningIcon;

		public PackIconKind WarningIcon
		{
			get => _warningIcon;
			set
			{
				if (_warningIcon == value) return;
				_warningIcon = value;
				RaisePropertyChanged("WarningIcon");
			}
		}

		private SolidColorBrush _warningColor;

		public SolidColorBrush WarningColor
		{
			get => _warningColor;
			set
			{
				if (_warningColor == value) return;
				_warningColor = value;
				RaisePropertyChanged("WarningIcon");
			}
		}

		private ObservableCollection<Invoice> _invoices;
		public ObservableCollection<Invoice> Invoices
		{
			get => _invoices;
			set
			{
				if (_invoices == value) return;
				_invoices = value;
				RaisePropertyChanged("Invoices");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void RaisePropertyChanged(string property)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
		}
	}
}