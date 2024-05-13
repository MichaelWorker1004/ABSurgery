CREATE PROCEDURE [dbo].[dev_ins_cescoring_test_users]
    --Users VARS
    @ExaminerEmailAddress VARCHAR(320),
    @AssociateExaminerEmailAddress VARCHAR(320),
    @ExaminerUserName VARCHAR(50),
    @AssociateExaminerUserName VARCHAR(50),
    @ExaminerPassword VARCHAR(50),
    @AssociateExaminerPassword VARCHAR(50),



    --User_Profile VARS
    @ExaminerFirstName VARCHAR(35),
    @ExaminerLastName VARCHAR(35),
    @AssociateExaminerFirstName VARCHAR(35),
    @AssociateExaminerLastName VARCHAR(35),


    --Exam Schedules VARS
    @ExamineeUserId INT,
    @MeetingLink VARCHAR(250),

    --Exam Case VARS
    @CaseHeaderId INT

AS

    DECLARE @Examinersalt VARCHAR(36) = CONVERT(VARCHAR(36), NEWID())
    DECLARE @AssociateExaminersalt VARCHAR(36) = CONVERT(VARCHAR(36), NEWID())
    DECLARE @Examinerhashedpassword varbinary(64) = HASHBYTES('SHA2_256', CONCAT(@ExaminerPassword, @Examinersalt))
    DECLARE @AssociateExaminerhashedpassword varbinary(64) = HASHBYTES('SHA2_256', CONCAT(@AssociateExaminerPassword, @AssociateExaminersalt))


    INSERT INTO users(EmailAddress, UserName, Password,SALT, CreatedByUserId, LastUpdatedByUserId)
    VALUES (@ExaminerEmailAddress, @ExaminerUserName, @Examinerhashedpassword,@Examinersalt, 6, 6),
           (@AssociateExaminerEmailAddress, @AssociateExaminerUserName, @AssociateExaminerhashedpassword,@AssociateExaminersalt, 6, 6)

    DECLARE @ExaminerUserId INT = (SELECT MAX(UserId)-1 FROM users)
    DECLARE @AssociateExaminerUserId INT = (SELECT MAX(UserId) FROM users)

    INSERT INTO dbo.user_claims(ApplicationClaimId, UserId, CreatedByUserId, LastUpdatedByUserId)
    VALUES
        (1, @ExaminerUserId, 6, 6),
        (1, @AssociateExaminerUserId, 6, 6),
        (3, @ExaminerUserId, 6, 6),
        (3, @AssociateExaminerUserId, 6, 6),
        (4, @ExaminerUserId, 6, 6),
        (4, @AssociateExaminerUserId, 6, 6)

    DECLARE @ExaminerDisplayName VARCHAR(70) = @ExaminerFirstName + ' ' + @ExaminerLastName
    DECLARE @AssociateExaminerDisplayName VARCHAR(70) = @AssociateExaminerFirstName + ' ' + @AssociateExaminerLastName
    DECLARE @ExaminerABSId INT = (SELECT max(Id)+1 from (SELECT  TRY_CAST(ABSId AS INT) as 'ID' FROM user_profiles)absId)
    DECLARE @AssociateExaminerABSId INT = (SELECT max(Id)+2 from (SELECT  TRY_CAST(ABSId AS INT) as 'ID' FROM user_profiles)absId)

    INSERT INTO user_profiles(UserId, FirstName, LastName, DisplayName,ABSId, CreatedByUserId, LastUpdatedByUserId)
    VALUES (@ExaminerUserId, @ExaminerFirstName, @ExaminerLastName, @ExaminerDisplayName,@ExaminerABSId, 6, 6),
           (@AssociateExaminerUserId, @AssociateExaminerFirstName, @AssociateExaminerLastName, @AssociateExaminerDisplayName,@AssociateExaminerABSId, 6, 6)


    INSERT INTO exam_schedules (ExamDate, DayNumber, SessionNumber, StartTime, EndTime, ExaminerUserId, AssociateExaminerUserId, ExamineeUserId, MeetingLink, Roster,ExaminerIsCurrentSession,AssociateIsCurrentSession,CreatedByUserId,LastUpdatedByUserId)
    VALUES ('2024-01-01', 1, 1, '9:00', '10:00', @ExaminerUserId, @AssociateExaminerUserId, @ExamineeUserId, @MeetingLink, 'A',1,1,6,6),
           ('2024-01-01', 1, 2, '10:00', '11:00', @ExaminerUserId, @AssociateExaminerUserId, @ExamineeUserId+1, @MeetingLink,'A',0,0,6,6),
           ('2024-01-01', 1, 3, '11:00', '12:00', @ExaminerUserId, @AssociateExaminerUserId, @ExamineeUserId+2, @MeetingLink,'B',0,0,6,6),
           ('2024-01-01', 1, 4, '12:00', '13:00', @ExaminerUserId, @AssociateExaminerUserId, @ExamineeUserId+3, @MeetingLink,'B',0,0,6,6)

    DECLARE @ExamScheduleId INT = (SELECT MAX(Id) FROM exam_schedules)

    INSERT INTO exam_schedule_links(ExamScheduleId, ExamHeaderId, CreatedByUserId, LastUpdatedByUserId)
    VALUES(@ExamScheduleId, 491, 6, 6),
          (@ExamScheduleId-1, 491, 6, 6),
          (@ExamScheduleId-2, 491, 6, 6),
          (@ExamScheduleId-3, 491, 6, 6)


    INSERT INTO exam_cases(ExamScheduleId, CaseId, SortOrder, CreatedByUserId,  LastUpdatedByUserId)
    VALUES (@ExamScheduleId, @CaseHeaderId, 1, 6, 6),
           (@ExamScheduleId, @CaseHeaderId+1, 2, 6, 6),
           (@ExamScheduleId, @CaseHeaderId+2, 3, 6, 6),
           (@ExamScheduleId, @CaseHeaderId+3, 5, 6, 6),
           (@ExamScheduleId-1, @CaseHeaderId+4, 1, 6, 6),
           (@ExamScheduleId-1, @CaseHeaderId+5, 2, 6, 6),
           (@ExamScheduleId-1, @CaseHeaderId+6, 3, 6, 6),
           (@ExamScheduleId-1, @CaseHeaderId+7, 5, 6, 6),
           (@ExamScheduleId-2, @CaseHeaderId+8, 1, 6, 6),
           (@ExamScheduleId-2, @CaseHeaderId+9, 2, 6, 6),
           (@ExamScheduleId-2, @CaseHeaderId+10, 3, 6, 6),
           (@ExamScheduleId-2, @CaseHeaderId+11, 5, 6, 6),
           (@ExamScheduleId-3, @CaseHeaderId+12, 1, 6, 6),
           (@ExamScheduleId-3, @CaseHeaderId+13, 2, 6, 6),
           (@ExamScheduleId-3, @CaseHeaderId+14, 3, 6, 6),
           (@ExamScheduleId-3, @CaseHeaderId+15, 5, 6, 6)


