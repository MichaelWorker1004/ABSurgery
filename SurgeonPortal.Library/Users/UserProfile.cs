using Csla;
using Csla.Rules.CommonRules;
using SurgeonPortal.DataAccess.Contracts.Users;
using SurgeonPortal.Library.Contracts.Users;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;
using static SurgeonPortal.Library.Users.UserProfileFactory;

namespace SurgeonPortal.Library.Users
{
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
  [Serializable]
  [DataContract]
  public class UserProfile : YtgBusinessBase<UserProfile>, IUserProfile
  {
    private readonly IUserProfileDal _userProfileDal;

    public UserProfile(
        IIdentityProvider identityProvider,
        IUserProfileDal userProfileDal)
        : base(identityProvider)
    {
      _userProfileDal = userProfileDal;
    }

    [Key]
    [DisplayName(nameof(UserProfileId))]
    public int UserProfileId
    {
      get { return GetProperty(UserProfileIdProperty); }
      set { SetProperty(UserProfileIdProperty, value); }
    }
    public static readonly PropertyInfo<int> UserProfileIdProperty = RegisterProperty<int>(c => c.UserProfileId);

    [DisplayName(nameof(UserId))]
    public int UserId
    {
      get { return GetProperty(UserIdProperty); }
      set { SetProperty(UserIdProperty, value); }
    }
    public static readonly PropertyInfo<int> UserIdProperty = RegisterProperty<int>(c => c.UserId);

    [DisplayName(nameof(FirstName))]
    public string FirstName
    {
      get { return GetProperty(FirstNameProperty); }
      set { SetProperty(FirstNameProperty, value); }
    }
    public static readonly PropertyInfo<string> FirstNameProperty = RegisterProperty<string>(c => c.FirstName);

    [DisplayName(nameof(MiddleName))]
    public string MiddleName
    {
      get { return GetProperty(MiddleNameProperty); }
      set { SetProperty(MiddleNameProperty, value); }
    }
    public static readonly PropertyInfo<string> MiddleNameProperty = RegisterProperty<string>(c => c.MiddleName);

    [DisplayName(nameof(LastName))]
    public string LastName
    {
      get { return GetProperty(LastNameProperty); }
      set { SetProperty(LastNameProperty, value); }
    }
    public static readonly PropertyInfo<string> LastNameProperty = RegisterProperty<string>(c => c.LastName);

    [DisplayName(nameof(Suffix))]
    public string Suffix
    {
      get { return GetProperty(SuffixProperty); }
      set { SetProperty(SuffixProperty, value); }
    }
    public static readonly PropertyInfo<string> SuffixProperty = RegisterProperty<string>(c => c.Suffix);

    [DisplayName(nameof(DisplayName))]
    public string DisplayName
    {
      get { return GetProperty(DisplayNameProperty); }
      set { SetProperty(DisplayNameProperty, value); }
    }
    public static readonly PropertyInfo<string> DisplayNameProperty = RegisterProperty<string>(c => c.DisplayName);

    [DisplayName(nameof(OfficePhoneNumber))]
    public string OfficePhoneNumber
    {
      get { return GetProperty(OfficePhoneNumberProperty); }
      set { SetProperty(OfficePhoneNumberProperty, value); }
    }
    public static readonly PropertyInfo<string> OfficePhoneNumberProperty = RegisterProperty<string>(c => c.OfficePhoneNumber);

    [DisplayName(nameof(MobilePhoneNumber))]
    public string MobilePhoneNumber
    {
      get { return GetProperty(MobilePhoneNumberProperty); }
      set { SetProperty(MobilePhoneNumberProperty, value); }
    }
    public static readonly PropertyInfo<string> MobilePhoneNumberProperty = RegisterProperty<string>(c => c.MobilePhoneNumber);

    [DisplayName(nameof(BirthCity))]
    public string BirthCity
    {
      get { return GetProperty(BirthCityProperty); }
      set { SetProperty(BirthCityProperty, value); }
    }
    public static readonly PropertyInfo<string> BirthCityProperty = RegisterProperty<string>(c => c.BirthCity);

    [DisplayName(nameof(BirthState))]
    public string BirthState
    {
      get { return GetProperty(BirthStateProperty); }
      set { SetProperty(BirthStateProperty, value); }
    }
    public static readonly PropertyInfo<string> BirthStateProperty = RegisterProperty<string>(c => c.BirthState);

