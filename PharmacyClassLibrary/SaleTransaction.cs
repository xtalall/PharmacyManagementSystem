using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyClassLibrary
{
    public class SaleTransaction
    {
        private int transactionID;
        private Customer customerInfo;
        private List<Medicine> medicineList;
        private double totalPrice;
        private DateTime transactionDate;
        private string pharmacistName;

        public int TransactionID
        {
            get { return transactionID; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("TransactionID must be greater than zero.");
                }
                transactionID = value;
            }
        }

        public Customer CustomerInfo
        {
            get { return customerInfo; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("CustomerInfo cannot be null.");
                }
                customerInfo = value;
            }
        }

        public List<Medicine> MedicineList
        {
            get { return medicineList; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("MedicineList cannot be null.");
                }
                medicineList = value;
            }
        }

        public double TotalPrice
        {
            get { return totalPrice; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("TotalPrice cannot be negative.");
                }
                totalPrice = value;
            }
        }

        public DateTime TransactionDate
        {
            get { return transactionDate; }
            set
            {
                if (value == DateTime.MinValue)
                {
                    throw new ArgumentException("TransactionDate must be a valid date.");
                }
                transactionDate = value;
            }
        }

        public string PharmacistName
        {
            get { return pharmacistName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("PharmacistName cannot be empty.");
                }
                pharmacistName = value;
            }
        }

        public double CalculateTotal()
        {
            return 0.0;
        }

        public void UpdateInventory()
        {
        }

        public bool VerifyPrescriptionRequirements()
        {
            return false;
        }
        public SaleTransaction()
        {

        }

    }
}
