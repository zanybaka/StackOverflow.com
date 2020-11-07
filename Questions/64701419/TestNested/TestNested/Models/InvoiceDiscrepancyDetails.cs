using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace TestNested.Models
{
	public class InvoiceDiscrepancyDetails : INotifyPropertyChanged
	{
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