    [DisplayName(nameof(BirthCountry))]
    public string BirthCountry
    {
      get { return GetProperty(BirthCountryProperty); }
      set { SetProperty(BirthCountryProperty, value); }
    }
    public static readonly PropertyInfo<string> BirthCountryProperty = RegisterProperty<string>(c => c.BirthCountry);

    [DisplayName(nameof(CountryCitizenship))]
    public string CountryCitizenship
    {
      get { return GetProperty(CountryCitizenshipProperty); }
      set { SetProperty(CountryCitizenshipProperty, value); }
    }
    public static readonly PropertyInfo<string> CountryCitizenshipProperty = RegisterProperty<string>(c => c.CountryCitizenship);

    [DisplayName(nameof(AbsId))]
    public string AbsId
    {
      get { return GetProperty(AbsIdProperty); }
      private set { SetProperty(AbsIdProperty, value); }
    }
    public static readonly PropertyInfo<string> AbsIdProperty = RegisterProperty<string>(c => c.AbsId);

    [DisplayName(nameof(CertificationStatus))]
    public string CertificationStatus
    {
      get { return GetProperty(CertificationStatusProperty); }
      private set { SetProperty(CertificationStatusProperty, value); }
    }
    public static readonly PropertyInfo<string> CertificationStatusProperty = RegisterProperty<string>(c => c.CertificationStatus);

    [DisplayName(nameof(NPI))]
    public string NPI
    {
      get { return GetProperty(NPIProperty); }
      private set { SetProperty(NPIProperty, value); }
    }
    public static readonly PropertyInfo<string> NPIProperty = RegisterProperty<string>(c => c.NPI);

    [DisplayName(nameof(GenderId))]
    public int? GenderId
    {
      get { return GetProperty(GenderIdProperty); }
      set { SetProperty(GenderIdProperty, value); }
    }
    public static readonly PropertyInfo<int?> GenderIdProperty = RegisterProperty<int?>(c => c.GenderId);

    [DisplayName(nameof(BirthDate))]
    public DateTime? BirthDate
    {
      get { return GetProperty(BirthDateProperty); }
      set { SetProperty(BirthDateProperty, value); }
    }
    public static readonly PropertyInfo<DateTime?> BirthDateProperty = RegisterProperty<DateTime?>(c => c.BirthDate);

    [DisplayName(nameof(Race))]
    public string Race
    {
      get { return GetProperty(RaceProperty); }
      set { SetProperty(RaceProperty, value); }
    }
    public static readonly PropertyInfo<string> RaceProperty = RegisterProperty<string>(c => c.Race);

    [DisplayName(nameof(Ethnicity))]
    public string Ethnicity
    {
      get { return GetProperty(EthnicityProperty); }
      set { SetProperty(EthnicityProperty, value); }
    }
    public static readonly PropertyInfo<string> EthnicityProperty = RegisterProperty<string>(c => c.Ethnicity);

    [DisplayName(nameof(FirstLanguageId))]
    public int? FirstLanguageId
    {
      get { return GetProperty(FirstLanguageIdProperty); }
      set { SetProperty(FirstLanguageIdProperty, value); }
    }
    public static readonly PropertyInfo<int?> FirstLanguageIdProperty = RegisterProperty<int?>(c => c.FirstLanguageId);

    [DisplayName(nameof(BestLanguageId))]
    public int? BestLanguageId
    {
      get { return GetProperty(BestLanguageIdProperty); }
      set { SetProperty(BestLanguageIdProperty, value); }
    }
    public static readonly PropertyInfo<int?> BestLanguageIdProperty = RegisterProperty<int?>(c => c.BestLanguageId);

    [DisplayName(nameof(ReceiveComms))]
    public bool? ReceiveComms
    {
      get { return GetProperty(ReceiveCommsProperty); }
      set { SetProperty(ReceiveCommsProperty, value); }
    }
    public static readonly PropertyInfo<bool?> ReceiveCommsProperty = RegisterProperty<bool?>(c => c.ReceiveComms);

    [DisplayName(nameof(UserConfirmed))]
    public bool? UserConfirmed
    {
      get { return GetProperty(UserConfirmedProperty); }
      set { SetProperty(UserConfirmedProperty, value); }
    }
    public static readonly PropertyInfo<bool?> UserConfirmedProperty = RegisterProperty<bool?>(c => c.UserConfirmed);

