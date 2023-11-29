using Csla;
using SurgeonPortal.DataAccess.Contracts.Billing;
using SurgeonPortal.Library.Contracts.Billing;
using System;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;

namespace SurgeonPortal.Library.Billing
{
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
	[Serializable]
	public class GenerateTransactionTokenCommand : YtgCommandBase<GenerateTransactionTokenCommand, int>, IGenerateTransactionTokenCommand
	{
		private readonly IGenerateTransactionTokenCommandDal _generateTransactionTokenCommandDal;

		public GenerateTransactionTokenCommand(
			IIdentityProvider identity,
			IGenerateTransactionTokenCommandDal generateTransactionTokenCommandDal) 
			: base(identity)
		{
			_generateTransactionTokenCommandDal = generateTransactionTokenCommandDal;
		}

		public static bool CanExecuteCommand()
		{
			return true;
		}

		public decimal Amount
		{
			get { return ReadProperty(AmountProperty); }
			internal set { LoadProperty(AmountProperty, value); }
		}
		public static readonly PropertyInfo<decimal> AmountProperty = RegisterProperty<decimal>(c => c.Amount);

		public string InvoiceNumber
		{
			get { return ReadProperty(InvoiceNumberProperty); }
			internal set { LoadProperty(InvoiceNumberProperty, value); }
		}
		public static readonly PropertyInfo<string> InvoiceNumberProperty = RegisterProperty<string>(c => c.InvoiceNumber);

		public int Quantity
		{
			get { return ReadProperty(QuantityProperty); }
			internal set { LoadProperty(QuantityProperty, value); }
		}
		public static readonly PropertyInfo<int> QuantityProperty = RegisterProperty<int>(c => c.Quantity);

		public string Description
		{
			get { return ReadProperty(DescriptionProperty); }
			internal set { LoadProperty(DescriptionProperty, value); }
		}
		public static readonly PropertyInfo<string> DescriptionProperty = RegisterProperty<string>(c => c.Description);

		public decimal CostPerUnit
		{
			get { return ReadProperty(CostPerUnitProperty); }
			internal set { LoadProperty(CostPerUnitProperty, value); }
		}
		public static readonly PropertyInfo<decimal> CostPerUnitProperty = RegisterProperty<decimal>(c => c.CostPerUnit);

		public string FirstName
		{
			get { return ReadProperty(FirstNameProperty); }
			internal set { LoadProperty(FirstNameProperty, value); }
		}
		public static readonly PropertyInfo<string> FirstNameProperty = RegisterProperty<string>(c => c.FirstName);

		public string LastName
		{
			get { return ReadProperty(LastNameProperty); }
			internal set { LoadProperty(LastNameProperty, value); }
		}
		public static readonly PropertyInfo<string> LastNameProperty = RegisterProperty<string>(c => c.LastName);

		public string Email
		{
			get { return ReadProperty(EmailProperty); }
			internal set { LoadProperty(EmailProperty, value); }
		}
		public static readonly PropertyInfo<string> EmailProperty = RegisterProperty<string>(c => c.Email);

		public string PhoneNumber
		{
			get { return ReadProperty(PhoneNumberProperty); }
			internal set { LoadProperty(PhoneNumberProperty, value); }
		}
		public static readonly PropertyInfo<string> PhoneNumberProperty = RegisterProperty<string>(c => c.PhoneNumber);

		public string AddressLine1
		{
			get { return ReadProperty(AddressLine1Property); }
			internal set { LoadProperty(AddressLine1Property, value); }
		}
		public static readonly PropertyInfo<string> AddressLine1Property = RegisterProperty<string>(c => c.AddressLine1);

		public string AddressLine2
		{
			get { return ReadProperty(AddressLine2Property); }
			internal set { LoadProperty(AddressLine2Property, value); }
		}
		public static readonly PropertyInfo<string> AddressLine2Property = RegisterProperty<string>(c => c.AddressLine2);

		public string City
		{
			get { return ReadProperty(CityProperty); }
			internal set { LoadProperty(CityProperty, value); }
		}
		public static readonly PropertyInfo<string> CityProperty = RegisterProperty<string>(c => c.City);

		public string State
		{
			get { return ReadProperty(StateProperty); }
			internal set { LoadProperty(StateProperty, value); }
		}
		public static readonly PropertyInfo<string> StateProperty = RegisterProperty<string>(c => c.State);

		public string ZipCode
		{
			get { return ReadProperty(ZipCodeProperty); }
			internal set { LoadProperty(ZipCodeProperty, value); }
		}
		public static readonly PropertyInfo<string> ZipCodeProperty = RegisterProperty<string>(c => c.ZipCode);

		public string CallbackUrl
		{
			get { return ReadProperty(CallbackUrlProperty); }
			internal set { LoadProperty(CallbackUrlProperty, value); }
		}
		public static readonly PropertyInfo<string> CallbackUrlProperty = RegisterProperty<string>(c => c.CallbackUrl);

		public string TransactionToken
		{
			get { return ReadProperty(TransactionTokenProperty); }
			internal set { LoadProperty(TransactionTokenProperty, value); }
		}
		public static readonly PropertyInfo<string> TransactionTokenProperty = RegisterProperty<string>(c => c.TransactionToken);

		[Execute]
		protected async Task ExecuteCommand()
		{
			var dto = await _generateTransactionTokenCommandDal.GenerateTransactionTokenAsync(Amount, InvoiceNumber, Quantity, Description, CostPerUnit, FirstName, LastName, Email, PhoneNumber, AddressLine1, AddressLine2, City, State, ZipCode, CallbackUrl);
			TransactionToken = dto.TransactionToken;
		}
	}
}
