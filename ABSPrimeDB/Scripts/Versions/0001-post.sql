/*
Version 1
*/
SET @versionNumber = 1;

IF dbo.[Version_ShouldRunScript] ( @versionNumber, ~@postDeployment ) = 1
BEGIN
	
	-- :r "..\Picklists\REL 1.0 Clinical Activity.sql"
	-- :r "..\Picklists\REL 1.0 Clinical Level.sql"
	-- :r "..\Picklists\REL 1.0 Graduate Profile.sql"
	-- :r "..\Migration\REL 1.0 GME Rotations.sql"
	-- :r "..\Migration\REL 1.0 Medical Training.sql"
	-- :r "..\Migration\REL 1.0 User Fellowship.sql"
	-- :r "..\Migration\REL 1.0 Program Address.sql"
    -- :r "..\Migration\REL 1.0 User Accommodations.sql"
    -- :r "..\Migration\REL 1.0 User Professional Standing.sql"
	-- :r "..\Picklists\REL 1.0 Accommodation Ids.sql"
	-- :r "..\Migration\REL 1.0 FPD Recog.sql"
	-- :r "..\Migration\REL 1.0 Outcome Registries.sql"
	-- :r "..\Picklists\REL 1.0 Document Types.sql"
	-- :r "..\Picklists\REL 1.0 Certificate Types.sql"
	-- :r "..\Picklists\REL 1.0 License Types.sql"
	-- :r "..\Migration\REL 1.0 Licenses Type ID.sql"
	-- :r "..\Picklists\REL 1.0 Primary Practice.sql"
	-- :r "..\Picklists\REL 1.0 Organization Type.sql"
    -- :r "..\Picklists\REL 1.0 Practice Types.sql" 
    -- :r "..\Picklists\REL 1.0 Appointment Types.sql"
	-- :r "..\Migration\REL 1.0 User Hospital Appointments.sql"
    -- :r "..\Migration\REL 1.0 User Certificates Other.sql"
    -- :r "..\Migration\REL 1.0 User Sanctions.sql"
	-- :r "..\Picklists\REL 1.0 Exam Statuses.sql"
	-- :r "..\Picklists\REL 1.0 Exam Specialties.sql"
	-- :r "..\Picklists\REL 1.0 Exam Types.sql"
	-- :r "..\Migration\REL 1.0 Exam.sql"
	-- :r "..\Migration\REL 1.0 Exam Headers.sql"
	-- :r "..\Picklists\REL 1.0 Score Results.sql"
    -- :r "..\Migration\REL 1.0 User Claims.sql"
	:r "..\Migration\REL 1.0 CME EXP DATE.sql"
	-- :r "..\Migration\REL 1.0 Exam Header ID.sql"
	-- :r "..\Migration\REL 1.0 User Clinically Inactive.sql"
	-- :r "..\Picklists\REL 1.0 Fellowship Type Picklist.sql"
	:r "..\Migration\REL 1.0 UserId in fee_received.sql"
	:r "..\Migration\REL 1.0 UserId in inv_det.sql"
	-- :r "..\Migration\REL 1.0 Lapsed Pathway.sql"
	-- :r "..\Picklists\REL 1.0 Exam Formats.sql"
	-- :r "..\Picklists\REL 1.0 Training Types.sql"
	-- :r "..\Picklists\REL 1.0 Languages.sql"
	-- :r "..\Picklists\REL 1.0 Gender.sql"
	--:r "..\Migration\REL 1.0 User Addresses.sql"
	--:r "..\Migration\REL 1.0 User Profiles Ethnicity.sql"
	--:r "..\Migration\REL 1.0 User Profiles First Language.sql"
	--:r "..\Migration\REL 1.0 User Profiles Best Language.sql"
    --:r "..\Migration\REL 1.0 User Profiles Receive Comms.sql"
    --:r "..\Migration\REL 1.0 User Profiles Birth Country.sql"
    --:r "..\Migration\REL 1.0 User Profiles Country Citizenship.sql"
    --:r "..\Migration\REL 1.0 User Profiles Race.sql"
    --:r "..\Migration\REL 1.0 User Profiles Gender.sql"


	EXEC [Version_SetPostDeploymentFlag] @versionNumber=@versionNumber;

END