    [DisplayName(nameof(UserConfirmedDate))]
    public DateTime? UserConfirmedDate
    {
      get { return GetProperty(UserConfirmedDateProperty); }
      set { SetProperty(UserConfirmedDateProperty, value); }
    }
    public static readonly PropertyInfo<DateTime?> UserConfirmedDateProperty = RegisterProperty<DateTime?>(c => c.UserConfirmedDate);

    [DisplayName(nameof(Street1))]
    public string Street1
    {
      get { return GetProperty(Street1Property); }
      set { SetProperty(Street1Property, value); }
    }
    public static readonly PropertyInfo<string> Street1Property = RegisterProperty<string>(c => c.Street1);

    [DisplayName(nameof(Street2))]
    public string Street2
    {
      get { return GetProperty(Street2Property); }
      set { SetProperty(Street2Property, value); }
    }
    public static readonly PropertyInfo<string> Street2Property = RegisterProperty<string>(c => c.Street2);

    [DisplayName(nameof(City))]
    public string City
    {
      get { return GetProperty(CityProperty); }
      set { SetProperty(CityProperty, value); }
    }
    public static readonly PropertyInfo<string> CityProperty = RegisterProperty<string>(c => c.City);

    [DisplayName(nameof(State))]
    public string State
    {
      get { return GetProperty(StateProperty); }
      set { SetProperty(StateProperty, value); }
    }
    public static readonly PropertyInfo<string> StateProperty = RegisterProperty<string>(c => c.State);

    [DisplayName(nameof(ZipCode))]
    public string ZipCode
    {
      get { return GetProperty(ZipCodeProperty); }
      set { SetProperty(ZipCodeProperty, value); }
    }
    public static readonly PropertyInfo<string> ZipCodeProperty = RegisterProperty<string>(c => c.ZipCode);

    [DisplayName(nameof(Country))]
    public string Country
    {
      get { return GetProperty(CountryProperty); }
      set { SetProperty(CountryProperty, value); }
    }
    public static readonly PropertyInfo<string> CountryProperty = RegisterProperty<string>(c => c.Country);



    /// <summary>
    /// This method is used to apply authorization rules on the object
    /// </summary>
    [ObjectAuthorizationRules]
    public static void AddObjectAuthorizationRules()
    {


    }



    /// <summary>
    /// This method is used to add business rules to the Csla 
    /// business rule engine
    /// </summary>
    protected override void AddBusinessRules()
    {
      // Only process priority 5 and higher if all 4 and lower completed first
      BusinessRules.ProcessThroughPriority = 4;

      BusinessRules.AddRule(new Required(FirstNameProperty, "FirstName is required"));
      BusinessRules.AddRule(new Required(LastNameProperty, "LastName is required"));
      BusinessRules.AddRule(new Required(DisplayNameProperty, "DisplayName is required"));
      BusinessRules.AddRule(new Required(OfficePhoneNumberProperty, "OfficePhoneNumber is required"));
      BusinessRules.AddRule(new Required(BirthCityProperty, "BirthCity is required"));
      BusinessRules.AddRule(new Required(BirthStateProperty, "BirthState is required"));
      BusinessRules.AddRule(new Required(BirthCountryProperty, "BirthCountry is required"));
      BusinessRules.AddRule(new Required(CountryCitizenshipProperty, "CountryCitizenship is required"));
      BusinessRules.AddRule(new Required(GenderIdProperty, "GenderId is required"));
      BusinessRules.AddRule(new Required(BirthDateProperty, "BirthDate is required"));
      BusinessRules.AddRule(new Required(RaceProperty, "Race is required"));
      BusinessRules.AddRule(new Required(EthnicityProperty, "Ethnicity is required"));
      BusinessRules.AddRule(new Required(FirstLanguageIdProperty, "FirstLanguageId is required"));
      BusinessRules.AddRule(new Required(BestLanguageIdProperty, "BestLanguageId is required"));
      BusinessRules.AddRule(new Required(ReceiveCommsProperty, "ReceiveComms is required"));
      BusinessRules.AddRule(new Required(Street1Property, "Street1 is required"));
      BusinessRules.AddRule(new Required(CityProperty, "City is required"));
      BusinessRules.AddRule(new Required(StateProperty, "State is required"));
      BusinessRules.AddRule(new Required(ZipCodeProperty, "ZipCode is required"));
      BusinessRules.AddRule(new Required(CountryProperty, "Country is required"));
    }


