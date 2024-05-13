CREATE proc ins_cme
	@doc text,
	@filename varchar(100)
AS
BEGIN
	DECLARE @idoc int,@ErrorCode int,
		   @ReportingOrganization varchar(300),
		   @inv_num varchar(16),
		   @org_id varchar(16),
		   @FirstName varchar(30),
		   @MiddleName varchar(30),
		   @LastName varchar(30),
		   @Email varchar(100),
		   @ProviderOrganization varchar(300),
		   @ActivityName varchar(1000),
		   @ModuleName varchar(1000),
		   @AccreditingBody varchar(300),
		   @AccreditedProvider varchar(300),
		   @NonAccreditedProvider varchar(300),
		   @ReleaseDate date,
		   @Hours numeric(9,2),
		   @SelfAssessment varchar(50),
		   @CreditID varchar(50),
		   @FileDate datetime,
		   @Editable bit,
		   @credits_sa numeric(9,2),
		   @current_mccode varchar(6)

	SET @ErrorCode=3 	
	SET @Editable=0
	EXEC sp_xml_preparedocument @idoc OUTPUT, @doc,'<root xmlns:a = "http://ns.medbiq.org/activityreport/v1/" 
	xmlns:xsi = "http://www.w3.org/2001/XMLSchema-instance" 
	xmlns:m = "http://ns.medbiq.org/member/v1/"
	xmlns:n = "http://ns.medbiq.org/name/v1/" 
	xmlns:hx = "http://ns.medbiq.org/lom/extend/v1/"
	xmlns:lom = "http://ltsc.ieee.org/xsd/LOM"
	xsi:schemaLocation = "http://ns.medbiq.org/activityreport/v1/ activityreport.xsd"
	/>' ;

	SELECT
		@inv_num=inv_num ,
		@Email=Email
	FROM OPENXML
	(
	   @idoc, '//a:Member',10
	)
	WITH
	(
	   inv_num varchar(16) 'm:UniqueID[@domain="authcode.absurgery.org"]',
	   Email varchar(100) 'm:EmailAddress'
	)

	IF LEN(RTRIM(ISNULL(@inv_num,'')))<13 OR 
		(NOT EXISTS (SELECT acct_num FROM surgeon WHERE acct_num=@inv_num) AND NOT EXISTS (SELECT id FROM TRACK WHERE candidate+examcode+'A'=@inv_num))
	BEGIN
		SET @ErrorCode=2
	END
	ELSE
	BEGIN
		DECLARE get_cursor CURSOR FOR 
			SELECT
			ReportingOrganization ,
			inv_num ,
			org_id ,
			FirstName ,
			MiddleName ,
			LastName ,
			Email ,
			ProviderOrganization ,
			ActivityName ,
			ModuleName ,
			AccreditingBody ,
			AccreditedProvider ,
			NonAccreditedProvider ,
			ReleaseDate,
			Hours ,
			SelfAssessment,
			CreditID ,
			FileDate ,
			credits_sa 
			FROM OPENXML
			(
			   @idoc, '//a:CreditCertificate',10
			)
			WITH
			(
			   ReportingOrganization varchar(300) '//a:ReportingOrganization',
			   inv_num varchar(16) '//m:UniqueID[@domain="authcode.absurgery.org"]',
			   org_id varchar(16) '//m:UniqueID[@domain!="authcode.absurgery.org"]',
			   FirstName varchar(30) '//n:GivenName',
			   MiddleName varchar(30) '//n:MiddleName',
			   LastName varchar(30) '//n:FamilyName',
			   Email varchar(100) '//m:EmailAddress',
			
			   
			   ProviderOrganization varchar(300) '../../a:ProviderOrganization',
			   ActivityName varchar(1000) '../../a:ActivityName',
			   ModuleName varchar(1000) '../a:ModuleName',
			   
			   AccreditingBody varchar(300) 'a:CreditReceived/hx:accreditingBody',
			   AccreditedProvider varchar(300) 'a:CreditReceived/hx:accreditedProvider',
			   NonAccreditedProvider varchar(300) 'a:CreditReceived/hx:nonAccreditedProvider',
			   ReleaseDate date 'a:CreditReceived/hx:releaseDate',
			   Hours numeric(9,2) 'a:CreditReceived/hx:numberOfCredits',
			   SelfAssessment varchar(50) '../a:Metadata//lom:string',
			   CreditID varchar(50) 'a:CreditID',
			   FileDate datetime '//a:DateTimeCreated',
			   credits_sa numeric(9,2) 'a:CreditReceived/a:CreditFocus/a:NumberOfCredits'
			)
		
		OPEN get_cursor
			FETCH NEXT FROM get_cursor INTO 
			   @ReportingOrganization,
			   @inv_num,
			   @org_id,
			   @FirstName,
			   @MiddleName,
			   @LastName,
			   @Email,
			   @ProviderOrganization,
			   @ActivityName,
			   @ModuleName,
			   @AccreditingBody,
			   @AccreditedProvider,
			   @NonAccreditedProvider,
			   @ReleaseDate,
			   @Hours,
			   @SelfAssessment,
			   @CreditID,
			   @FileDate,
			   @credits_sa			   	
		
		WHILE @@FETCH_STATUS = 0
		BEGIN
			SET @ErrorCode=0
			SET @credits_sa=isnull(@credits_sa,0)
			SET @SelfAssessment=
				CASE 
					WHEN @SelfAssessment='self assessment' THEN 'Yes'
					WHEN isnull(@SelfAssessment,'')='' AND @credits_sa=0 THEN 'No'
					ELSE 'NA'
				END
			
			IF EXISTS (SELECT id FROM cme WHERE CreditID=@CreditID AND ReportingOrganization=@ReportingOrganization AND candidate=left(@inv_num,6))
			BEGIN
				UPDATE cme SET
					created=getdate(),
					inv_num=@inv_num ,
					candidate=left(@inv_num,6),
					org_id=@org_id,
					FirstName=@FirstName ,
					MiddleName=@MiddleName ,
					LastName=@LastName ,
					Email=@Email ,
					ProviderOrganization=@ProviderOrganization ,
					ActivityName=@ActivityName ,
					ModuleName=@ModuleName ,
					AccreditingBody=@AccreditingBody ,
					AccreditedProvider=@AccreditedProvider ,
					NonAccreditedProvider=@NonAccreditedProvider ,
					ReleaseDate=@ReleaseDate ,
					Hours=@Hours ,
					SelfAssessment=@SelfAssessment ,
					FileName=@filename ,
					FileDate=@FileDate,
					Editable=@Editable,
					credits_sa=@credits_sa
				WHERE CreditID=@CreditID AND ReportingOrganization=@ReportingOrganization  AND candidate=left(@inv_num,6)
			END
			ELSE
			BEGIN
				insert into cme
				(
					created ,
					ReportingOrganization ,
					inv_num ,
					candidate,
					org_id ,
					FirstName ,
					MiddleName ,
					LastName ,
					Email ,
					ProviderOrganization ,
					ActivityName ,
					ModuleName ,
					AccreditingBody ,
					AccreditedProvider ,
					NonAccreditedProvider ,
					ReleaseDate ,
					Hours ,
					SelfAssessment ,
					CreditID ,
					FileName ,
					FileDate,
					Editable,
					credits_sa
				) VALUES (
					getdate() ,
					@ReportingOrganization ,
					@inv_num ,
					left(@inv_num,6),
					@org_id ,
					@FirstName ,
					@MiddleName ,
					@LastName ,
					@Email ,
					@ProviderOrganization ,
					@ActivityName ,
					@ModuleName ,
					@AccreditingBody ,
					@AccreditedProvider ,
					@NonAccreditedProvider ,
					@ReleaseDate ,
					@Hours ,
					@SelfAssessment,
					@CreditID ,
					@filename ,
					@FileDate ,
					@Editable,
					@credits_sa
				)
			END
		
			FETCH NEXT FROM get_cursor INTO 
			   @ReportingOrganization,
			   @inv_num,
			   @org_id,
			   @FirstName,
			   @MiddleName,
			   @LastName,
			   @Email,
			   @ProviderOrganization,
			   @ActivityName,
			   @ModuleName,
			   @AccreditingBody,
			   @AccreditedProvider,
			   @NonAccreditedProvider,
			   @ReleaseDate,
			   @Hours,
			   @SelfAssessment,
			   @CreditID,
			   @FileDate,
			   @credits_sa
		END
		
		CLOSE get_cursor
		DEALLOCATE get_cursor
	
		EXEC sp_xml_removedocument @idoc
	END

	SELECT @current_mccode=current_trackcode FROM moc_eligibility WHERE candidate=left(@inv_num,6)
	
	SELECT isnull(@email,'') Email,isnull(@inv_num,'') AuthCode,@ErrorCode ErrorCode,isnull(@ReportingOrganization,'') ReportingOrganization,isnull(@current_mccode,'') current_mccode
END