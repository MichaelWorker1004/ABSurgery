using Csla;
using SurgeonPortal.DataAccess.Contracts.Scoring;
using SurgeonPortal.Library.Contracts.Scoring;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Ytg.Framework.Csla;
using Ytg.Framework.Identity;
using static SurgeonPortal.Library.Scoring.CaseCommentFactory;

namespace SurgeonPortal.Library.Scoring
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0003", Justification = "Direct Injection.")]
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Csla.Analyzers", "CSLA0004", Justification = "Direct Injection.")]
	[Serializable]
	[DataContract] 
	public class CaseComment : YtgBusinessBase<CaseComment>, ICaseComment
    {
        private readonly ICaseCommentDal _caseCommentDal;

        public CaseComment(
            IIdentityProvider identityProvider,
            ICaseCommentDal caseCommentDal)
            : base(identityProvider)
        {
            _caseCommentDal = caseCommentDal;
        }

        [Key] 
        [DisplayName(nameof(Id))]
		public int Id
		{
			get { return GetProperty(IdProperty); }
			set { SetProperty(IdProperty, value); }
		}
		public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);

        [DisplayName(nameof(UserId))]
		public int UserId
		{
			get { return GetProperty(UserIdProperty); }
			set { SetProperty(UserIdProperty, value); }
		}
		public static readonly PropertyInfo<int> UserIdProperty = RegisterProperty<int>(c => c.UserId);

        [DisplayName(nameof(CaseContentId))]
		public int CaseContentId
		{
			get { return GetProperty(CaseContentIdProperty); }
			set { SetProperty(CaseContentIdProperty, value); }
		}
		public static readonly PropertyInfo<int> CaseContentIdProperty = RegisterProperty<int>(c => c.CaseContentId);

        [DisplayName(nameof(Comments))]
		public string Comments
		{
			get { return GetProperty(CommentsProperty); }
			set { SetProperty(CommentsProperty, value); }
		}
		public static readonly PropertyInfo<string> CommentsProperty = RegisterProperty<string>(c => c.Comments);



        /// <summary>
        /// This method is used to apply authorization rules on the object
        /// </summary>
        [ObjectAuthorizationRules]
        public static void AddObjectAuthorizationRules()
        {
            

        }




        [Fetch]
        [RunLocal]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode",
           Justification = "This method is called indirectly by the CSLA.NET DataPortal.")]
        private async Task GetById(GetByIdCriteria criteria)
        
        {
            using (BypassPropertyChecks)
            {
                var dto = await _caseCommentDal.GetByIdAsync(criteria.Id);
        
                if(dto == null)
                {
                    throw new Ytg.Framework.Exceptions.DataNotFoundException("CaseComment not found based on criteria");
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
                var dto = await _caseCommentDal.InsertAsync(ToDto());
        
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
                var dto = await _caseCommentDal.UpdateAsync(ToDto());
        
                FetchData(dto);
        
                MarkIdle();
            }
        }



        private void FetchData(CaseCommentDto dto)
		{
            base.FetchData(dto);
            
			this.Id = dto.Id;
			this.UserId = dto.UserId;
			this.CaseContentId = dto.CaseContentId;
			this.Comments = dto.Comments;
		}

		internal CaseCommentDto ToDto()
		{
			var dto = new CaseCommentDto();
			return ToDto(dto);
		}
		internal CaseCommentDto ToDto(CaseCommentDto dto)
		{
            base.ToDto(dto);
            
			dto.Id = this.Id;
			dto.UserId = this.UserId;
			dto.CaseContentId = this.CaseContentId;
			dto.Comments = this.Comments;

			return dto;
		}


    }
}
