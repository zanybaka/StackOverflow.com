using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNested.Models
{
	public class Invoice : INotifyPropertyChanged
	{
		private string _documentNumber;
		public string DocumentNumber
		{
			get => _documentNumber;
			set
			{
				if (_documentNumber == value) return;
				_documentNumber = value;
				RaisePropertyChanged("DocumentNumber");
			}
		}

		private string _customerName;
		public string CustomerName
		{
			get => _customerName;
			set
			{
				if (_customerName == value) return;
				_customerName = value;
				RaisePropertyChanged("CustomerName");
			}
		}


		public event PropertyChangedEventHandler PropertyChanged;

		private void RaisePropertyChanged(string property)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
		}
	}
}