    [Fetch]
    [RunLocal]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
       Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
    private async Task GetByUserId(GetByUserIdCriteria criteria)

    {
      using (BypassPropertyChecks)
      {
        var dto = await _userProfileDal.GetByUserIdAsync(criteria.UserId);

        if (dto == null)
        {
          throw new Ytg.Framework.Exceptions.DataNotFoundException("UserProfile not found based on criteria");
        }
        FetchData(dto);
      }
    }

    [RunLocal]
    [Insert]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
        Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
    private async Task Insert()
    {
      base.DataPortal_Insert();

      using (BypassPropertyChecks)
      {
        var dto = await _userProfileDal.InsertAsync(ToDto());

        FetchData(dto);

        MarkIdle();
      }
    }

    [RunLocal]
    [Update]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
        Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
    private async Task Update()
    {
      base.DataPortal_Update();

      using (BypassPropertyChecks)
      {
        var dto = await _userProfileDal.UpdateAsync(ToDto());

        FetchData(dto);

        MarkIdle();
      }
    }



    private void FetchData(UserProfileDto dto)
    {
      base.FetchData(dto);

      this.UserProfileId = dto.UserProfileId;
      this.UserId = dto.UserId;
      this.FirstName = dto.FirstName;
      this.MiddleName = dto.MiddleName;
      this.LastName = dto.LastName;
      this.Suffix = dto.Suffix;
      this.DisplayName = dto.DisplayName;
      this.OfficePhoneNumber = dto.OfficePhoneNumber;
      this.MobilePhoneNumber = dto.MobilePhoneNumber;
      this.BirthCity = dto.BirthCity;
      this.BirthState = dto.BirthState;
      this.BirthCountry = dto.BirthCountry;
      this.CountryCitizenship = dto.CountryCitizenship;
      this.AbsId = dto.AbsId;
      this.CertificationStatus = dto.CertificationStatus;
      this.NPI = dto.NPI;
      this.GenderId = dto.GenderId;
      this.BirthDate = dto.BirthDate;
      this.Race = dto.Race;
      this.Ethnicity = dto.Ethnicity;
      this.FirstLanguageId = dto.FirstLanguageId;
      this.BestLanguageId = dto.BestLanguageId;
      this.ReceiveComms = dto.ReceiveComms;
      this.UserConfirmed = dto.UserConfirmed;
      this.UserConfirmedDate = dto.UserConfirmedDate;
      this.Street1 = dto.Street1;
      this.Street2 = dto.Street2;
      this.City = dto.City;
      this.State = dto.State;
      this.ZipCode = dto.ZipCode;
      this.Country = dto.Country;
    }

    internal UserProfileDto ToDto()
    {
      var dto = new UserProfileDto();
      return ToDto(dto);
    }
    internal UserProfileDto ToDto(UserProfileDto dto)
    {
      base.ToDto(dto);

      dto.UserProfileId = this.UserProfileId;
      dto.UserId = this.UserId;
      dto.FirstName = this.FirstName;
      dto.MiddleName = this.MiddleName;
      dto.LastName = this.LastName;
      dto.Suffix = this.Suffix;
      dto.DisplayName = this.DisplayName;
      dto.OfficePhoneNumber = this.OfficePhoneNumber;
      dto.MobilePhoneNumber = this.MobilePhoneNumber;
      dto.BirthCity = this.BirthCity;
      dto.BirthState = this.BirthState;
      dto.BirthCountry = this.BirthCountry;
      dto.CountryCitizenship = this.CountryCitizenship;
      dto.AbsId = this.AbsId;
      dto.CertificationStatus = this.CertificationStatus;
      dto.NPI = this.NPI;
      dto.GenderId = this.GenderId;
      dto.BirthDate = this.BirthDate;
      dto.Race = this.Race;
      dto.Ethnicity = this.Ethnicity;
      dto.FirstLanguageId = this.FirstLanguageId;
      dto.BestLanguageId = this.BestLanguageId;
      dto.ReceiveComms = this.ReceiveComms;
      dto.UserConfirmed = this.UserConfirmed;
      dto.UserConfirmedDate = this.UserConfirmedDate;
      dto.Street1 = this.Street1;
      dto.Street2 = this.Street2;
      dto.City = this.City;
      dto.State = this.State;
      dto.ZipCode = this.ZipCode;
      dto.Country = this.Country;

      return dto;
    }


  }
}
